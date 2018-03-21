using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITAwards
{
    public class AnswerDetail
    {
        private int question;
        private int projectID;
        private int answer;
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        public int Question
        {
            get { return question; }
            set { question = value; }
        }


        public int Answer
        {
            get { return answer; }
            set { answer = value; }
        }


        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }

    }
}