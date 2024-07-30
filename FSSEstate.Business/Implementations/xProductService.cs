using AutoMapper;
using AutoMapper.QueryableExtensions;
using FSSEstate.Business.Implementations.Helpers;
using FSSEstate.Business.Interfaces;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Core.Models.xProductCharacteristicsModels;
using FSSEstate.Core.Models.xProductImageModels;
using FSSEstate.Core.Models.xProductModels;
using FSSEstate.Core.Utility.Listing;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FSSEstate.Business.Implementations;

public class xProductService : BaseService, IxProductService
{
    private IxProductCharacteristicsService CharacteristicsService;
    public xProductService(IUnitOfWork unitOfWork, 
                            IService service,
                            IJwtUtils jwtUtils,
                            IMapper mapper,
                            IFileService fileService, IxProductCharacteristicsService charService
                            ) : base( unitOfWork, 
                                                                service, 
                                                                jwtUtils, 
                                                                mapper, 
                                                                fileService)
    {
        CharacteristicsService = charService;
    }//constructor


    public async Task<bool> CreateAsync(xProductCreateModel product)
    {
        var productEntity = Mapper.Map<xProduct>(product);
        productEntity.SeoUrl = SeoUrlHelper.ToSeoUrl(productEntity.NameUz);
        await UnitOfWork.XProductRepository.AddAsync(productEntity);
        await UnitOfWork.CommitAsync();
        if (productEntity.Id != 0)
        {
            try
            {
                int countImages = 0;
                foreach (var item in product.Images)
                {
                    countImages++;
                    var imgPath = await FileService.UploadImageAsync(item, "Product");

                    var productImageEntity = new xProductImage
                    {
                        ProductId = productEntity.Id,
                        ImagePath = imgPath,
                        IsMain = countImages == 1 ? true : false,
                        CreatedAt = DateTime.UtcNow.AddHours(5),
                        UpdatedAt = DateTime.UtcNow.AddHours(5)
                    };

                    await UnitOfWork.XProductImageRepository.AddAsync(productImageEntity);
                    await UnitOfWork.CommitAsync();
                }
                    
                int countChars = 0;
                foreach (var item in product.CharacteristicsList)
                {
                    item.ProductId = productEntity.Id;
                    countChars++;
                    await CharacteristicsService.CreateAsync(item);
                    await UnitOfWork.CommitAsync();
                }
                
                productEntity.SeoUrl += $"-{productEntity.Id}";
                await UnitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        return false;
    }
    

    public async Task<bool> DeleteAsync(long id)
    {
        var product = await UnitOfWork.XProductRepository.GetAsync(item => item.Id == id);
        if (product is null) throw new Exception("Product not found!");

        var images = await UnitOfWork.XProductImageRepository.GetAllByQueryAsync(item => item.ProductId == product.Id);
        if (!images.IsNullOrEmpty())
        {
            foreach (var image in images)
            {
                await FileService.DeleteImageAsync(image.ImagePath);
            }
            UnitOfWork.XProductImageRepository.RemoveRange(images);
        }
        
        var chars = await UnitOfWork.XProductCharacteristicsRepository.GetAllByQueryAsync(item =>
            item.ProductId == product.Id);

        if (!chars.IsNullOrEmpty())
        {
            foreach (var character in chars)
            {
                await CharacteristicsService.DeleteAsync(character.Id);
            }
            UnitOfWork.XProductCharacteristicsRepository.RemoveRange(chars);
        }
        UnitOfWork.XProductRepository.Remove(product);
        await UnitOfWork.CommitAsync();
        return true;
    
    }

    public async Task<PagedList<xProductModel>> GetAllAsync(xProductFilterParams filterParams)
    {
        var entityItems = await UnitOfWork.XProductRepository.GetAllByQueryAsync(item => 
        (filterParams.SearchText == string.Empty || item.NameRu.ToLower().Contains(filterParams.SearchText.ToLower())) &&
        (filterParams.SearchText == string.Empty || item.NameUz.ToLower().Contains(filterParams.SearchText.ToLower())) && 
        (filterParams.CategoryId == null || item.CategoryId == filterParams.CategoryId) && 
        (filterParams.MaxPrice == null || item.PriceUsd <= filterParams.MaxPrice) &&    
        (filterParams.MinPrice == null || item.PriceUsd >= filterParams.MinPrice), 
        null, x => x.CreatedAt, filterParams.Order == "desc");

        var productItems = entityItems.ProjectTo<xProductModel>(Mapper.ConfigurationProvider); 

        PagedList<xProductModel> pagedlist = PagedList<xProductModel>.ToPagedListFromQuery(
            filterParams.PageNumber,
            filterParams.PageSize,
            filterParams.Order,
            productItems);

        foreach (var productItem in pagedlist)
        {
            var imageItem = await UnitOfWork.XProductImageRepository.GetAsync(item => item.ProductId == productItem.Id && item.IsMain);
            if (imageItem != null)
            {
                var image = Mapper.Map<xProductImageModel>(imageItem);

                if (productItem.Images == null)
                    productItem.Images = new List<xProductImageModel>();

                productItem.Images.Add(image);
            }
            
            
            var charItems =
                await UnitOfWork.XProductCharacteristicsRepository.GetAllByQueryAsync(item =>
                    item.ProductId == productItem.Id);
    
            if(!charItems.IsNullOrEmpty())
            {
                var chars = charItems.ProjectTo<xProductCharacteristicsModel>(Mapper.ConfigurationProvider);

                if (productItem.CharacteristicsList == null) 
                    productItem.CharacteristicsList = new List<xProductCharacteristicsModel>();

                productItem.CharacteristicsList.AddRange(chars.ToList());
            }
        }
        return pagedlist;
    }


    public async Task<xProductModel> GetByIdAsync(long id)
    {
        var productEnitity = await UnitOfWork.XProductRepository.GetAsync(item => item.Id == id);
        if (productEnitity is null) throw new Exception("Product not found");

        var productItem = Mapper.Map<xProductModel>(productEnitity);
        var imageItems = await UnitOfWork.XProductImageRepository.GetAllByQueryAsync(item => item.ProductId == productItem.Id);

        if(!imageItems.IsNullOrEmpty())
        {
            var images = imageItems.ProjectTo<xProductImageModel>(Mapper.ConfigurationProvider);

            if (productItem.Images == null) 
                productItem.Images = new List<xProductImageModel>();

            productItem.Images.AddRange(images);
        }

        var charItems =
            await UnitOfWork.XProductCharacteristicsRepository.GetAllByQueryAsync(item =>
                item.ProductId == productItem.Id);
        
        if(!charItems.IsNullOrEmpty())
        {
            var chars = charItems.ProjectTo<xProductCharacteristicsModel>(Mapper.ConfigurationProvider);

            if (productItem.CharacteristicsList == null) 
                productItem.CharacteristicsList = new List<xProductCharacteristicsModel>();

            productItem.CharacteristicsList.AddRange(chars.ToList());
        }
        return productItem;

    }

    public async Task<bool> UpdateAsync(long id, xProductUpdateModel product)
    {
        var productEnitity = await UnitOfWork.XProductRepository.GetAsync(item => item.Id == id);
        if (productEnitity is null) throw new Exception("Product not found");
        Mapper.Map(product, productEnitity);
        UnitOfWork.XProductRepository.Update(productEnitity);
        await UnitOfWork.CommitAsync();

        if (product.Images.Count != 0)
        {
            var images = await UnitOfWork.XProductImageRepository.GetAllByQueryAsync(item => item.ProductId == product.Id);
            if (!images.IsNullOrEmpty())
            {
                foreach (var image in images)
                {
                    await FileService.DeleteImageAsync(image.ImagePath);
                }
                UnitOfWork.XProductImageRepository.RemoveRange(images);
            }
            
            int countImages = 0;
            foreach (var item in product.Images)
            {
                countImages++;
                var imgPath = await FileService.UploadImageAsync(item, "Product");

                var productImageEntity = new xProductImage
                {
                    ProductId = productEnitity.Id,
                    ImagePath = imgPath,
                    IsMain = countImages == 1 ? true : false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await UnitOfWork.XProductImageRepository.AddAsync(productImageEntity);
                await UnitOfWork.CommitAsync();
            }
        }

        if (product.Characteristics.Count != 0)
        {
            var chars = await UnitOfWork.XProductCharacteristicsRepository.GetAllByQueryAsync(item =>
                item.ProductId == product.Id);

            if (!chars.IsNullOrEmpty())
            {
                foreach (var character in chars)
                {
                    await CharacteristicsService.DeleteAsync(character.Id);
                }
                UnitOfWork.XProductCharacteristicsRepository.RemoveRange(chars);
            }
            
            int countChars = 0;
            foreach (var item in product.Characteristics)
            {
                item.ProductId = productEnitity.Id;
                countChars++;
                await CharacteristicsService.CreateAsync(item);
                await UnitOfWork.CommitAsync();
            }
        }
    
        await UnitOfWork.CommitAsync();
        return true;
    }
}
