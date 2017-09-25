using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Admin.SemesterManager
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public Semester Input { get; set; }

        private ApplicationDbContext _context;

        public UpdateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(string id)
        {
            if (id == null)
                return;
            Input = _context.Semesters.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                _context.Semesters.Update(Input);
                await _context.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
