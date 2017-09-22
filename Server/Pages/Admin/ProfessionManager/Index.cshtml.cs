using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Data;
using System.Linq;

namespace Server.Pages.Admin.ProfessionManager
{
    public class IndexModel : PageModel
    {
        public Profession[] Profession { get; set; }
        public Institute[] Institute { get; set; }
        private ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Profession = _context.Professions.ToArray();
            Institute = _context.Institutes.ToArray();
        }
    }
}
