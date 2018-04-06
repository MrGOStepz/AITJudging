using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITAwards
{
    public class ProjectDetail
    {
        private int projectID;
        private string name;
        private int supCatID;
        private int categoryID;
        private DateTime uploadAt;
        private int scoreID;
        private string pathFile;
        private int typeFileID;
        private string description;
        private string preImage;
        private string categoryName;
        private int eventID;

        public int EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }


        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }


        public string PreImage
        {
            get { return preImage; }
            set { preImage = value; }
        }


        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        public int TypeFileID
        {
            get { return typeFileID; }
            set { typeFileID = value; }
        }


        public string PathFile
        {
            get { return pathFile; }
            set { pathFile = value; }
        }


        public int ScoreID
        {
            get { return scoreID; }
            set { scoreID = value; }
        }


        public DateTime UploadAt
        {
            get { return uploadAt; }
            set { uploadAt = value; }
        }


        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }


        public int SupCatID
        {
            get { return supCatID; }
            set { supCatID = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }

    }
}