using System.ComponentModel.DataAnnotations;
using static RentWheels.Infrastructure.Constants.DataConstants.MessageEntityConstants;
using static RentWheels.Core.Constants.MessageConstants;
using RentWheels.Core.ViewModels.Rental;

namespace RentWheels.Core.ViewModels.Message
{
	public class MessageFormViewModel
	{
		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(MessageMaxTitleLength, MinimumLength = MessageMinTitleLength,
			ErrorMessage = StringLengthMessage)]
		public string Title { get; set; } = string.Empty;
		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(MessageMaxContentLength, MinimumLength = MessageMinContentLength,
			ErrorMessage = StringLengthMessage)]
		[Display(Name = "Message")]
        public string Content { get; set; } = string.Empty;
		[Required(ErrorMessage = RequiredMessage)]
		public int RentalId { get; set; } 
		public IEnumerable<RentalMessageViewModel> Rentals{ get; set; } = new List<RentalMessageViewModel>();
    }
}
