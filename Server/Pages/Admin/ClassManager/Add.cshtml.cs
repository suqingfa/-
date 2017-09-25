using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Admin.ClassManager
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "学院")]
            public string InstituteId { get; set; }

            [Required]
            [Display(Name = "课程名称")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "学分")]
            public double Credit { get; set; }
        }

        public Institute[] Institute { get; set; }

        private ApplicationDbContext _context;

        public AddModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Institute = _context.Institutes.ToArray();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                _context.Classes.Add(new Class { InstituteId = Input.InstituteId, Name = Input.Name, Credit = Input.Credit });
                await _context.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            OnGet();
            return Page();
        }
    }
}
