using HadramutSite.Models;
using Microsoft.EntityFrameworkCore;

namespace HadramutSite.Data
{
    public class MyDbContext : DbContext    
    {
        public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User_Subject> User_Subjects { get; set; }


    }
}
