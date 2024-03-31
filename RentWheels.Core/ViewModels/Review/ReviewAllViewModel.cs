namespace RentWheels.Core.ViewModels.Review
{
    public class ReviewAllViewModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int CarId { get; set; }
    }
}
