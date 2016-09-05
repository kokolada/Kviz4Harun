using System.Collections.Generic;

namespace Kviz4Harun.ViewModels.Teacher
{
    public class EditQuizViewModel
    {
        public EditQuizViewModel()
        {
            Questions = new List<QuestionInfo>();
        }

        public int QuizvId { get; set; }
        public string QuizName { get; set; }
        public List<QuestionInfo> Questions { get; set; }

        public QuestionInfo NewQuestion { get; set; }

        public class QuestionInfo
        {
            public QuestionInfo()
            {
                Answers = new List<AnswerInfo>();
            }

            public int Id { get; set; }
            public string Text { get; set; }
            public List<AnswerInfo> Answers { get; set; }

            public class AnswerInfo
            {
                public int Id { get; set; }
                public string Text { get; set; }
                public bool IsCorrect { get; set; }
            }
        }
    }
}