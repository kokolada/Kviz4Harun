using System;
using System.Collections.Generic;

namespace Kviz4Harun.Models
{
    public class User
    {
        public User()
        {
            QuizSessions = new List<QuizSession>();
        }

        public int Id { get; set; }
        public string username { get; set; }
        public string passwordSalt { get; set; }
        public string passwordHash { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<QuizSession> QuizSessions { get; set; }
    }
}