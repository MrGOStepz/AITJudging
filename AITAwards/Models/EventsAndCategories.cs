using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITAwards
{
    public class AITEvent
    {
        private int eventID;
        private string name;
        private DateTime startAt;
        private DateTime endAt;
        private string address;
        private int isActive;
        private string pathFile;

        public AITEvent()
        {
            startAt = DateTime.Now;
            endAt = DateTime.Now;
        }

        public string PathFile
        {
            get { return pathFile; }
            set { pathFile = value; }
        }


        public int IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }


        public string Address
        {
            get { return address; }
            set { address = value; }
        }


        public DateTime EndAt
        {
            get { return endAt; }
            set { endAt = value; }
        }


        public DateTime StartAt
        {
            get { return startAt; }
            set { startAt = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public int EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }

    }

    public class AITCategories
    {
        private int eventID;
        private int categoryID;
        private string name;
        private string pathFile;

        public string PathFile
        {
            get { return pathFile; }
            set { pathFile = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }


        public int EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }

    }

    public class JudgeCategory
    {
        private int judgeCategoryID;
        private int userID;
        private int categoryID;
        private int isMark;

        public int IsMark
        {
            get { return isMark; }
            set { isMark = value; }
        }


        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }


        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }


        public int JudgeCategoryID
        {
            get { return judgeCategoryID; }
            set { judgeCategoryID = value; }
        }

    }
}