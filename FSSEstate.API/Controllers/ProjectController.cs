using AutoMapper;
using FSSEstate.API.Authorization;
using FSSEstate.API.Models.FavouriteProjectModels;
using FSSEstate.API.Models.ProjectModels;
using FSSEstate.Business.Interfaces;
using FSSEstate.Core.Models.FavouriteProjectModels;
using FSSEstate.Core.Models.ProjectModels;
using Microsoft.AspNetCore.Mvc;

namespace FSSEstate.API.Controllers;

//[ApiController]
//[Route("api")]
//public class ProjectController : BaseController
//{
//    public ProjectController(IService service, IMapper mapper) : base(service, mapper)
//    {
//    }

//    [HttpPost("project/create")]
//    public async Task<IActionResult> Create([FromForm] ProjectCreateRequest project)
//    {
//        try
//        {
//            var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
//            var resultClaims = await Service.UserService.GetUserClaimsAsync(jwt);

//            if (resultClaims != null)
//            {
//                var projectModel = Mapper.Map<ProjectCreateModel>(project);
//                projectModel.AccountId = long.Parse(resultClaims.Id);
//                var result = await Service.ProjectService.CreateAsync(projectModel);
//                return Ok(result);
//            }
//            return BadRequest("Authorization problem");
//        }
//        catch (Exception ex)
//        {
//            throw new Exception(ex.Message);
//        }
//    }

//    [HttpPost("project/delete")]
//    public async Task<IActionResult> Delete(long id)
//    {
//        try
//        {
//            var result = await Service.ProjectService.DeleteAsync(id);
//            return Ok(result);
//        }
//        catch (Exception ex)
//        {
//            throw new Exception(ex.Message);
//        }
//    }

//    [HttpGet("project/get-all")]
//    [AllowAnonymous]
//    public async Task<IActionResult> GetAll([FromQuery] ProjectFilterParams filterParams)
//    {
//        try
//        {
//            var result = await Service.ProjectService.GetAllAsync(filterParams);
//            return Ok(result.ToPagedListData());
//        }
//        catch (Exception ex)
//        {
//            throw new Exception(ex.Message);
//        }
//    }

//    [HttpGet("project/get/{id}")]
//    [AllowAnonymous]
//    public async Task<IActionResult> Get(long id)
//    {
//        try
//        {
//            var result = await Service.ProjectService.GetByIdAsync(id);
//            return Ok(result);
//        }
//        catch (Exception ex)
//        {
//            throw new Exception(ex.Message);
//        }
//    }

//    [HttpPost("project/update/{id}")]
//    public async Task<IActionResult> Update(long id, ProjectUpdateRequest project)
//    {
//        try
//        {
//            var projectUpdateModel = Mapper.Map<ProjectUpdateModel>(project);
//            var result = await Service.ProjectService.UpdateAsync(id, projectUpdateModel);
//            return Ok(result);
//        }
//        catch (Exception ex)
//        {
//            throw new Exception(ex.Message);
//        }
//    }

//    [HttpGet("projects/top-list")]
//    [AllowAnonymous]
//    public async Task<IActionResult> GetAllTopList([FromQuery] ProjectFilterParams filterParams)
//    {
//        try
//        {
//            var result = await Service.ProjectService.GetAllAsync(filterParams);
//            return Ok(result.ToPagedListData());
//        }
//        catch (Exception ex)
//        {
//            throw new Exception(ex.Message);
//        }
//    }

//    [HttpGet("project/get-photos")]
//    [AllowAnonymous]
//    public async Task<IActionResult> GetProjectPhotos([FromQuery] long projectId)
//    {
//        var photos = await Service.ProjectPhotoService.GetImageFiles(projectId);

//        return Ok(photos);
//    }

//    [HttpGet("project/get-photo")]
//    [AllowAnonymous]
//    public async Task<IActionResult> GetProjectPhoto([FromQuery] string path)
//    {
//        var photo = await Service.ProjectPhotoService.GetImageAsync(path);

//        return Ok(photo);
//    }

//    [HttpGet("projects/my-list")]
//    public async Task<IActionResult> GetMyProjects([FromQuery] ProjectFilterParams projectFilterParams)
//    {
//        var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
//        var resultClaims = await Service.UserService.GetUserClaimsAsync(jwt);

//        if (resultClaims != null)
//        {
//            var result = await Service.ProjectService.GetAllByUserIdAsync(projectFilterParams, long.Parse(resultClaims.Id));

//            return Ok(result.ToPagedListData());
//        }
//        return BadRequest("Authorization problem");
//    }

//    [HttpGet("projects/list-by-account-id")]
//    [AllowAnonymous]
//    public async Task<IActionResult> GetProjectsByAccountId([FromQuery] ProjectFilterParams projectFilterParams, long accountId)
//    {
//        var result = await Service.ProjectService.GetAllByUserIdAsync(projectFilterParams, accountId);

//        return Ok(result.ToPagedListData());
//    }

//    [HttpGet("projects/my-favourite-list")]
//    public async Task<IActionResult> GetMyFavouriteProjects([FromQuery] FavouriteProjectFilterParams projectFilterParams)
//    {
//        var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
//        var resultClaims = await Service.UserService.GetUserClaimsAsync(jwt);

//        if (resultClaims != null)
//        {
//            var result = await Service.FavouriteProjectService.GetAllAsync(projectFilterParams, long.Parse(resultClaims.Id));

//            return Ok(result.ToPagedListData());
//        }
//        return BadRequest("Authorization problem");
//    }

//    [HttpPost("project/add-favourite")]
//    public async Task<IActionResult> CreateFavourite([FromForm] FavouriteProjectCreateRequest favouriteProject)
//    {
//        try
//        {
//            var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
//            var resultClaims = await Service.UserService.GetUserClaimsAsync(jwt);

//            if (resultClaims != null)
//            {
//                var projectModel = Mapper.Map<FavouriteProjectCreateModel>(favouriteProject);
//                projectModel.AccountId = long.Parse(resultClaims.Id);
//                var result = await Service.FavouriteProjectService.CreateAsync(projectModel);
//                return Ok(result);
//            }
//            return BadRequest("Authorization problem");
//        }
//        catch (Exception ex)
//        {
//            throw new Exception(ex.Message);
//        }
//    }

//    [HttpPost("project/delete-favourite")]
//    public async Task<IActionResult> DeleteFavourite(long id)
//    {
//        try
//        {
//            var result = await Service.FavouriteProjectService.DeleteAsync(id);
//            return Ok(result);
//        }
//        catch (Exception ex)
//        {
//            throw new Exception(ex.Message);
//        }
//    }
//}
