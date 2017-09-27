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
    }
}
