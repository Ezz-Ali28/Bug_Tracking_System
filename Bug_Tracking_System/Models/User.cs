using System.ComponentModel.DataAnnotations;

namespace Bug_Tracking_System.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Developer";

        public ICollection<Bug>? AssignedBugs { get; set; }
        public ICollection<Comment>? Comment { get; set; }
    }
}