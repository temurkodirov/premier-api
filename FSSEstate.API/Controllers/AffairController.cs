using AutoMapper;
using FSSEstate.API.Authorization;
using FSSEstate.API.Models.AffairModels;
using FSSEstate.Business.Interfaces;
using FSSEstate.Core.Models.AffairModels;
using Microsoft.AspNetCore.Mvc;

namespace FSSEstate.API.Controllers
{
    //[ApiController]
    //[Route("api")]
    //public class AffairController : BaseController
    //{
    //    public AffairController(IService service, IMapper mapper) : base(service, mapper)
    //    {
    //    }

    //    [HttpPost("service/create")]
    //    public async Task<IActionResult> Create(AffairCreateRequest affair)
    //    {
    //        try
    //        {
    //            bool result = false;

    //            var affairModel = Mapper.Map<AffairCreateModel>(affair);
    //            result = await Service.AffairService.CreateAsync(affairModel);
              
    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception(ex.Message);
    //        }
    //    }

    //    [HttpPost("service/delete")]
    //    public async Task<IActionResult> Delete(long id)
    //    {
    //        try
    //        {
    //            var result = await Service.AffairService.DeleteAsync(id);
    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception(ex.Message);
    //        }
    //    }

    //    [HttpGet("service/get-all")]
    //    [AllowAnonymous]
    //    public async Task<IActionResult> GetAll([FromQuery] AffairFilterParams filterParams)
    //    {
    //        try
    //        {
    //            var result = await Service.AffairService.GetAllAsync(filterParams);
    //            return Ok(result.ToPagedListData());
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception(ex.Message);
    //        }
    //    }

    //    [HttpGet("service/get/{id}")]
    //    public async Task<IActionResult> Get(long id)
    //    {
    //        try
    //        {
    //            var result = await Service.AffairService.GetByIdAsync(id);
    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception(ex.Message);
    //        }
    //    }

    //    [HttpPost("service/update/{id}")]
    //    public async Task<IActionResult> Update(long id, AffairUpdateRequest affair)
    //    {
    //        try
    //        {
    //            var affairUpdateModel = Mapper.Map<AffairUpdateModel>(affair);
    //            var result = await Service.AffairService.UpdateAsync(id, affairUpdateModel);
    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception(ex.Message);
    //        }
    //    }

    //}
}
