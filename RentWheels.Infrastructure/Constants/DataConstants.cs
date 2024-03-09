using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentWheels.Infrastructure.Constants
{
	public static class DataConstants
	{
		//Car
		public const int CarMinBrandLength = 2;
		public const int CarMaxBrandLength = 30;

		public const int CarMinModelLength = 1;
		public const int CarMaxModelLength = 30;

		public const int CarMinColorLength = 3;
		public const int CarMaxColorLength = 25;

		public const double CarMinPricePerDay = 0.00;
		public const double CarMaxPricePerDay = 2000.00;
	}
}
