using System.ComponentModel.DataAnnotations;

namespace Bug_Tracking_System.Models
{
    public class Project
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Project name is required")]
        [StringLength(100, ErrorMessage = "Project name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }
        
        public ICollection<Bug>? Bug { get; set; }
    }
}