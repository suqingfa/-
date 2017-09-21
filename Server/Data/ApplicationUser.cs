using Microsoft.AspNetCore.Identity;
using System;

namespace Server.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        // 身份证号
        public string CardID { get; set; }
        // 姓名
        public string Name { get; set; }
        // 性别 true-男 false-女
        public bool Sex { get; set; }
        // 出生年月
        public DateTime Birthday { get; set; }
    }
}
