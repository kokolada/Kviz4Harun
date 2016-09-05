using System.Collections.Generic;

namespace Kviz4Harun.Models
{
    public class Question
    {
        public Question()
        {
            PossibleAnswers = new List<QuestionOption>();
        }

        public int Id { get; set; }
        public string Text { get; set; }

        public int? QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public ICollection<QuestionOption> PossibleAnswers { get; set; }

        public int? CorrectAnswerId { get; set; }
        public QuestionOption CorrectAnswer { get; set; }
    }
}