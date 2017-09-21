using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Data
{
    // 学期
    public class Semester
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        // 开始时间
        [Required]
        public DateTime Start { get; set; }
        // 结束时间
        [Required]
        public DateTime End { get; set; }

        // 当前学期的开课
        public List<Course> Courses { get; set; }
    }
}
