using Microsoft.EntityFrameworkCore;
using RentWheels.Infrastructure.Data;

namespace RentWheels.Infrastructure.Common
{
	public class Repository : IRepository
	{
		private readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext _context)
        {
			context = _context;
        }

		private DbSet<T> DbSet<T>() where T : class 
		{
			return context.Set<T>();
		}

        public IQueryable<T> All<T>() where T : class
		{
			return DbSet<T>();
		}

		public IQueryable<T> AllAsReadOnly<T>() where T : class
		{
			return DbSet<T>().AsNoTracking();
		}

		public async Task AddAsync<T>(T entity) where T : class
		{
			await DbSet<T>().AddAsync(entity);
		}

		public async Task<int> SaveChangesAsync()
		{
			return await context.SaveChangesAsync();
		}
	}
}
