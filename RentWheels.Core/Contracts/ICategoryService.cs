using RentWheels.Core.ViewModels.Category;

namespace RentWheels.Core.Contracts
{
	public interface ICategoryService
	{
		Task<bool> CategoryExistsAsync(int categoryId);

		Task<IEnumerable<string>> AllCategoriesNamesAsync();

		Task<IEnumerable<CategoryViewModel>> AllCategoriesFormAsync();
	}
}
