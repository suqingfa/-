using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Data;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Server.Pages.Admin.InstituteManager
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "学院名")]
            public string Name { get; set; }
        }

        private ApplicationDbContext _context;

        public AddModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                _context.Institutes.Add(new Data.Institute { Name = Input.Name });
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
