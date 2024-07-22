using AutoMapper;
using FSSEstate.API.Authorization;
using FSSEstate.API.Models.ProjectCategoryModels;
using FSSEstate.Business.Interfaces;
using FSSEstate.Core.Models.ProjectCategoryModels;
using Microsoft.AspNetCore.Mvc;

namespace FSSEstate.API.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : BaseController
    {
        public CategoryController(IService service, IMapper mapper) : base(service, mapper)
        {
        }
        
        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromForm]CategoryCreateRequest projectCategory)
        {
            try
            {
                bool result = false;

                var projectCategoryModel = Mapper.Map<CategoryCreateModel>(projectCategory);
                result = await Service.CategoryService.CreateAsync(projectCategoryModel);
              
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var result = await Service.CategoryService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("get-all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] CategoryFilterParams filterParams)
        {
            try
            {
                var result = await Service.CategoryService.GetAllAsync(filterParams);
                return Ok(result.ToPagedListData());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var result = await Service.CategoryService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(long id, [FromForm]CategoryUpdateRequest projectCategory)
        {
            try
            {
                var projectCategoryUpdateModel = Mapper.Map<CategoryUpdateModel>(projectCategory);
                var result = await Service.CategoryService.UpdateAsync(id, projectCategoryUpdateModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("get-all-main")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMain([FromQuery] CategoryFilterParams filterParams)
        {
            try
            {
                var result = await Service.CategoryService.GetAllMainAsync(filterParams);
                return Ok(result.ToPagedListData());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
