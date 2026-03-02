
using System.ComponentModel.DataAnnotations;

namespace Bug_Tracking_System.Models
{
    public class Bug
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Open";

        [Required]
        public string Priority { get; set; } = "Medium";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public int? AssignedToId { get; set; }
        public User? AssignedTo { get; set; }
    }
}
