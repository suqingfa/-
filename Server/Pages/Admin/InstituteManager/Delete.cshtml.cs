using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Admin.InstituteManager
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Data.Institute Input { get; set; }

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
            if (ModelState.IsValid)
            {
                _context.Institutes.Remove(_context.Institutes.FirstOrDefault(x => x.Id == Input.Id));
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
