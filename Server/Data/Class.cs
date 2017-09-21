using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Data
{
    // 课程
    public class Class
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        // 学分
        public double Credit { get; set; }
        // 所属学院
        [Required]
        public Institute Institute { get; set; }

        // 当前课程所开的课
        public List<Course> Courses { get; set; }
    }
}
