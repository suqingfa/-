using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Server.Pages.Admin.SemesterManager
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "学期名称")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "开始时间")]
            public DateTime Start { get; set; }

            [Required]
            [Display(Name = "结束时间")]
            public DateTime End { get; set; }
        }

        private ApplicationDbContext _context;

        public AddModel(ApplicationDbContext context)
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
                _context.Semesters.Add(new Semester { Name = Input.Name, Start = Input.Start, End = Input.End });
                await _context.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            OnGet();
            return Page();
        }
    }
}
