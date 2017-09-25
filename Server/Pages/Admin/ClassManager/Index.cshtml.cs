using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Admin.ClassManager
{
    public class IndexModel : PageModel
    {
        public Class[] Class { get; set; }
        private ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Class = _context.Classes.Include(x => x.Institute).ToArray();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            _context.Classes.Remove(_context.Classes.FirstOrDefault(x => x.Id == id));
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
