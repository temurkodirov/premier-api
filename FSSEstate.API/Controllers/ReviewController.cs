using AutoMapper;
using FSSEstate.API.Authorization;
using FSSEstate.API.Models.AgentModels;
using FSSEstate.API.Models.ReviewModels;
using FSSEstate.Business.Interfaces;
using FSSEstate.Core.Models.Agents;
using FSSEstate.Core.Models.ReviewModels;
using Microsoft.AspNetCore.Mvc;

namespace FSSEstate.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class ReviewController : BaseController
    {
        public ReviewController(IService service, IMapper mapper) : base(service, mapper)
        {
        }

        [HttpPost("review/create")]
        public async Task<IActionResult> Create(ReviewCreateRequest review)
        {
            try
            {
                var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                var resultClaims = await Service.UserService.GetUserClaimsAsync(jwt);

                if (resultClaims != null)
                {
                    var reviewModel = Mapper.Map<ReviewCreateModel>(review);
                    reviewModel.AccountId = long.Parse(resultClaims.Id);
                    var result = await Service.ReviewService.CreateAsync(reviewModel);
                    return Ok(result);
                }
                return BadRequest("Authorization problem");
                    
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("review/delete")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var result = await Service.ReviewService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("review/get-all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] ReviewFilterParams filterParams)
        {
            try
            {
                var result = await Service.ReviewService.GetAllAsync(filterParams);
                return Ok(result.ToPagedListData());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("review/get-all-with-grade")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllWithAverageGrade([FromQuery] ReviewFilterParams filterParams)
        {
            try
            {
                var result = await Service.ReviewService.GetWithAverageGrade(filterParams);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("review/get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var result = await Service.ReviewService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("review/update/{id}")]
        public async Task<IActionResult> Update(long id, ReviewUpdateRequest review)
        {
            try
            {
                var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                var resultClaims = await Service.UserService.GetUserClaimsAsync(jwt);

                if (resultClaims != null)
                {
                    var reviewUpdateModel = Mapper.Map<ReviewUpdateModel>(review);
                    reviewUpdateModel.AccountId = long.Parse(resultClaims.Id); 
                    var result = await Service.ReviewService.UpdateAsync(id, reviewUpdateModel);
                    return Ok(result);
                }
                return BadRequest("Authorization problem");
                    
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
