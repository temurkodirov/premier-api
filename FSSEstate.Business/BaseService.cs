using AutoMapper;
using FSSEstate.Business.Interfaces;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Repository.Interfaces;

namespace FSSEstate.Business
{
    public abstract class BaseService
    {
        protected IUnitOfWork UnitOfWork { get; }
        protected IService Service { get; }
        protected IJwtUtils JwtUtils { get; }
        protected IMapper Mapper { get; }
        protected IFileService FileService { get; }

        protected BaseService(IUnitOfWork unitOfWork, IService service, IJwtUtils jwtUtils, IMapper mapper,
            IFileService fileService)
        {
            UnitOfWork = unitOfWork;
            Service = service;
            JwtUtils = jwtUtils;
            Mapper = mapper;
            FileService = fileService;
        }
    }
}
