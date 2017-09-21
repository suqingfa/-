using System.ComponentModel.DataAnnotations;

namespace Server.Data
{
    // 奖惩
    public class Reward
    {
        public string Id { get; set; }
        // 学生
        [Required]
        public Student Student { get; set; }
        // 标题
        [Required]
        public string Title { get; set; }
        // 奖惩内容
        [Required]
        public string Content { get; set; }
    }
}
