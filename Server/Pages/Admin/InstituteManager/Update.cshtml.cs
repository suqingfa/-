using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Admin.InstituteManager
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public Institute Input { get; set; }

        private ApplicationDbContext _context;

        public UpdateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(string id)
        {
            Input = _context.Institutes.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                _context.Institutes.Update(Input);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
