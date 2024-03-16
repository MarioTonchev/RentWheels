using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentWheels.Infrastructure.Constants
{
	public static class DataConstants
	{
		public static class CarConstants
		{
			public const int CarMinBrandLength = 2;
			public const int CarMaxBrandLength = 30;

			public const int CarMinModelLength = 1;
			public const int CarMaxModelLength = 30;

			public const int CarMinColorLength = 3;
			public const int CarMaxColorLength = 25;

			public const double CarMinPricePerDay = 0.00;
			public const double CarMaxPricePerDay = 2000.00;

			public const int CarMinYear = 2000;
			public const int CarMaxYear = 2024;
		}
		
		public static class EngineConstants
		{
			public const int EngineMinNameLength = 3;
			public const int EngineMaxNameLength = 20;

			public const double EngineMinHorsepower = 0.00;
			public const double EngineMaxHorsepower = 1000.00;

			public const double EngineMinCubage = 0.00;
			public const double EngineMaxCubage = 6000.00;

			public const int EngineMinFuelTypeLength = 3;
			public const int EngineMaxFuelTypeLength = 20;
		}

		public static class ReviewConstants
		{
			public const int ReviewMinCommentLength = 15;
			public const int ReviewMaxCommentLength = 1500;

			public const int ReviewMinRating = 1;
			public const int ReviewMaxRating = 10;
		}

		public static class RentalConstants
		{
			public const int RentalMinPickUpLocationLength = 4;
			public const int RentalMaxPickUpLocationLength = 30;
			
			public const int RentalMinDropOffLocationLength = 4;
			public const int RentalMaxDropOffLocationLength = 30;
		}

		public const string DateFormat = "dd/MM/yyyy";
	}
}
