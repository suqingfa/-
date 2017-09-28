using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Server.Data;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Teacher
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "学期")]
            public string SemesterId { get; set; }
        }

        public Course[] Course { get; set; }
        public Semester[] Semester { get; set; }

        private UserManager<ApplicationUser> _userManager { get; set; }
        private ApplicationDbContext _context { get; set; }
        private ApplicationUser _user => _userManager.GetUserAsync(User).Result;

        public IndexModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;

            Semester = _context.Semesters.ToArray();
        }

        public void OnGet()
        {
            Course = _context.Courses
                .Include(x => x.Semester)
                .Include(x => x.Class)
                .Where(x => x.TeacherId == _user.Id)
                .ToArray();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = _context.Courses
                .Include(x => x.Semester)
                .Include(x => x.Class)
                .Where(x => x.TeacherId == _user.Id);

            if (!string.IsNullOrWhiteSpace(Input.SemesterId))
                result = result.Where(x => x.SemesterId == Input.SemesterId);

            Course = result.ToArray();

            return Page();
        }

        public async Task<IActionResult> OnGetGetStudentListAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                OnGet();
                return Page();
            }

            var studentList = _context.StudentCourses
                .Include(x => x.Student)
                .Include(x => x.Student.Profession)
                .Where(x => x.CourseId == id);

            MemoryStream stream = new MemoryStream();

            using (ExcelPackage package = new ExcelPackage(stream))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("学生花名册");
                //First add the headers
                worksheet.Cells[1, 1].Value = "专业";
                worksheet.Cells[1, 2].Value = "学号";
                worksheet.Cells[1, 3].Value = "姓名";
                worksheet.Cells[1, 4].Value = "入学时间";

                int i = 1;
                await studentList.ForEachAsync(x =>
                {
                    i++;
                    worksheet.Cells["A" + i].Value = x.Student.Profession.Name;
                    worksheet.Cells["B" + i].Value = x.Student.Id;
                    worksheet.Cells["C" + i].Value = x.Student.Name;
                    worksheet.Cells["D" + i].Value = x.Student.Admission;
                });

                package.Save(); //Save the workbook.
            }

            await stream.FlushAsync();
            stream.Position = 0;

            return File(stream.GetBuffer(), "application/vnd.ms-excel", "student.xlsx");
        }
    }
}
