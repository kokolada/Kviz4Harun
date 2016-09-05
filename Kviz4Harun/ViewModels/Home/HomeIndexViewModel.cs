using System;
using System.Collections.Generic;

namespace Kviz4Harun.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
            AvailableQuizes = new List<QuizInfo>();
        }

        public List<QuizInfo> AvailableQuizes { get; set; }

        public class QuizInfo
        {
            public int vId { get; set; }
            public string Name { get; set; }
            public DateTime CreatedAt { get; set; }
            public string CreatedBy { get; set; }
        }
    }
}