using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Data
{
    // 学院
    public class Institute
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        // 所开专业
        public List<Profession> Professiones { get; set; }
        // 所开课程
        public List<Class> Classes { get; set; }
    }
}
