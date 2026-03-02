using System.Diagnostics;
using Bug_Tracking_System.Models;
using Bug_Tracking_System.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bug_Tracking_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BugsTrackingContext _context;

        public HomeController(ILogger<HomeController> logger, BugsTrackingContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel
            {
                TotalProjects = await _context.Project.CountAsync(),
                TotalBugs = await _context.Bug.CountAsync(),
                OpenBugs = await _context.Bug.CountAsync(b => b.Status == "Open"),
                InProgressBugs = await _context.Bug.CountAsync(b => b.Status == "In Progress"),
                ClosedBugs = await _context.Bug.CountAsync(b => b.Status == "Closed" || b.Status == "Resolved"),
                TotalUsers = await _context.User.CountAsync(),
                RecentBugs = await _context.Bug
                    .Include(b => b.Project)
                    .Include(b => b.AssignedTo)
                    .OrderByDescending(b => b.CreatedAt)
                    .Take(5)
                    .ToListAsync(),
                Projects = await _context.Project
                    .Include(p => p.Bug)
                    .ToListAsync()
            };

            viewModel.BugsByPriority = await _context.Bug
                .GroupBy(b => b.Priority)
                .Select(g => new { Priority = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Priority ?? "Unknown", x => x.Count);

            

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
