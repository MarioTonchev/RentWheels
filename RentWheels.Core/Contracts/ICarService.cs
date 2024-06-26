﻿using RentWheels.Core.Enumerations;
using RentWheels.Core.ViewModels.Admin;
using RentWheels.Core.ViewModels.Car;

namespace RentWheels.Core.Contracts
{
	public interface ICarService
	{		
		Task<CarQueryViewModel> AllAsync(
			string? cateogry = null,
			string? color = null,
			string? searchTerm = null, 
			CarSorting sorting = CarSorting.Newest,
			int currentPage = 1,
			int carPerPage = 4);

		Task<IEnumerable<AdminAllCarsViewModel>> AllCarsAsync();

        Task<bool> CarExistsAsync(int carId);
		
		Task<int> CreateAsync(CarFormViewModel model, string ownerId);

		Task<IEnumerable<MyCarsViewModel>> MyCarsAsync(string ownerId);

		Task<CarDetailsViewModel> DetailsAsync(int carId);

		Task EditAsync(int id, CarFormViewModel model);

		Task<CarFormViewModel> CreateCarFormViewModelByIdAsync(int id);

		Task<bool> HasOwnerWithIdAsync(int carId, string ownerId);

		Task RemoveCarAsync(int id);
	}
}