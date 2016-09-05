using System;
using System.Collections.Generic;

namespace Kviz4Harun.ViewModels.Quiz
{
    public class NewSessionViewModel //here is all the data that will be needed for a pupil to go through the quiz from start to finish
    {
        public NewSessionViewModel()
        {
            CurrentQuestion = new QuestionInfo();
        }

        public int vId { get; set; }
        public int SessionId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Teacher { get; set; }
        public QuestionInfo CurrentQuestion { get; set; }

        public class QuestionInfo
        {
            public QuestionInfo()
            {
                PossibleAnswers = new List<OptionInfo>();
            }

            public int Id { get; set; }
            public string Text { get; set; }
            public int SeqNumber { get; set; }
            public bool IsLastQuestion { get; set; }
            public List<OptionInfo> PossibleAnswers { get; set; }
            public int? ChosenAnswerId { get; set; }

            public class OptionInfo
            {
                public int Id { get; set; }
                public string Text { get; set; }
            }
        }
    }
}