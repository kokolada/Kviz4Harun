using System.Collections.Generic;

namespace Kviz4Harun.ViewModels.Teacher
{
    public class AddQuizViewModel
    {
        public AddQuizViewModel()
        {
            Questions = new List<QuestionInfo>();
        }

        public int QuizvId { get; set; }
        public string QuizName { get; set; }

        public List<QuestionInfo> Questions { get; set; }
        public QuestionInfo SubmittedQuestion { get; set; }


        public class QuestionInfo
        {
            public QuestionInfo()
            {
                Answers = new List<AnswerInfo>();
            }

            public int Id { get; set; }
            public string QuestionText { get; set; }
            public List<AnswerInfo> Answers { get; set; }

            public class AnswerInfo
            {
                public int Id { get; set; }
                public string AnswerText { get; set; }
                public bool IsCorrect { get; set; }
            }
        }
    }
}