using System.ComponentModel.DataAnnotations;
using static RentWheels.Core.Constants.MessageConstants;
using static RentWheels.Infrastructure.Constants.DataConstants.RentalConstants;

namespace RentWheels.Core.ViewModels.Rental
{
    public class RentCarFormViewModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Start Date")]
        public string Start { get; set; } = string.Empty;
        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "End Date")]
        public string End { get; set; } = string.Empty;
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(RentalMaxPickUpLocationLength, MinimumLength = RentalMinPickUpLocationLength
            , ErrorMessage = StringLengthMessage)]
        [Display(Name = "Pick Up")]
        public string PickUp { get; set; } = string.Empty;
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(RentalMaxDropOffLocationLength, MinimumLength = RentalMinDropOffLocationLength
            , ErrorMessage = StringLengthMessage)]
        [Display(Name = "Drop Off")]
        public string DropOff { get; set; } = string.Empty;
    }
}
