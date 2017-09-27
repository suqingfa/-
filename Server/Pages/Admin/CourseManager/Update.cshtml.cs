using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Admin.CourseManager
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Id { get; set; }

            // 课程
            [Required]
            [Display(Name = "课程")]
            public string ClassId { get; set; }
            public string OldClassId { get; set; }

            // 教课老师
            [Required]
            [Display(Name = "教师")]
            public string TeacherId { get; set; }
            public string OldTeacherId { get; set; }

            // 所开学期
            [Required]
            [Display(Name = "学期")]
            public string SemesterId { get; set; }
            public string OldSemesterId { get; set; }
        }

        public Semester[] Semester { get; set; }
        public Class[] Class { get; set; }
        public Data.Teacher[] Teacher { get; set; }

        private ApplicationDbContext _context;

        public UpdateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(string id)
        {
            if (id == null)
                return;

            Semester = _context.Semesters.ToArray();
            Class = _context.Classes.ToArray();
            Teacher = _context.Teachers.ToArray();

            var course = _context.Courses.FirstOrDefault(x => x.Id == id);
            Input = new InputModel
            {
                Id = course.Id,

                ClassId = course.ClassId,
                OldClassId = course.ClassId,

                TeacherId = course.TeacherId,
                OldTeacherId = course.TeacherId,

                SemesterId = course.SemesterId,
                OldSemesterId = course.SemesterId
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Course
                {
                    TeacherId = Input.TeacherId,
                    ClassId = Input.ClassId,
                    SemesterId = Input.SemesterId,
                });

                var old = _context.Courses.FirstOrDefault(x =>
                    x.SemesterId == Input.OldSemesterId &&
                    x.TeacherId == Input.OldTeacherId &&
                    x.ClassId == Input.ClassId);
                _context.Remove(old);

                await _context.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            OnGet(Input.Id);
            return Page();
        }
    }
}
