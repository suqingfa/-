using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Server.Pages.Student
{
    public class AchievementModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "学期")]
            public string SemesterId { get; set; }
        }

        public StudentCourse[] StudentCourses { get; set; }

        public Semester[] Semester { get; set; }

        private UserManager<ApplicationUser> _userManager { get; set; }
        private ApplicationDbContext _context { get; set; }
        private ApplicationUser _user => _userManager.GetUserAsync(User).Result;

        public AchievementModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;

            Semester = _context.Semesters.ToArray();
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

            StudentCourses = result.ToArray();
        }
    }
}
