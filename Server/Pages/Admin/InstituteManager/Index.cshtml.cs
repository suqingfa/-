using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Admin.InstituteManager
{
    public class IndexModel : PageModel
    {
        public Data.Institute[] Institute { get; set; }

        private ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Institute = _context.Institutes.ToArray();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            _context.Institutes.Remove(_context.Institutes.FirstOrDefault(x => x.Id == id));
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
