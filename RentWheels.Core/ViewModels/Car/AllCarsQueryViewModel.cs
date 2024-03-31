using RentWheels.Core.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace RentWheels.Core.ViewModels.Car
{
	public class AllCarsQueryViewModel
	{
		public int CarsPerPage { get; set; } = 4;
        public string Category { get; set; } = null!;
        [Display(Name = "Search")]
        public string SearchTerm { get; set; } = null!;
        [Display(Name = "Sort by")]
        public CarSorting Sorting { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalCarsCount { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<string> Categories { get; set; } = new List<string>();
        public IEnumerable<CarAllViewModel> Cars { get; set; } = new List<CarAllViewModel>();
    }
}