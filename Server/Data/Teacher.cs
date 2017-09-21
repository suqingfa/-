using System.Collections.Generic;

namespace Server.Data
{
    // 老师信息
    public class Teacher : ApplicationUser
    {
        // 教师教过的开课
        public List<Course> Courses { get; set; }
    }
}
