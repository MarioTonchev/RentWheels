using System.ComponentModel.DataAnnotations;
using static RentWheels.Core.Constants.MessageConstants;

namespace RentWheels.Core.VeiwModels.Category
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        public int Id { get; set; }
        [Required(ErrorMessage = RequiredMessage)]
        public string Name { get; set; } = string.Empty;
    }
}
