using RentWheels.Core.ViewModels.Review;

namespace RentWheels.Core.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewAllViewModel>> GetForCarAsync(int id);
        Task AddAsync(ReviewFormViewModel model, string reviewerId);
    }
}
