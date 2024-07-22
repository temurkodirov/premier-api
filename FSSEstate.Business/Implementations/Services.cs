using AutoMapper;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Business.Interfaces.Helpers;
using FSSEstate.Business.Interfaces;
using FSSEstate.Repository.Context;
using FSSEstate.Repository.Implementations;
using FSSEstate.Repository.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FSSEstate.Business.Implementations
{
    public class Services : IService
    {
        private readonly AppDbContext _databaseContext;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ISmsHelper _smsHelper;
        private IJwtUtils _jwtUtils;
        private IHttpContextAccessor _httpContextAccessor;
        private IAccountService _accountService;
        private IUserService _userService;
        private IProjectService _projectService;
        private IAgentService _agentService;
        private ICategoryService _categoryService;
        private IProjectPhotoService _projectPhotoService;
        private IFileService _fileService;
        private IAffairService _affairService;
        private IFavouriteProjectService _favouriteProjectService;
        private IReviewService _reviewService;
        private IInformationService _informationService;
        private IInformationPhotoService _informationPhotoService;
        private IxProductService _xProductService;
        private IxProductImageService _xProductImageService;
        private IxProductCharacteristicsService _xProductCharacteristicsService;
        public Services(AppDbContext databaseContext, IMapper mapper, ISmsHelper smsHelper, IJwtUtils jwtUtils, IFileService fileService)
        {
            _databaseContext = databaseContext;
            _unitOfWork = new UnitOfWork(_databaseContext);
            _mapper = mapper;
            _smsHelper = smsHelper;
            _jwtUtils = jwtUtils;
            _fileService = fileService;
        }

        public IAccountService AccountService => _accountService ?? (_accountService = new AccountService(_unitOfWork, this, _smsHelper, _jwtUtils, _mapper, _fileService));
        public IUserService UserService => _userService ?? (_userService = new UserService(_unitOfWork, this, _jwtUtils, _httpContextAccessor, _mapper, _fileService));
        public IProjectService ProjectService => _projectService ?? (_projectService = new ProjectService(_unitOfWork, this, _jwtUtils, _mapper, _fileService));
        public IxProductService xProductService => _xProductService ?? (_xProductService = new xProductService(_unitOfWork, this, _jwtUtils, _mapper, _fileService, _xProductCharacteristicsService));
        public IAgentService AgentService => _agentService ?? (_agentService = new AgentService(_unitOfWork, this, _jwtUtils, _mapper, _fileService));
        public ICategoryService CategoryService => _categoryService ?? (_categoryService = new CategoryService(_unitOfWork, this, _jwtUtils, _mapper, _fileService));
        public IProjectPhotoService ProjectPhotoService => _projectPhotoService ?? (_projectPhotoService = new ProjectPhotoService(_unitOfWork, this, _jwtUtils, _mapper, _fileService));
        public IxProductImageService xProductImageService => _xProductImageService ?? (_xProductImageService = new xProductImageService(_unitOfWork, this, _jwtUtils, _mapper, _fileService));
        public IAffairService AffairService => _affairService ?? (_affairService = new AffairService(_unitOfWork, this, _jwtUtils, _mapper, _fileService));
        public IFavouriteProjectService FavouriteProjectService => _favouriteProjectService ?? (_favouriteProjectService = new FavouriteProjectService(_unitOfWork, this, _jwtUtils, _mapper, _fileService));
        public IReviewService ReviewService => _reviewService ?? (_reviewService = new ReviewService(_unitOfWork, this, _jwtUtils, _mapper, _fileService));
        public IInformationService InformationService => _informationService ?? (_informationService = new InformationService(_unitOfWork, this, _jwtUtils, _mapper, _fileService));
        public IInformationPhotoService InformationPhotoService => _informationPhotoService ?? (_informationPhotoService = new InformationPhotoService(_unitOfWork,this, _jwtUtils, _mapper, _fileService));
        public IxProductCharacteristicsService xProductCharacteristicsService => _xProductCharacteristicsService ?? (_xProductCharacteristicsService = new xProductCharacteristicsService (_unitOfWork, this, _jwtUtils,_mapper, _fileService));
    }
}
