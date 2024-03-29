﻿using RentWheels.Core.VeiwModels.Engine;
using System.ComponentModel.DataAnnotations;
using static RentWheels.Infrastructure.Constants.DataConstants.CarConstants;
using static RentWheels.Core.Constants.MessageConstants;
using RentWheels.Core.VeiwModels.Category;

namespace RentWheels.Core.VeiwModels.Car
{
	public class CarFormViewModel
	{
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(CarMaxBrandLength, MinimumLength = CarMinBrandLength
            , ErrorMessage = StringLengthMessage)]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(CarMaxModelLength, MinimumLength = CarMinModelLength
            , ErrorMessage = StringLengthMessage)]
        public string CarModel { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(CarMaxColorLength, MinimumLength = CarMinColorLength
            , ErrorMessage = StringLengthMessage)]
        public string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Range(CarMinYear, CarMaxYear
            , ErrorMessage = RangeMessage)]
        public int Year { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [Range(CarMinPricePerDay, CarMaxPricePerDay
            , ErrorMessage = RangeMessage)]
        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        public int EngineId { get; set; }

        public IEnumerable<EngineFormViewModel> Engines { get; set; } = new List<EngineFormViewModel>();

        [Required(ErrorMessage = RequiredMessage)]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
