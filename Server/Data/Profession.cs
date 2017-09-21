using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Data
{
    // 专业
    public class Profession
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        // 所属学院
        [Required]
        public Institute Institute { get; set; }

        // 学生
        public List<Student> Students { get; set; }
    }
}
