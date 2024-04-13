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
		[Display(Name = "Pick Up")]
		public string PickUp { get; set; } = string.Empty;
		[Display(Name = "Drop Off")]
		public string DropOff { get; set; } = string.Empty;
        public List<string> Cities { get; set; } = new List<string>()
		{
			"Sofia",
			"Varna",
			"Plovdiv",
			"Burgas",
			"Ruse",
			"Stara Zagora",
			"Pleven",
			"Sliven",
			"Pernik",
			"Pazardzhik",
			"Dobrich",
			"Shumen",
			"Haskovo",
			"Veliko Tarnovo",
			"Blagoevgrad",
			"Yambol",
			"Kazanlak",
			"Asenovgrad",
			"Kyustendil",
			"Vratsa",
			"Gabrovo",
			"Targovishte",
			"Kardzhali",
			"Vidin",
			"Razgrad",
			"Svishtov",
			"Silistra",
			"Dupnitsa",
			"Montana",
			"Lovech",
			"Dimitrovgrad",
			"Smolyan"
		}; 
    }
}
