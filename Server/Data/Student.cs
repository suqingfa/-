using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Data
{
    // 学生信息
    public class Student : ApplicationUser
    {
        // 学号
        [Required]
        public string SID { get; set; }
        // 入学时间/年
        [Required]
        public int Admission { get; set; }
        // 所属专业
        [Required]
        public Profession Profession { get; set; }

        // 学生的所有奖惩情况
        public List<Reward> Rewards { get; set; }
        // 所选开课
        public List<StudentCourse> StudentCourses { get; set; }
    }
}
