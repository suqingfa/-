using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        // 课程
        public DbSet<Class> Classes { get; set; }
        // 开课
        public DbSet<Course> Courses { get; set; }
        // 学院
        public DbSet<Institute> Institutes { get; set; }
        // 专业
        public DbSet<Profession> Professions { get; set; }
        // 奖惩
        public DbSet<Reward> Rewards { get; set; }
        // 学期
        public DbSet<Semester> Semesters { get; set; }
        // 学生
        public DbSet<Student> Students { get; set; }
        // 学生选课
        public DbSet<StudentCourse> StudentCourses { get; set; }
        // 老师
        public DbSet<Teacher> Teachers { get; set; }
    }
}
