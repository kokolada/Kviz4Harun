using System;
using System.Collections.Generic;

namespace Kviz4Harun.Models
{
    public class QuizSession
    {
        public QuizSession()
        {
            PupilAnswers = new List<Answer>();
            CorrectAnswers = 0;
            WrongAnswers = 0;
        }

        public int Id { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Answer> PupilAnswers { get; set; }
    }
}