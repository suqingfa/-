using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Data
{
    // 开课
    public class Course
    {
        public string Id { get; set; }
        // 课程
        [Required]
        public Class Class { get; set; }
        // 教课老师
        [Required]
        public Teacher Teacher { get; set; }
        // 所开学期
        [Required]
        public Semester Semester { get; set; }

        // 当前课程所开的课
        public List<StudentCourse> StudentCourses { get; set; }
    }
}
