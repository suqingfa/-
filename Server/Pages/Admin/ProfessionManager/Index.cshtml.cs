using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Admin.ProfessionManager
{
    public class IndexModel : PageModel
    {
        public Profession[] Profession { get; set; }
        public Institute[] Institute { get; set; }
        private ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Profession = _context.Professions.ToArray();
            Institute = _context.Institutes.ToArray();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            _context.Professions.Remove(_context.Professions.FirstOrDefault(x => x.Id == id));
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
