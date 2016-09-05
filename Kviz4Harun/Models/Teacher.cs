using System.Collections.Generic;

namespace Kviz4Harun.Models
{
    public class Teacher
    {
        public Teacher()
        {
            Quizes = new List<Quiz>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Quiz> Quizes { get; set; }
    }
}