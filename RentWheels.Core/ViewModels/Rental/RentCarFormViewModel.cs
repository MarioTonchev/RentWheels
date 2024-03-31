using System.ComponentModel.DataAnnotations;
using static RentWheels.Core.Constants.MessageConstants;
using static RentWheels.Infrastructure.Constants.DataConstants.RentalConstants;

namespace RentWheels.Core.ViewModels.Rental
{
    public class RentCarFormViewModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        public string Start { get; set; } = string.Empty;
        [Required(ErrorMessage = RequiredMessage)]
        public string End { get; set; } = string.Empty;
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(RentalMaxPickUpLocationLength, MinimumLength = RentalMinPickUpLocationLength
            , ErrorMessage = StringLengthMessage)]
        public string PickUp { get; set; } = string.Empty;
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(RentalMaxDropOffLocationLength, MinimumLength = RentalMinDropOffLocationLength
            , ErrorMessage = StringLengthMessage)]
        public string DropOff { get; set; } = string.Empty;
    }
}
