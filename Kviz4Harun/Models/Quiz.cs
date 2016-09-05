using System;
using System.Collections.Generic;

namespace Kviz4Harun.Models
{
    public class Quiz
    {
        public Quiz()
        {
            Questions = new List<Question>();
            QuizResults = new List<QuizSession>();
        }

        public int Id { get; set; }
        public int vId { get; set; }
        public bool isPublished { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Question> Questions { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<QuizSession> QuizResults { get; set; }
    }
}