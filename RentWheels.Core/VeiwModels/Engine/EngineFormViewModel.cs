using System.ComponentModel.DataAnnotations;
using static RentWheels.Core.Constants.MessageConstants;
using static RentWheels.Infrastructure.Constants.DataConstants.EngineConstants;

namespace RentWheels.Core.VeiwModels.Engine
{
	public class EngineFormViewModel
	{
        [Required(ErrorMessage = RequiredMessage)]
        public int Id { get; set; }
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(EngineMaxNameLength, MinimumLength = EngineMinNameLength
            , ErrorMessage = StringLengthMessage)]
        public string Name { get; set; } = string.Empty;
    }
}
