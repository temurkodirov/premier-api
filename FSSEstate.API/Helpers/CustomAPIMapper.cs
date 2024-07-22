using AutoMapper;
using FSSEstate.API.Models.AccountModels;
using FSSEstate.API.Models.AffairModels;
using FSSEstate.API.Models.AgentModels;
using FSSEstate.API.Models.FavouriteProjectModels;
using FSSEstate.API.Models.InformationModels;
using FSSEstate.API.Models.ProjectCategoryModels;
using FSSEstate.API.Models.ProjectModels;
using FSSEstate.API.Models.ReviewModels;
using FSSEstate.API.Models.UserModels;
using FSSEstate.API.Models.xProductModels;
using FSSEstate.Core.Models.AccountModels;
using FSSEstate.Core.Models.AffairModels;
using FSSEstate.Core.Models.Agents;
using FSSEstate.Core.Models.FavouriteProjectModels;
using FSSEstate.Core.Models.InformationModels;
using FSSEstate.Core.Models.ProjectCategoryModels;
using FSSEstate.Core.Models.ProjectModels;
using FSSEstate.Core.Models.ReviewModels;
using FSSEstate.Core.Models.UserModels;
using FSSEstate.Core.Models.xProductModels;
using FSSEstate.Repository.Entities;

namespace FSSEstate.API.Helpers
{
    public class CustomAPIMapper : Profile
    {
        public CustomAPIMapper() 
        {
            CreateMap<AccountCreateRequest, AccountCreateModel>();
            CreateMap<UserLoginRequest, UserLogin>();
            CreateMap<UserModel, UserResponse>();
            CreateMap<UserUpdateOrCreateRequest, UserCreateModel>();
            CreateMap<ProjectCreateRequest, ProjectCreateModel>();
            CreateMap<ProjectUpdateRequest, ProjectUpdateModel>();

            CreateMap<AgentCreateRequest, AgentCreateModel>();
            CreateMap<AgentUpdateRequest, AgentUpdateModel>();
            CreateMap<AgentModel, AgentResponse>();

            CreateMap<UserClaimModel, UserClaims>();

            CreateMap<CategoryCreateRequest, CategoryCreateModel>();
            CreateMap<CategoryUpdateRequest, CategoryUpdateModel>();
            CreateMap<CategoryEntity, CategoryModel>();
            CreateMap<CategoryModel, CategoryResponse>();

            CreateMap<PasswordUpdateRequest, PasswordUpdateModel>();

            CreateMap<AffairCreateRequest, AffairCreateModel>();
            CreateMap<AffairUpdateRequest, AffairUpdateModel>();
            CreateMap<AffairModel, AffairResponse>();

            CreateMap<FavouriteProjectCreateRequest, FavouriteProjectCreateModel>();

            CreateMap<ReviewCreateRequest, ReviewCreateModel>();
            CreateMap<ReviewUpdateRequest, ReviewUpdateModel>();

            CreateMap<InformationCreateRequest, InformationCreateModel>();
            CreateMap<InformationUpdateRequest, InformationUpdateModel>();

            CreateMap<xProductCreateRequest, xProductCreateModel> ();
        }
    }
}
