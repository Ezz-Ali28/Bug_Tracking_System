using Bug_Tracking_System.Models;
using Bug_Tracking_System.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bug_Tracking_System.Controllers
{
    public class BugsController : Controller
    {
        private readonly BugsTrackingContext _context;

        public BugsController(BugsTrackingContext context)
        {
            _context = context;
        }

        // GET: Bugs
        public async Task<IActionResult> Index(string status, string priority, int? projectId)
        {
            var bugs = _context.Bug
                .Include(b => b.Project)
                .Include(b => b.AssignedTo)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                bugs = bugs.Where(b => b.Status == status);
            }

            if (!string.IsNullOrEmpty(priority))
            {
                bugs = bugs.Where(b => b.Priority == priority);
            }

            if (projectId.HasValue)
            {
                bugs = bugs.Where(b => b.ProjectId == projectId);
            }

            ViewBag.CurrentStatus = status;
            ViewBag.CurrentPriority = priority;
            ViewBag.CurrentProjectId = projectId;
            ViewBag.Project = new SelectList(await _context.Project.ToListAsync(), "Id", "Name", projectId);

            return View(await bugs.OrderByDescending(b => b.CreatedAt).ToListAsync());
        }

        // GET: Bugs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug
                .Include(b => b.Project)
                .Include(b => b.AssignedTo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bug == null)
            {
                return NotFound();
            }

            // Load comments for this bug
            var comments = await _context.Comment
                .Include(c => c.User)
                .Where(c => c.BugId == id)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            ViewBag.Comments = comments;
            ViewBag.Users = new SelectList(await _context.User.ToListAsync(), "Id", "FullName");

            return View(bug);
        }

        // GET: Bugs/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new BugCreateEditViewModel
            {
                Projects = new SelectList(await _context.Project.ToListAsync(), "Id", "Name"),
                Users = new SelectList(await _context.User.ToListAsync(), "Id", "FullName")
            };
            return View(viewModel);
        }

        // POST: Bugs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BugCreateEditViewModel viewModel)
        {
            // Remove navigation properties from validation
            ModelState.Remove("Bug.Project");
            ModelState.Remove("Bug.AssignedTo");

            if (ModelState.IsValid)
            {
                try
                {
                    viewModel.Bug.CreatedAt = DateTime.Now;
                    _context.Bug.Add(viewModel.Bug);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error saving bug: " + ex.Message);
                }
            }

            // Log validation errors to console for debugging
            foreach (var error in ModelState.Where(e => e.Value != null && e.Value.Errors.Count > 0))
            {
                foreach (var err in error.Value!.Errors)
                {
                    Console.WriteLine($"ModelState Error - {error.Key}: {err.ErrorMessage}");
                }
            }

            viewModel.Projects = new SelectList(await _context.Project.ToListAsync(), "Id", "Name", viewModel.Bug.ProjectId);
            viewModel.Users = new SelectList(await _context.User.ToListAsync(), "Id", "FullName", viewModel.Bug.AssignedToId);
            return View(viewModel);
        }

        // GET: Bugs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }

            var viewModel = new BugCreateEditViewModel
            {
                Bug = bug,
                Projects = new SelectList(await _context.Project.ToListAsync(), "Id", "Name", bug.ProjectId),
                Users = new SelectList(await _context.User.ToListAsync(), "Id", "FullName", bug.AssignedToId)
            };

            return View(viewModel);
        }

        // POST: Bugs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BugCreateEditViewModel viewModel)
        {
            if (id != viewModel.Bug.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewModel.Bug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BugExists(viewModel.Bug.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            viewModel.Projects = new SelectList(await _context.Project.ToListAsync(), "Id", "Name", viewModel.Bug.ProjectId);
            viewModel.Users = new SelectList(await _context.User.ToListAsync(), "Id", "FullName", viewModel.Bug.AssignedToId);
            return View(viewModel);
        }

        // GET: Bugs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug
                .Include(b => b.Project)
                .Include(b => b.AssignedTo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // POST: Bugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bug = await _context.Bug.FindAsync(id);
            if (bug != null)
            {
                _context.Bug.Remove(bug);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Bugs/AddComment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int bugId, string content, int userId)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                var comment = new Comment
                {
                    BugId = bugId,
                    Content = content,
                    UserId = userId,
                    CreatedAt = DateTime.Now
                };
                _context.Comment.Add(comment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new { id = bugId });
        }

        private bool BugExists(int id)
        {
            return _context.Bug.Any(e => e.Id == id);
        }
    }
}
