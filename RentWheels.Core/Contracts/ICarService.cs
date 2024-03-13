using RentWheels.Core.VeiwModels.Car;
using RentWheels.Core.VeiwModels.Engine;

namespace RentWheels.Core.Contracts
{
	public interface ICarService
	{
		Task<IEnumerable<CarAllViewModel>> AllCarsAsync();
		Task<IEnumerable<CarAllViewModel>> LastThreeCarsAsync();
		Task<IEnumerable<EngineAllViewModel>> AllEnginesAsync();
		Task<IEnumerable<EngineFormViewModel>> AllEnginesFormAsync();
		Task<bool> CarExistsAsync(int carId);
		Task<bool> EngineExistsAsync(int engineId);
		Task<int> CreateAsync(CarAddViewModel model, string ownerId);
		Task<CarDetailsViewModel> DetailsAsync(int carId);
	}
}