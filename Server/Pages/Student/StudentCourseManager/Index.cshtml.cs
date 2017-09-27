using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Student.StudentCourseManager
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "学期")]
            public string SemesterId { get; set; }

            [Display(Name = "教师")]
            public string TeacherId { get; set; }
        }

        public StudentCourse[] StudentCourses { get; set; }

        public Semester[] Semester { get; set; }
        public Data.Teacher[] Teacher { get; set; }

        private UserManager<ApplicationUser> _userManager { get; set; }
        private ApplicationDbContext _context { get; set; }
        private ApplicationUser _user => _userManager.GetUserAsync(User).Result;

        public IndexModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;

            Semester = _context.Semesters.ToArray();
            Teacher = _context.Teachers.ToArray();
        }

        public void OnGet()
        {
            StudentCourses = _context.StudentCourses
                .Include(x => x.Course)
                .Include(x => x.Course.Semester)
                .Include(x => x.Course.Class)
                .Include(x => x.Course.Teacher)
                .Where(x => x.StudentId == _user.Id)
                .ToArray();
        }

        public void OnPostAsync()
        {
            if (!ModelState.IsValid)
                return;

            var result = _context.StudentCourses
                .Include(x => x.Course)
                .Include(x => x.Course.Semester)
                .Include(x => x.Course.Class)
                .Include(x => x.Course.Teacher)
                .Where(x => x.StudentId == _user.Id);

            if (Input.SemesterId != null)
                result = result.Where(x => x.Course.SemesterId == Input.SemesterId);

            if (Input.TeacherId != null)
                result = result.Where(x => x.Course.TeacherId == Input.TeacherId);

            StudentCourses = result.ToArray();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return Page();

            var delete = _context.StudentCourses.FirstOrDefault(x => x.Id == id);
            if (delete == null)
                return Page();

            _context.Remove(delete);
            await _context.SaveChangesAsync();

            TempData["Message"] = "删除成功";

            return RedirectToPage("Index");
        }
    }
}
