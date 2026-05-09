using System.ComponentModel.DataAnnotations;

namespace BookstoreApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Role { get; set; } // "Admin" or "User"
    }
}
