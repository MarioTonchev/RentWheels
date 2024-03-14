using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RentWheels.Infrastructure.Constants.DataConstants.ReviewConstants;

namespace RentWheels.Infrastructure.Models
{
    [Comment("Reviews posted by the user about the experience with the car")]
    public class Review
    {
        [Key]
        [Comment("Identifier of the review")]
        public int Id { get; set; }
        [Required]
        [Comment("Identifier of the reviewed car")]
        public int CarId { get; set; }
        [Required]
        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;
        [Required]
        [Comment("Identifier of the reviewer of the car")]
        public string ReviewerId { get; set; } = string.Empty;
        [Required]
        [ForeignKey(nameof(ReviewerId))]
        public IdentityUser Reviewer { get; set; } = null!;
        [Required]
        [Comment("Rating given by the reviewer for the car")]
        public int Rating { get; set; }
        [Required]
        [MaxLength(ReviewMaxCommentLength)]
        [Comment("Comment given by the reviewer for the car")]
        public string Comment { get; set; } = string.Empty;
    }
}