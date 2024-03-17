using RentWheels.Core.VeiwModels.Car;
using RentWheels.Core.VeiwModels.Category;
using RentWheels.Core.VeiwModels.Engine;

namespace RentWheels.Core.Contracts
{
	public interface ICarService
	{
		Task<IEnumerable<CarAllViewModel>> AllCarsAsync();

		Task<IEnumerable<EngineAllViewModel>> AllEnginesAsync();

		Task<IEnumerable<EngineFormViewModel>> AllEnginesFormAsync();

		Task<IEnumerable<CategoryViewModel>> AllCategoriesFormAsync();

        Task<bool> CarExistsAsync(int carId);
		

		Task<int> CreateAsync(CarFormViewModel model, string ownerId);

		Task<CarDetailsViewModel> DetailsAsync(int carId);

		Task EditAsync(int id, CarFormViewModel model);

		Task<CarFormViewModel> CreateCarFormViewModelByIdAsync(int id);

		Task<bool> HasOwnerWithIdAsync(int carId, string ownerId);
		Task RemoveCarAsync(int id);
	}
}