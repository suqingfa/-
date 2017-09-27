using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Teacher
{
    public class AchievementModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string StudentCourseId { get; set; }

            [Required]
            public double Achievement { get; set; }
        }

        public StudentCourse[] StudentCourse { get; set; }

        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;
        private ApplicationUser _user => _userManager.GetUserAsync(User).Result;

        public AchievementModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult OnGet(string courseId)
        {
            if (string.IsNullOrWhiteSpace(courseId))
                return RedirectToPage("Index");

            StudentCourse = _context.StudentCourses
                .Include(x => x.Student)
                .Where(x => x.CourseId == courseId)
                .ToArray();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string courseId)
        {
            if (!ModelState.IsValid)
            {
                OnGet(courseId);
                return Page();
            }

            var studentCourse = _context.StudentCourses
                .Include(x => x.Course)
                .Include(x => x.Course.Class)
                .FirstOrDefault(x => x.Id == Input.StudentCourseId);

            if (studentCourse == null)
            {
                OnGet(courseId);
                return Page();
            }

            if (Input.Achievement < 0 || Input.Achievement > studentCourse.Course.Class.Credit)
            {
                OnGet(courseId);
                return Page();
            }

            studentCourse.Achievement = Input.Achievement;
            _context.Update(studentCourse);
            await _context.SaveChangesAsync();

            TempData["Message"] = "修改成功";

            OnGet(courseId);
            return Page();
        }
    }
}
