using FSSEstate.Repository.Context;
using FSSEstate.Repository.Implementations.Repositories;
using FSSEstate.Repository.Interfaces.Repositories;
using FSSEstate.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FSSEstate.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IAccountRepository _accountRepository;
        private IUserRepository _userRepository;
        private IConfirmationEmailRepository _confirmationEmailRepository;
        private IProjectRepository _projectRepository;
        private IAgentRepository _agentRepository;
        private ICategoryRepository _categoryRepository;
        private IProjectPhotosRepository _projectPhotosRepository;
        private IAffairRepository _affairRepository;
        private IFavouriteProjectRepository _favouriteProjectRepository;
        private IReviewRepository _reviewRepository;
        private IInformationRepository _informationRepository;
        private IInformationPhotosRepository _informationPhotosRepository;
        private IAgentAffairRepository _agentAffairRepository;
        private IxProductImageRepository _xProductImageRepository;
        private IxProductRepository _xProductRepository;
        private IxProductCharacteristicsRepository _xProductCharacteristicsRepository;
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IAccountRepository AccountRepository => _accountRepository ?? (_accountRepository = new AccountRepository(_dbContext));
        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_dbContext));
        public IConfirmationEmailRepository ConfirmationEmailRepository => _confirmationEmailRepository ?? (_confirmationEmailRepository = new ConfirmationEmailRepository(_dbContext));
        public IProjectRepository ProjectRepository => _projectRepository ?? (_projectRepository= new ProjectRepository(_dbContext));
        public IAgentRepository AgentRepository => _agentRepository ?? (_agentRepository= new AgentRepository(_dbContext));
        public ICategoryRepository CategoryRepository => _categoryRepository ?? (_categoryRepository = new CategoryRepository(_dbContext));
        public IProjectPhotosRepository ProjectPhotosRepository => _projectPhotosRepository ?? (_projectPhotosRepository = new ProjectPhotosRepository(_dbContext));
        public IAffairRepository AffairRepository => _affairRepository ?? (_affairRepository = new AffairRepository(_dbContext));        public IFavouriteProjectRepository FavouriteProjectRepository => _favouriteProjectRepository ??(_favouriteProjectRepository = new FavouriteProjectRepository(_dbContext));        public IReviewRepository ReviewRepository => _reviewRepository ?? (_reviewRepository = new ReviewRepository(_dbContext));
        public IInformationRepository InformationRepository => _informationRepository ?? (_informationRepository= new InformationRepository(_dbContext));
        public IInformationPhotosRepository InformationPhotosRepository => _informationPhotosRepository ?? (_informationPhotosRepository = new InformationPhotosRepository(_dbContext)); 
        public IAgentAffairRepository AgentAffairRepository => _agentAffairRepository ?? (_agentAffairRepository= new AgentAffairRepository(_dbContext));
        public IxProductRepository XProductRepository => _xProductRepository ?? (_xProductRepository = new xProductRepository(_dbContext));
      
        public IxProductImageRepository XProductImageRepository => _xProductImageRepository ?? (_xProductImageRepository = new xProductImageRepository(_dbContext));
        public IxProductCharacteristicsRepository XProductCharacteristicsRepository => _xProductCharacteristicsRepository ?? (_xProductCharacteristicsRepository = new xProductCharacteristicsRepository(_dbContext));


        public async Task CommitAsync()
            => await _dbContext.SaveChangesAsync();

        public async Task RollbackAsync()
            => await _dbContext.DisposeAsync();
    }
}
