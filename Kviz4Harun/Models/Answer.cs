using System;

namespace Kviz4Harun.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int SeqNumber { get; set; } //number of question in the quiz
        public bool? AndsweredCorrectly { get; set; }
        public DateTime? AnsweredAt { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int? QuestionOptionId { get; set; }
        public QuestionOption QuestionOption { get; set; } //answer selected by the user

        public int QuizSessionId { get; set; }
        public QuizSession QuizSession { get; set; }
    }
}