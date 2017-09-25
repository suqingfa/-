using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Admin.SemesterManager
{
    public class IndexModel : PageModel
    {
        public Semester[] Semester { get; set; }

        private ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Semester = _context.Semesters.ToArray();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            _context.Semesters.Remove(_context.Semesters.FirstOrDefault(x => x.Id == id));
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
