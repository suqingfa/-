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
    public class AddModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "学期")]
            public string SemesterId { get; set; }

            [Display(Name = "课程名")]
            public string ClassId { get; set; }

            [Display(Name = "教师")]
            public string TeacherId { get; set; }
        }

        public Semester[] Semester { get; set; }
        public Class[] Class { get; set; }
        public Data.Teacher[] Teacher { get; set; }

        public Course[] Course { get; set; }

        private UserManager<ApplicationUser> _userManager { get; set; }
        private ApplicationDbContext _context { get; set; }
        private ApplicationUser _user => _userManager.GetUserAsync(User).Result;

        public AddModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;

            Semester = _context.Semesters.ToArray();
            Class = _context.Classes.ToArray();
            Teacher = _context.Teachers.ToArray();

            Course = _context.Courses
                .Include(x => x.Class)
                .Include(x => x.Semester)
                .Include(x => x.Teacher)
                .ToArray();
        }

        public void OnPostAsync()
        {
            if (!ModelState.IsValid)
                return;

            var result = _context.Courses
                .Include(x => x.Class)
                .Include(x => x.Semester)
                .Include(x => x.Teacher)
                .Where(x => true);

            if (!string.IsNullOrWhiteSpace(Input.ClassId))
                result = result.Where(x => x.ClassId == Input.ClassId);

            if (!string.IsNullOrWhiteSpace(Input.SemesterId))
                result = result.Where(x => x.SemesterId == Input.SemesterId);

            if (!string.IsNullOrWhiteSpace(Input.TeacherId))
                result = result.Where(x => x.TeacherId == Input.TeacherId);

            Course = result.ToArray();
        }

        public async Task<IActionResult> OnPostAddAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return Page();

            _context.Add(new StudentCourse
            {
                CourseId = id,
                StudentId = _user.Id
            });
            await _context.SaveChangesAsync();

            TempData["Message"] = "添加成功";
            return RedirectToPage("Add");
        }
    }
}
