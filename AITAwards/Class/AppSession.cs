﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITAwards
{
    public static class AppSession
    {
        public static void SetDDEvent(int eventID)
        {
            HttpContext.Current.Session["DDEvent"] = eventID;
        }

        public static int GetDDEvent()
        {
            if (HttpContext.Current.Session["DDEvent"] == null)
            {
                return -1;
            }
            else
            {
                return (int)HttpContext.Current.Session["DDEvent"];
            }
        }

        public static void SetCategoryID(int categoryID)
        {
            HttpContext.Current.Session["CategoryID"] = categoryID;
        }

        public static int GetCategoryID()
        {
            if (HttpContext.Current.Session["CategoryID"] == null)
            {
                return -1;
            }
            else
            {
                return (int)HttpContext.Current.Session["CategoryID"];
            }
        }

        public static void SetBreadCrumbState(string breadCrumbState)
        {
            HttpContext.Current.Session["BreadCrumbState"] = breadCrumbState;
        }

        public static string GetBreadCrumbState()
        {
            if (HttpContext.Current.Session["BreadCrumbState"] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session["BreadCrumbState"].ToString();
            }
        }

        public static void SetUpdateEventState(string eventState)
        {
            HttpContext.Current.Session["UpdateEventState"] = eventState;
        }

        public static string GetUpdateEventState()
        {
            if (HttpContext.Current.Session["UpdateEventState"] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session["UpdateEventState"].ToString();
            }
        }

        public static void SetUpdateEventID(int eventID)
        {
            HttpContext.Current.Session["UpdateEventID"] = eventID;
        }

        public static int GetUpdateEventID()
        {
            if (HttpContext.Current.Session["UpdateEventID"] == null)
            {
                return -1;
            }
            else
            {
                return (int)HttpContext.Current.Session["UpdateEventID"];
            }
        }

        public static void SetMenuState(string menuState)
        {
            HttpContext.Current.Session["MenuState"] = menuState;
        }

        public static string GetMenuState()
        {
            if (HttpContext.Current.Session["MenuState"] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session["MenuState"].ToString();
            }
        }

        public static void SetListAnswer(List<AnswerDetail> lstAnswerDetail)
        {
            HttpContext.Current.Session["ListAnsweDetail"] = lstAnswerDetail;
        }

        public static List<AnswerDetail> GetListAnswer()
        {
            if (HttpContext.Current.Session["ListAnsweDetail"] == null)
            {
                return null;
            }
            else
            {
                return (List<AnswerDetail>)HttpContext.Current.Session["ListAnsweDetail"];
            }
        }

        public static void SetQuestionNo(int questionNo)
        {
            HttpContext.Current.Session["QuestionNo"] = questionNo;
        }

        public static int GetQuestionNo()
        {
            if (HttpContext.Current.Session["QuestionNo"] == null)
            {
                return 0;
            }
            else
            {
                return  (int)HttpContext.Current.Session["QuestionNo"];
            }
        }

        public static void SetRubric(RubricDetail rubricDetail)
        {
            HttpContext.Current.Session["RubricDetail"] = rubricDetail;
        }

        public static RubricDetail GetRubric()
        {
            if (HttpContext.Current.Session["RubricDetail"] == null)
            {
                return null;
            }
            else
            {
                return (RubricDetail)HttpContext.Current.Session["RubricDetail"];
            }
        }

        public static void SetProjectID(int ProjectID)
        {
            HttpContext.Current.Session["ProjectID"] = ProjectID;
        }

        public static int GetProjectID()
        {
            if (HttpContext.Current.Session["ProjectID"] == null)
            {
                return -1;
            }
            else
            {
                return (int)HttpContext.Current.Session["ProjectID"];
            }

        }

        public static void SetUserID(int userID)
        {
            HttpContext.Current.Session["UserID"] = userID;
        }

        public static int GetUserID()
        {
            if (HttpContext.Current.Session["UserID"] == null)
            {
                return -1;
            }
            else
            {
                return (int)HttpContext.Current.Session["UserID"];
            }

        }

        public static void SetUserProfile(UserProfile userProfile)
        {
            HttpContext.Current.Session["UserProfile"] = userProfile;
        }

        public static UserProfile GetUserProfile()
        {
            if (HttpContext.Current.Session["UserProfile"] == null)
            {
                return null;
            }
            else
            {
                return (UserProfile) HttpContext.Current.Session["UserProfile"];
            }

        }

        public static void SetJudgeAndCategory(JudgeCategory judgeCategory)
        {
            HttpContext.Current.Session["JudgeCategory"] = judgeCategory;
        }

        public static JudgeCategory GetJudgeAndCategory()
        {
            if (HttpContext.Current.Session["JudgeCategory"] == null)
            {
                return null;
            }
            else
            {
                return (JudgeCategory)HttpContext.Current.Session["JudgeCategory"];
            }


        }
    }
}