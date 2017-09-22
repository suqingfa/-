using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Admin.ProfessionManager
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public Profession Input { get; set; }
        public Institute[] Institute { get; set; }

        private ApplicationDbContext _context;

        public UpdateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(string id)
        {
            Input = _context.Professions.FirstOrDefault(x => x.Id == id);
            Institute = _context.Institutes.ToArray();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                _context.Professions.Update(Input);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
