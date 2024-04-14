using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RentWheels.Infrastructure.Constants.DataConstants.MessageEntityConstants;

namespace RentWheels.Infrastructure.Models
{
	[Comment("Message sent by a user to the owner of the car they have rented")]
	public class Message
	{
		[Key]
		[Comment("Identifier of the message")]
        public int Id { get; set; }
		[Required]
		[StringLength(MessageMaxTitleLength)]
		[Comment("Title of the message")]
		public string Title { get; set; } = string.Empty;
        [Required]
		[StringLength(MessageMaxContentLength)]
		[Comment("Main content of the message")]
		public string Content { get; set; } = string.Empty;
		[Required]
		[Comment("When the message was sent")]
		public DateTime CreatedOn { get; set; } 
		[Required]
		[Comment("Identifier of the sender")]
		public string SenderId { get; set; } = string.Empty;
		[Required]
		[ForeignKey(nameof(SenderId))]
		public IdentityUser Sender { get; set; } = null!;
        [Required]
		[Comment("Identifier of the receiver")]
		public string ReceiverId { get; set; } = string.Empty;
		[Required]
		[ForeignKey(nameof(ReceiverId))]
        public IdentityUser Receiver { get; set; } = null!;
    }
}
