using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Admin.ProfessionManager
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Profession Input { get; set; }

        private ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Professions.Remove(_context.Professions.FirstOrDefault(x => x.Id == Input.Id));
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
