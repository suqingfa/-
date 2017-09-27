using System.ComponentModel.DataAnnotations;

namespace Server.Data
{
    // 学生选课
    public class StudentCourse
    {
        public string Id { get; set; }
        // 学生
        [Required]
        public string StudentId { get; set; }
        public Student Student { get; set; }
        // 开课
        [Required]
        public string CourseId { get; set; }
        public Course Course { get; set; }
        // 成绩
        public double Achievement { get; set; } = 0;
    }
}
