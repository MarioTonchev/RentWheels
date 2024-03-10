using RentWheels.Core.Contracts;
using RentWheels.Core.VeiwModels.Car;
using RentWheels.Core.VeiwModels.Engine;

namespace RentWheels.Core.Services
{
	public class CarService : ICarService
	{
		public Task<int> AddAsync(CarAddViewModel model)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<CarAllViewModel>> AllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<EngineAllViewModel>> AllEnginesAsyc()
		{
			throw new NotImplementedException();
		}

		public Task<bool> CarExistsAsync(int carId)
		{
			throw new NotImplementedException();
		}

		public Task<CarDetailsViewModel> DetailsAsync(int carId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> EngineExistsAsync(int engineId)
		{
			throw new NotImplementedException();
		}
	}
}