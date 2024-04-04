using Microsoft.EntityFrameworkCore;
using RentWheels.Core.Contracts;
using RentWheels.Core.ViewModels.Category;
using RentWheels.Infrastructure.Common;
using RentWheels.Infrastructure.Models;

namespace RentWheels.Core.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IRepository repository;

        public CategoryService(IRepository _repository)
        {
			repository = _repository;
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
		{
			return await repository.AllAsReadOnly<Category>().AnyAsync(c => c.Id == categoryId);
		}

		public async Task<IEnumerable<string>> AllCategoriesNamesAsync()
		{
			return await repository.AllAsReadOnly<Category>().Select(c => c.Name).ToListAsync();
		}

		public async Task<IEnumerable<CategoryViewModel>> AllCategoriesFormAsync()
		{
			return await repository.AllAsReadOnly<Category>().Select(e => new CategoryViewModel()
			{
				Id = e.Id,
				Name = e.Name
			}).ToListAsync();
		}
	}
}
