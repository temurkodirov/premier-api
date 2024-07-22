using AutoMapper;
using FSSEstate.API.Authorization;
using FSSEstate.API.Models.AgentModels;
using FSSEstate.Business.Interfaces;
using FSSEstate.Core.Models.AccountModels;
using FSSEstate.Core.Models.Agents;
using FSSEstate.Repository.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FSSEstate.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class AgentController : BaseController
    {
        public AgentController(IService service, IMapper mapper) : base(service, mapper)
        {
        }

        [HttpPost("agent/create")]
        public async Task<IActionResult> Create(AgentCreateRequest agent)
        {
            try
            {
                var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                var resultClaims = await Service.UserService.GetUserClaimsAsync(jwt);

                if (resultClaims != null)
                {
                    var agentModel = Mapper.Map<AgentCreateModel>(agent);
                    agentModel.AccountId = long.Parse(resultClaims.Id);
                    var result = await Service.AgentService.CreateAsync(agentModel);
                    if (result)
                    {
                        var accountModel = new AccountUpdateModel();
                        accountModel.Id = agentModel.AccountId;
                        accountModel.AccountType = Core.Enums.AccountType.Master;
                        await Service.AccountService.UpdateAsync(accountModel);                        
                    }
                    return Ok(result);
                }
                return BadRequest("Authorization problem");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("agent/delete")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var result = await Service.AgentService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("agent/get-all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] AgentFilterParams filterParams)
        {
            try
            {
                var result = await Service.AgentService.GetAllAsync(filterParams);
                return Ok(result.ToPagedListData());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("agent/get-all-with-affairs")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllWithAffairs([FromQuery] AgentAffairFilterParams filterParams)
        {
            try
            {
                var result = await Service.AgentService.GetAllWithAffair(filterParams);
                return Ok(result.ToPagedListData());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("agent/get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var result = await Service.AgentService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("agent/get-by-accountId/{accountId}")]
        public async Task<IActionResult> GetbyAccountId(long accountId)
        {
            try
            {
                var result = await Service.AgentService.GetByAccountIdAsync(accountId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("agent/update")]
        public async Task<IActionResult> Update(AgentUpdateRequest agent)
        {
            try
            {
                var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                var resultClaims = await Service.UserService.GetUserClaimsAsync(jwt);
                if(resultClaims is not null)
                {
                    
                    var agentModel = await Service.AgentService.GetByAccountIdAsync(long.Parse(resultClaims.Id));
                    if(agentModel.Id == agent.Id)
                    {
                        var agentUpdateModel = Mapper.Map<AgentUpdateModel>(agent);
                        var result = await Service.AgentService.UpdateAsync(agentUpdateModel);
                        return Ok(result);
                    }
                    else
                        return BadRequest("You're not allowed to update this account");
                }
                return BadRequest("You're not allowed to update this account");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("agents/top-list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] AgentFilterParams filterParams)
        {
            try
            {
                var result = await Service.AgentService.GetAllAsync(filterParams);
                return Ok(result.ToPagedListData());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
