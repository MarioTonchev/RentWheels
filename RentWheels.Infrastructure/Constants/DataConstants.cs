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
		}

		public static class ReviewConstants
		{
			public const int ReviewMinCommentLength = 15;
			public const int ReviewMaxCommentLength = 1500;

			public const int ReviewMinRating = 1;
			public const int ReviewMaxRating = 10;
		}

		public const string DateFormat = "dd/MM/yyyy";
	}
}
