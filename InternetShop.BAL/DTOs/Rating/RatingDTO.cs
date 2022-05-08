using System.ComponentModel.DataAnnotations;

namespace InternetShop.BAL.DTOs.Rating
{
    public class RatingDTO
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int StarsCount { get; set; }
    }
}
