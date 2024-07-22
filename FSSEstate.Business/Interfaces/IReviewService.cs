using FSSEstate.Core.Models.ReviewModels;
using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Business.Interfaces
{
    public interface IReviewService
    {
        public Task<bool> CreateAsync(ReviewCreateModel review);
        public Task<PagedList<ReviewModel>> GetAllAsync(ReviewFilterParams filterParams);
        public Task<bool> UpdateAsync(long id, ReviewUpdateModel review);
        public Task<bool> DeleteAsync(long id);
        public Task<ReviewModel> GetByIdAsync(long id);
        public Task<ReviewWithAverageGradeModel> GetWithAverageGrade(ReviewFilterParams filterParams);
    }
}
