namespace Server.Data
{
    public class Roles
    {
        public const string ADMIN = "admin";
        public const string TEACHER = "teacher";
        public const string STUDENT = "student";

        public static string[] Values => new string[] { ADMIN, TEACHER, STUDENT };
    }
}
