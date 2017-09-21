using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "教务系统，管理学生信息.";
        }
    }
}
