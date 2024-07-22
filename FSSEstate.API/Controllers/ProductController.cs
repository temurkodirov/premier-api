using AutoMapper;
using FSSEstate.API.Authorization;
using FSSEstate.API.Models.xProductModels;
using FSSEstate.Business.Interfaces;
using FSSEstate.Core.Models.ProjectModels;
using FSSEstate.Core.Models.xProductModels;
using Microsoft.AspNetCore.Mvc;

namespace FSSEstate.API.Controllers;



[ApiController]
[Route("api")]
public class ProductController : BaseController
{
    public ProductController(IService service, IMapper mapper) : base(service, mapper)
    {
    }

    [HttpPost("product/create")]
    public async Task<IActionResult> Create([FromForm] xProductCreateRequest product)
    {
        try
        {
            var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            var resultClaims = await Service.UserService.GetUserClaimsAsync(jwt);

            if (resultClaims != null)
            {
                var productModel = Mapper.Map<xProductCreateModel>(product);
                var result = await Service.xProductService.CreateAsync(productModel);
                return Ok(result);
            }
            return BadRequest("Authorization problem");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    [HttpPost("product/delete")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            var result = await Service.xProductService.DeleteAsync(id);
            return Ok(result);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpGet("product/get-all")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] xProductFilterParams filterParams)
    {
        try
        {
            var result = await Service.xProductService.GetAllAsync(filterParams);
            return Ok(result.ToPagedListData());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpGet("product/get/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(long id)
    {
        try
        {
            var result = await Service.xProductService.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }



}
