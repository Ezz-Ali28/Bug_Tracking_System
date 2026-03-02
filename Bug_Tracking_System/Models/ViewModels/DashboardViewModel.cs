namespace Bug_Tracking_System.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalProjects { get; set; }
        public int TotalBugs { get; set; }
        public int OpenBugs { get; set; }
        public int InProgressBugs { get; set; }
        public int ClosedBugs { get; set; }
        public int TotalUsers { get; set; }
        public List<Bug> RecentBugs { get; set; } = new List<Bug>();
        public List<Project> Projects { get; set; } = new List<Project>();
        public Dictionary<string, int> BugsByPriority { get; set; } = new Dictionary<string, int>();
    }
}
