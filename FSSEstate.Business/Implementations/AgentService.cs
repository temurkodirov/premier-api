using AutoMapper;
using AutoMapper.QueryableExtensions;
using FSSEstate.Business.Interfaces;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Core.Models.AffairModels;
using FSSEstate.Core.Models.Agents;
using FSSEstate.Core.Utility.Listing;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FSSEstate.Business.Implementations
{
    public class AgentService : BaseService, IAgentService
    {
        public AgentService(IUnitOfWork unitOfWork, IService service, IJwtUtils jwtUtils, IMapper mapper, IFileService fileService) 
            : base(unitOfWork, service, jwtUtils, mapper, fileService)
        {
        }

        public async Task<bool> CreateAsync(AgentCreateModel agent)
        {
            var existAgents = await UnitOfWork.AgentRepository.GetAllByQueryAsync(item=>item.AccountId == agent.AccountId);
            if (existAgents.Any())
                throw new Exception($"This account already Master. Account id: {agent.AccountId}");

            var agentEntity = Mapper.Map<AgentEntity>(agent);
            await UnitOfWork.AgentRepository.AddAsync(agentEntity);
            await UnitOfWork.CommitAsync();

            var agentAffairs = new List<AgentAffairEntity>();

            if(agent.AffairIds.Count() > 0)
            {
                foreach (var item in agent.AffairIds)
                {
                    var agentAffairEntity = new AgentAffairEntity();
                    agentAffairEntity.AffairId = item;
                    agentAffairEntity.AgentId = agentEntity.Id;
                    agentAffairs.Add(agentAffairEntity);
                }

                await UnitOfWork.AgentAffairRepository.AddRangeAsync(agentAffairs);
                await UnitOfWork.CommitAsync();
            }       
             
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var agent = await UnitOfWork.AgentRepository.GetAsync(item => item.Id == id);
            if (agent is null) throw new Exception("Agent not found!");

            UnitOfWork.AgentRepository.Remove(agent);
            await UnitOfWork.CommitAsync();

            return true;
        }

        public async Task<PagedList<AgentModel>> GetAllAsync(AgentFilterParams filterParams)
        {
            var entityItems = await UnitOfWork.AgentRepository.GetAllByQueryAsync(item =>
               (filterParams.SearchText == string.Empty || item.FullName.Contains(filterParams.SearchText)),
                null, x => x.CreatedAt,
               filterParams.Order == "desc");
            var items = entityItems.ProjectTo<AgentModel>(Mapper.ConfigurationProvider);
            PagedList<AgentModel> pagedList = PagedList<AgentModel>.ToPagedListFromQuery(
                filterParams.PageNumber,
                filterParams.PageSize,
                filterParams.Order,
                items);

            return pagedList;
        }

        public async Task<PagedList<AgentAffairModel>> GetAllWithAffair(AgentAffairFilterParams filterParams)
        {
            var entityItems = await UnitOfWork.AgentAffairRepository.GetAllByQueryAsync(item =>
            (filterParams.SearchText.IsNullOrEmpty() || item.Agent.FullName.ToLower().Contains(filterParams.SearchText.ToLower()))&&
            (filterParams.AffairId == null || item.AffairId == filterParams.AffairId),
            query => query.Include(item=>item.Agent), x => x.CreatedAt,
               filterParams.Order == "desc");
            var items = entityItems.ProjectTo<AgentAffairModel>(Mapper.ConfigurationProvider);
            PagedList<AgentAffairModel> pagedList = PagedList<AgentAffairModel>.ToPagedListFromQuery(
                filterParams.PageNumber,
                filterParams.PageSize,
                filterParams.Order,
                items);

            return pagedList;
        }

        public async Task<AgentModel> GetByAccountIdAsync(long accountId)
        {
            var agentEntity = await UnitOfWork.AgentRepository.GetAsync(item => item.AccountId == accountId);
            if (agentEntity is null) throw new Exception("Agent not found");

            var agent = Mapper.Map<AgentModel>(agentEntity);

            var agentAffairs = await UnitOfWork.AgentAffairRepository.GetAllByQueryAsync(item => item.AgentId == agent.Id);

            foreach (var affair in agentAffairs)
            {
                var affairEntity = await UnitOfWork.AffairRepository.GetAsync(item => item.Id == affair.AffairId);
                if (affairEntity is not null)
                {
                    if (agent.Affairs is null)
                        agent.Affairs = new List<AffairModel>();
                    agent.Affairs.Add(Mapper.Map<AffairModel>(affairEntity));
                }
            }

            return agent;
        }

        public async Task<AgentModel> GetByIdAsync(long id)
        {
            var agentEntity = await UnitOfWork.AgentRepository.GetAsync(item => item.Id == id);
            if (agentEntity is null) throw new Exception("Agent not found!");

            var agent = Mapper.Map<AgentModel>(agentEntity);

            var agentAffairs = await UnitOfWork.AgentAffairRepository.GetAllByQueryAsync(item=>item.AgentId == id);          

            foreach ( var affair in agentAffairs)
            {
                var affairEntity = await UnitOfWork.AffairRepository.GetAsync(item => item.Id == affair.AffairId);
                if (affairEntity is not null)
                {
                    if (agent.Affairs is null)
                        agent.Affairs = new List<AffairModel>();
                    agent.Affairs.Add(Mapper.Map<AffairModel>(affairEntity));
                }
            }

            return agent;
        }

        public async Task<bool> UpdateAsync(AgentUpdateModel agent)
        {
            var agentEntity = await UnitOfWork.AgentRepository.GetAsync(item => item.Id == agent.Id);
            if (agentEntity is null) throw new Exception("Agent not found!");

            var agentAffairs = new List<AgentAffairEntity>();

            if (agent.AffairIds.Count() > 0)
            {
                foreach (var item in agent.AffairIds)
                {
                    var agentAffairEntity = new AgentAffairEntity();
                    agentAffairEntity.AffairId = item;
                    agentAffairEntity.AgentId = agentEntity.Id;
                    agentAffairs.Add(agentAffairEntity);
                }
                var agentServices = await UnitOfWork.AgentAffairRepository.GetAllByQueryAsync(item => item.AgentId == agentEntity.Id); ;

                if(agentServices.Count() > 0)
                    UnitOfWork.AgentAffairRepository.RemoveRange(agentServices);

                await UnitOfWork.AgentAffairRepository.AddRangeAsync(agentAffairs);
                await UnitOfWork.CommitAsync();
            }
            Mapper.Map(agent, agentEntity);
            UnitOfWork.AgentRepository.Update(agentEntity);
            await UnitOfWork.CommitAsync();
            return true;
        }
    }
}
