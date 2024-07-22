using AutoMapper;
using FSSEstate.API.Authorization;
using FSSEstate.API.Models.AccountModels;
using FSSEstate.Business.Interfaces;
using FSSEstate.Core.Models.AccountModels;
using Microsoft.AspNetCore.Mvc;

namespace FSSEstate.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(IService service, IMapper mapper) : base(service, mapper)
        {
        }

        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyAccountAsync(string emailOrPhoneNumber, string code)
        {
            try
            {
                var result = await Service.AccountService.VerifyAccountAsync(emailOrPhoneNumber, code);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(AccountCreateRequest accountRequest)
        {
            try
            {
                var account = Mapper.Map<AccountCreateModel>(accountRequest);
                var result = await Service.AccountService.CreateAsync(account);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword(PasswordUpdateRequest passwordUpdateRequest)
        {
            var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            var resultClaims = await Service.UserService.GetUserClaimsAsync(jwt);

            var passwordUpdateModel = Mapper.Map<PasswordUpdateModel>(passwordUpdateRequest);
            var result = await Service.AccountService.UpdatePassword(passwordUpdateModel, resultClaims.Id);
            
            return Ok(result);
        }
    }
}
