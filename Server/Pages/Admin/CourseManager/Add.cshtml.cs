using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Admin.CourseManager
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "学期名称")]
            public string SemesterId { get; set; }

            [Required]
            [Display(Name = "课程名称")]
            public string ClassId { get; set; }

            [Required]
            [Display(Name = "教师")]
            public string TeacherId { get; set; }
        }

        public Semester[] Semester { get; set; }
        public Class[] Class { get; set; }
        public Teacher[] Teacher { get; set; }

        private ApplicationDbContext _context;

        public AddModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Semester = _context.Semesters.ToArray();
            Class = _context.Classes.ToArray();
            Teacher = _context.Teachers.ToArray();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Course
                {
                    SemesterId = Input.SemesterId,
                    ClassId = Input.ClassId,
                    TeacherId = Input.TeacherId
                });
                await _context.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            OnGet();
            return Page();
        }
    }
}
