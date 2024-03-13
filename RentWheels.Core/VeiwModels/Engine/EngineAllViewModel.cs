using System.ComponentModel.DataAnnotations;
using static RentWheels.Infrastructure.Constants.DataConstants.EngineConstants;
using static RentWheels.Core.Constants.MessageConstants;

namespace RentWheels.Core.VeiwModels.Engine
{
    public class EngineAllViewModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(EngineMaxNameLength, MinimumLength = EngineMinNameLength
            , ErrorMessage = StringLengthMessage)]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = RequiredMessage)]
        [Range(EngineMinHorsepower, EngineMaxHorsepower
            , ErrorMessage = RangeMessage)]
        public int Horsepower { get; set; }
        [Required(ErrorMessage = RequiredMessage)]
        [Range(EngineMinCubage, EngineMaxCubage
            , ErrorMessage = RangeMessage)]
        public int Cubage { get; set; }
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(EngineMaxFuelTypeLength, MinimumLength = EngineMinFuelTypeLength
            , ErrorMessage = StringLengthMessage)]
        public string FuelType { get; set; } = string.Empty;
    }
}
