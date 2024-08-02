using AutoMapper;
using FSSEstate.Core.Models.AccountModels;
using FSSEstate.Core.Models.AffairModels;
using FSSEstate.Core.Models.Agents;
using FSSEstate.Core.Models.ConfirmEmailModels;
using FSSEstate.Core.Models.FavouriteProjectModels;
using FSSEstate.Core.Models.InformationModels;
using FSSEstate.Core.Models.InformationPhotosModels;
using FSSEstate.Core.Models.ProjectCategoryModels;
using FSSEstate.Core.Models.ProjectModels;
using FSSEstate.Core.Models.ProjectPhotosModels;
using FSSEstate.Core.Models.ReviewModels;
using FSSEstate.Core.Models.UserModels;
using FSSEstate.Core.Models.xProductCharacteristicsModels;
using FSSEstate.Core.Models.xProductImageModels;
using FSSEstate.Core.Models.xProductModels;
using FSSEstate.Repository.Entities;

namespace FSSEstate.Business.Implementations.Helpers
{
    public class CustomMapper : Profile
    {
        public CustomMapper()
        {
            CreateMap<AccountCreateModel, UserEntity>();
            CreateMap<AccountCreateModel, AccountEntity>();
            CreateMap<AccountUpdateModel, AccountEntity>();
            CreateMap<AccountEntity, UserEntity>();
            CreateMap<UserEntity, User>();
            CreateMap<UserEntity, UserModel>();
            CreateMap<UserCreateModel, UserEntity>();

            CreateMap<ConfirmationEmailEntity, ConfirmEmailModel>();
            CreateMap<ConfirmEmailCreateModel, ConfirmationEmailEntity>();

            CreateMap<ProjectCreateModel, ProjectEntity>();
            CreateMap<ProjectEntity, ProjectModel>();
            CreateMap<ProjectUpdateModel, ProjectEntity>();

            CreateMap<xProductCreateModel, xProduct>();
            CreateMap<xProduct, xProductModel>();

            CreateMap<AgentCreateModel, AgentEntity>();
            CreateMap<AgentEntity, AgentModel>();
            CreateMap<AgentUpdateModel, AgentEntity>();
            CreateMap<AgentAffairEntity, AgentAffairModel>();

            CreateMap<CategoryCreateModel, CategoryEntity>();
            CreateMap<CategoryEntity, CategoryModel>();
            CreateMap<CategoryUpdateModel, CategoryEntity>();

            CreateMap<ProjectPhotoCreateModel, ProjectPhotosEntity>();
            CreateMap<ProjectPhotosEntity, ProjectPhotoModel>();
            CreateMap<ProjectPhotoUpdateModel, ProjectPhotosEntity>();

            CreateMap<xProductImageCreateModel, xProductImage>();
            CreateMap<xProductImage, xProductImageModel>();
            CreateMap<xProductUpdateModel, xProductImageModel>();
            CreateMap<xProductCharacteristicsCreateModel, xProductCharacteristics>();

            CreateMap<AffairCreateModel, AffairEntity>();
            CreateMap<AffairEntity, AffairModel>();
            CreateMap<AffairUpdateModel, AffairEntity>();

            CreateMap<FavouriteProjectCreateModel, FavouriteProjectEntity>();
            CreateMap<FavouriteProjectEntity, FavouriteProjectModel>();

            CreateMap<ReviewCreateModel, ReviewEntity>();
            CreateMap<ReviewEntity, ReviewModel>();
            CreateMap<ReviewUpdateModel, ReviewEntity>();

            CreateMap<InformationCreateModel, InformationEntity>();
            CreateMap<InformationEntity, InformationModel>();
            CreateMap<InformationUpdateModel, InformationEntity>();

            CreateMap<InformationPhotoCreateModel, InformationPhotosEntity>();
            CreateMap<InformationPhotosEntity, InformationPhotoModel>();
            CreateMap<InformationPhotoUpdateModel, InformationPhotosEntity>();

        }
    }
}
