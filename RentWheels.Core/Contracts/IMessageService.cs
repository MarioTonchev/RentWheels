using RentWheels.Core.ViewModels.Message;

namespace RentWheels.Core.Contracts
{
	public interface IMessageService
	{
		Task<IEnumerable<AllMessagesViewModel>> MyMessagesAsync(string receiverId);

		Task SendAsync(MessageFormViewModel model, string senderId);
	}
}
