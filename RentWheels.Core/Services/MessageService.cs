using Microsoft.EntityFrameworkCore;
using RentWheels.Core.Contracts;
using RentWheels.Core.ViewModels.Message;
using RentWheels.Infrastructure.Common;
using RentWheels.Infrastructure.Models;

namespace RentWheels.Core.Services
{
	public class MessageService : IMessageService
	{
		private readonly IRepository repository;

        public MessageService(IRepository _repository)
        {
            repository = _repository;
        }

		public async Task<IEnumerable<AllMessagesViewModel>> MyMessagesAsync(string receiverId)
		{
			return await repository.AllAsReadOnly<Message>().Where(m => m.ReceiverId == receiverId)
				.Select(m => new AllMessagesViewModel()
				{
					Title = m.Title,
					Content = m.Content,
					CreatedOn = m.CreatedOn.ToString().Substring(0, 16),
					SenderUserName = m.Sender.UserName
				}).OrderByDescending(m => m.CreatedOn).ToListAsync();
		}

		public async Task SendAsync(MessageFormViewModel model, string senderId)
		{
			var rental = await repository.AllAsReadOnly<Rental>().Include(r => r.Car).FirstOrDefaultAsync(r => r.Id == model.RentalId);

			var message = new Message()
			{
				Title = model.Title,
				Content = model.Content,
				CreatedOn = DateTime.Now,
				SenderId = senderId,
				ReceiverId = rental.Car.OwnerId
			};

			await repository.AddAsync(message);
			await repository.SaveChangesAsync();
		}
	}
}
