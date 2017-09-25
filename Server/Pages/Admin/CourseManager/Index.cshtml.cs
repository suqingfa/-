using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Admin.CourseManager
{
    public class IndexModel : PageModel
    {
        public Course[] Input { get; set; }

        private ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Input = _context.Courses
                .Include(x => x.Semester)
                .Include(x => x.Teacher)
                .Include(x => x.Class)
                .ToArray();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            _context.Courses.Remove(_context.Courses.FirstOrDefault(x => x.Id == id));
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
