using System.ComponentModel.DataAnnotations;

namespace BookstoreApp.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Author { get; set; }

        [Required]
        [RegularExpression(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$", ErrorMessage = "Invalid ISBN format.")]
        public string ISBN { get; set; }

        [Required]
        [PriceRange(1, 1000, ErrorMessage = "Price must be between 1 and 1000.")]
        public decimal Price { get; set; }
    }

    public class PriceRangeAttribute : ValidationAttribute
    {
        private readonly decimal _minPrice;
        private readonly decimal _maxPrice;

        public PriceRangeAttribute(double minPrice, double maxPrice)
        {
            _minPrice = (decimal)minPrice;
            _maxPrice = (decimal)maxPrice;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is decimal price)
            {
                if (price < _minPrice || price > _maxPrice)
                {
                    return new ValidationResult(ErrorMessage ?? $"Price must be between {_minPrice} and {_maxPrice}.");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid price format.");
        }
    }
}
