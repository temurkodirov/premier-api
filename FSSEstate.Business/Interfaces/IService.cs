namespace FSSEstate.Business.Interfaces
{
    public interface IService
    {
        IAccountService AccountService { get; }
        IUserService UserService { get; }
        IProjectService ProjectService { get; }
        IAgentService AgentService { get; }
        ICategoryService CategoryService { get; }
        IProjectPhotoService ProjectPhotoService { get; }
        IAffairService AffairService { get; }
        IFavouriteProjectService FavouriteProjectService { get; }
        IReviewService ReviewService { get; }
        IInformationService InformationService { get; }
        IInformationPhotoService InformationPhotoService { get; }
        IxProductService xProductService { get; }
        IxProductImageService xProductImageService { get; }
        IxProductCharacteristicsService xProductCharacteristicsService { get; }
    }
}
