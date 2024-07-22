using AutoMapper;
using AutoMapper.QueryableExtensions;
using FSSEstate.Business.Interfaces;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Core.Models.Agents;
using FSSEstate.Core.Models.ReviewModels;
using FSSEstate.Core.Utility.Listing;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces;

namespace FSSEstate.Business.Implementations
{
    public class ReviewService : BaseService, IReviewService
    {
        public ReviewService(IUnitOfWork unitOfWork, IService service, IJwtUtils jwtUtils, IMapper mapper, IFileService fileService) : base(unitOfWork, service, jwtUtils, mapper, fileService)
        {
        }

        public async Task<bool> CreateAsync(ReviewCreateModel review)
        {
            var reviewEntity = Mapper.Map<ReviewEntity>(review);
            var items = await UnitOfWork.ReviewRepository.GetAllByQueryAsync(item => (item.AccountId == review.AccountId) &&
            (item.ProjectId == review.ProjectId));

            if (items.Any())
                throw new Exception("Already exist");

            await UnitOfWork.ReviewRepository.AddAsync(reviewEntity);
            await UnitOfWork.CommitAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var review = await UnitOfWork.ReviewRepository.GetAsync(item => item.Id == id);
            if (review is null) throw new Exception("Review not found!");

            UnitOfWork.ReviewRepository.Remove(review);
            await UnitOfWork.CommitAsync();

            return true;
        }

        public async Task<PagedList<ReviewModel>> GetAllAsync(ReviewFilterParams filterParams)
        {
            var entityItems = await UnitOfWork.ReviewRepository.GetAllByQueryAsync(item =>
               (filterParams.SearchText == string.Empty || item.Description.Contains(filterParams.SearchText)) &&
               (filterParams.ProjectId == null || item.ProjectId == filterParams.ProjectId) &&
               (filterParams.AccountId == null || item.AccountId == filterParams.AccountId),
                null, x => x.CreatedAt,
               filterParams.Order == "desc");
            var items = entityItems.ProjectTo<ReviewModel>(Mapper.ConfigurationProvider);
            PagedList<ReviewModel> pagedList = PagedList<ReviewModel>.ToPagedListFromQuery(
                filterParams.PageNumber,
                filterParams.PageSize,
                filterParams.Order,
                items);

            return pagedList;
        }

        public async Task<ReviewModel> GetByIdAsync(long id)
        {
            var reviewEntity = await UnitOfWork.ReviewRepository.GetAsync(item => item.Id == id);
            if (reviewEntity is null) throw new Exception("Review not found!");

            var review = Mapper.Map<ReviewModel>(reviewEntity);
            return review;
        }

        public async Task<ReviewWithAverageGradeModel> GetWithAverageGrade(ReviewFilterParams filterParams)
        {
            var result = new ReviewWithAverageGradeModel();

            var entityItems = await UnitOfWork.ReviewRepository.GetAllByQueryAsync(item =>
               (filterParams.SearchText == string.Empty || item.Description.Contains(filterParams.SearchText)) &&
               (filterParams.ProjectId == null || item.ProjectId == filterParams.ProjectId) &&
               (filterParams.AccountId == null || item.AccountId == filterParams.AccountId),
                null, x => x.CreatedAt,
               filterParams.Order == "desc");
            var items = entityItems.ProjectTo<ReviewModel>(Mapper.ConfigurationProvider);
            var averageGrade = items.Select(item => item.Mark).Average();

            PagedList<ReviewModel> pagedList = PagedList<ReviewModel>.ToPagedListFromQuery(
                filterParams.PageNumber,
                filterParams.PageSize,
                filterParams.Order,
                items);

            result.Reviews = pagedList.ToPagedListData();
            result.AverageGrade = averageGrade;

            return result;
        }

        public async Task<bool> UpdateAsync(long id, ReviewUpdateModel review)
        {
            var reviewEntity = await UnitOfWork.ReviewRepository.GetAsync(item => item.Id == id);
            if (reviewEntity is null) throw new Exception("Review not found!");
            if(review.AccountId == reviewEntity.AccountId)
            {
                Mapper.Map(review, reviewEntity);
                UnitOfWork.ReviewRepository.Update(reviewEntity);
                await UnitOfWork.CommitAsync();
                return true;
            }

            return false;
        }
    }
}
