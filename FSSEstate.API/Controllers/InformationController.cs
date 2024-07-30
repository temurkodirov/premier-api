using AutoMapper;
using FSSEstate.API.Authorization;
using FSSEstate.API.Models.InformationModels;
using FSSEstate.Business.Interfaces;
using FSSEstate.Core.Models.InformationModels;
using Microsoft.AspNetCore.Mvc;

namespace FSSEstate.API.Controllers
{
//    [ApiController]
//    [Route("api")]
//    public class InformationController : BaseController
//    {
//        public InformationController(IService service, IMapper mapper) : base(service, mapper)
//        {
//        }
//        [AllowAnonymous]

//        [HttpPost("news/create")]
//        public async Task<IActionResult> Create([FromForm] InformationCreateRequest information)
//        {
//            try
//            {
//  //              var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
////                var resultClaims = await Service.UserService.GetUserClaimsAsync(jwt);

//                //if (resultClaims != null)
//                //{
//                    var informationModel = Mapper.Map<InformationCreateModel>(information);
//                    informationModel.AccountId = 1;
//                    var result = await Service.InformationService.CreateAsync(informationModel);
//                    return Ok(result);
//               // }
//                //return BadRequest("Authorization problem");
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }
//        [AllowAnonymous]

//        [HttpPost("news/delete/{id}")]
//        public async Task<IActionResult> Delete(long id)
//        {
//            try
//            {
//                var result = await Service.InformationService.DeleteAsync(id);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        [HttpGet("news/get-all")]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetAll([FromQuery] InformationFilterParams filterParams)
//        {
//            try
//            {
//                var result = await Service.InformationService.GetAllAsync(filterParams);
//                return Ok(result.ToPagedListData());
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        [HttpGet("news/get/{id}")]
//        [AllowAnonymous]
//        public async Task<IActionResult> Get(long id)
//        {
//            try
//            {
//                var result = await Service.InformationService.GetByIdAsync(id);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        [HttpPost("news/update")]
//        public async Task<IActionResult> Update(InformationUpdateRequest information)
//        {
//            try
//            {
//                var informationUpdateModel = Mapper.Map<InformationUpdateModel>(information);
//                var result = await Service.InformationService.UpdateAsync(information.Id, informationUpdateModel);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        [HttpGet("news/get-photos")]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetProjectPhotos([FromQuery] long informationId)
//        {
//            var photos = await Service.InformationPhotoService.GetImageFiles(informationId);

//            return Ok(photos);
//        }

//        [HttpGet("news/get-photo-by-path")]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetProjectPhotoByPath([FromQuery] string path)
//        {
//            var photo = await Service.InformationPhotoService.GetImageAsync(path);

//            return Ok(photo);
//        }
//    }
}
