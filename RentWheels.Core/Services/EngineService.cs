using Microsoft.EntityFrameworkCore;
using RentWheels.Core.Contracts;
using RentWheels.Core.ViewModels.Engine;
using RentWheels.Infrastructure.Common;
using RentWheels.Infrastructure.Models;

namespace RentWheels.Core.Services
{
    public class EngineService : IEngineService
    {
        private readonly IRepository repository;

        public EngineService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<EngineDetailsViewModel> DetailsAsync(int id)
        {
            var engine = await repository.AllAsReadOnly<Engine>().Where(e => e.Id == id).FirstOrDefaultAsync();

            var model = new EngineDetailsViewModel()
            {
                Name = engine.Name,
                Horsepower = engine.Horsepower,
                Cubage = engine.Cubage,
                FuelType = engine.FuelType
            };

            return model;
        }

        public async Task<bool> EngineExistsAsync(int engineId)
        {
            return await repository.AllAsReadOnly<Engine>().AnyAsync(e => e.Id == engineId);
        }

		public async Task<IEnumerable<EngineAllViewModel>> AllEnginesAsync()
		{
			return await repository.AllAsReadOnly<Engine>().Select(e => new EngineAllViewModel()
			{
				Name = e.Name,
				Horsepower = e.Horsepower,
				Cubage = e.Cubage,
				FuelType = e.FuelType,
			}).ToListAsync();
		}

		public async Task<IEnumerable<EngineFormViewModel>> AllEnginesFormAsync()
		{
			return await repository.AllAsReadOnly<Engine>().Select(e => new EngineFormViewModel()
			{
				Id = e.Id,
				Name = e.Name
			}).ToListAsync();
		}
	}
}
