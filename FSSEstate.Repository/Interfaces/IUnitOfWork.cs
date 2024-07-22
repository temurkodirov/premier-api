using FSSEstate.Repository.Interfaces.Repositories;

namespace FSSEstate.Repository.Interfaces;

public interface IUnitOfWork
{
    IAccountRepository AccountRepository { get; }
    IUserRepository UserRepository { get; }
    IConfirmationEmailRepository ConfirmationEmailRepository { get; }
    IProjectRepository ProjectRepository { get; }
    IAgentRepository AgentRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IProjectPhotosRepository ProjectPhotosRepository { get; }
    IAffairRepository AffairRepository { get; }
    IFavouriteProjectRepository FavouriteProjectRepository { get; }
    IReviewRepository ReviewRepository { get; }
    IInformationRepository InformationRepository { get; }
    IInformationPhotosRepository InformationPhotosRepository { get; }
    IAgentAffairRepository AgentAffairRepository { get; }
    IxProductImageRepository XProductImageRepository { get; }
    IxProductRepository XProductRepository { get; }
    IxProductCharacteristicsRepository XProductCharacteristicsRepository { get; }

    Task CommitAsync();
    Task RollbackAsync();

}
