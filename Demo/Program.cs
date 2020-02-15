using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                new User();
            }
        }
    }

    public static class God
    {
        public static void Urodz(User user)
        {
            user.DoB = DateTime.Now;
            Console.WriteLine(user.DoB);
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime DoB { get; set; }
    }
}
