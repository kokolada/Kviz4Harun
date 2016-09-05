using Kviz4Harun.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Kviz4Harun
{
    public class Context : DbContext
    {
        public Context() : base("name=LocalConnection")
        {

        }

        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<QuizSession> QuizSessions { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}