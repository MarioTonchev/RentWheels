using System.ComponentModel.DataAnnotations;
using static RentWheels.Infrastructure.Constants.DataConstants.ReviewConstants;
using static RentWheels.Core.Constants.MessageConstants;

namespace RentWheels.Core.ViewModels.Review
{
	public class ReviewFormViewModel
	{
        [Required(ErrorMessage = RequiredMessage)]
		[Range(ReviewMinRating, ReviewMaxRating, ErrorMessage = RangeMessage)]
        public int Rating { get; set; }
		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(ReviewMaxCommentLength, MinimumLength = ReviewMinCommentLength,
			ErrorMessage = StringLengthMessage)]
		public string Comment { get; set; } = string.Empty;
		public int CarId { get; set; }
    }
}
