using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bug_Tracking_System.Models.ViewModels
{
    public class BugCreateEditViewModel
    {
        public Bug Bug { get; set; } = new Bug();
        public SelectList? Projects { get; set; }
        public SelectList? Users { get; set; }
        public List<SelectListItem> Statuses { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Open", Text = "Open" },
            new SelectListItem { Value = "In Progress", Text = "In Progress" },
            new SelectListItem { Value = "Resolved", Text = "Resolved" },
            new SelectListItem { Value = "Closed", Text = "Closed" }
        };
        public List<SelectListItem> Priorities { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Low", Text = "Low" },
            new SelectListItem { Value = "Medium", Text = "Medium" },
            new SelectListItem { Value = "High", Text = "High" },
            new SelectListItem { Value = "Critical", Text = "Critical" }
        };
    }
}
