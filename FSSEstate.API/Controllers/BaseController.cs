using AutoMapper;
using FSSEstate.API.Authorization;
using FSSEstate.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FSSEstate.API.Controllers
{
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        public BaseController(IService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }
        public IService Service { get; private set; }
        public IMapper Mapper { get; private set; }
    }
}
