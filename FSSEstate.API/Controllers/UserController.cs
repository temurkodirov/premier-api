using AutoMapper;
using FSSEstate.API.Authorization;
using FSSEstate.API.Models.UserModels;
using FSSEstate.Business.Interfaces;
using FSSEstate.Core.Models.UserModels;
using FSSEstate.Repository.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace FSSEstate.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : BaseController
    {
        public UserController(IService service, IMapper mapper) : base(service, mapper)
        {
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(UserLoginRequest userLoginRequest)
        {
            var userLoginResult = Mapper.Map<UserLogin>(userLoginRequest);
            var token = await Service.UserService.LoginAsync(userLoginResult);
            return Ok(token);
        }

        [HttpPost("update-profile")]
        public async Task<IActionResult> UpdateProfile(UserUpdateOrCreateRequest user)
        {
            var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            var resultClaims = await Service.UserService.GetUserClaimsAsync(jwt);

            if(resultClaims.Email == user.Email || resultClaims.PhoneNumber == user.PhoneNumber) // email va telefon raqam bilan alohida register qilsa muammo bor!
            {
                var userCreateModel = Mapper.Map<UserCreateModel>(user);
                var result = await Service.UserService.CreateAsync(userCreateModel);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var user = await Service.UserService.GetByIdAsync(id);
            var response = Mapper.Map<UserResponse>(user);
            return Ok(response);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var userlist = await Service.UserService.GetAllAsync();
            var response = Mapper.Map<IEnumerable<UserResponse>>(userlist);
            return Ok(response);
        }

        [HttpGet("verify")]
        public async Task<UserClaims> GetUserClaimsAsync()
        {
            var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            var resultClaims = await Service.UserService.GetUserClaimsAsync(jwt);
            return Mapper.Map<UserClaims>(resultClaims);
        }
    }
}
