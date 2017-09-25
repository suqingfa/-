using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Admin.ClassManager
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public Class Input { get; set; }
        public Institute[] Institute { get; set; }

        private ApplicationDbContext _context;

        public UpdateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(string id)
        {
            Input = _context.Classes.FirstOrDefault(x => x.Id == id);
            Institute = _context.Institutes.ToArray();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                _context.Classes.Update(Input);
                await _context.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            OnGet(Input.Id);
            return Page();
        }
    }
}
