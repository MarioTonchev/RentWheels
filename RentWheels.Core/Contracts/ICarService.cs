using RentWheels.Core.VeiwModels.Car;
using RentWheels.Core.VeiwModels.Engine;

namespace RentWheels.Core.Contracts
{
	public interface ICarService
	{
		Task<IEnumerable<CarAllViewModel>> AllAsync();
		Task<IEnumerable<EngineAllViewModel>> AllEnginesAsyc();
		Task<bool> CarExistsAsync(int carId);
		Task<bool> EngineExistsAsync(int engineId);
		Task<int> AddAsync(CarAddViewModel model);
		Task<CarDetailsViewModel> DetailsAsync(int carId);
	}
}