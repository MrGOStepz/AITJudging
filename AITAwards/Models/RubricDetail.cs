using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITAwards
{
    public class RubricDetail
    {
        private int categoryID;
        private List<CriteriaDetail> listCriteriaDetail;

        public List<CriteriaDetail> ListCriteriaDetail
        {
            get { return listCriteriaDetail; }
            set { listCriteriaDetail = value; }
        }

        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }


    }

    public class CriteriaDetail
    {
        private int criteriaID;
        private string name;
        private int categoryID;
        private List<LevelCriteria> levelCriteria;

        public List<LevelCriteria> LevelCritieria
        {
            get { return levelCriteria; }
            set { levelCriteria = value; }
        }


        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public int CriteriaID
        {
            get { return criteriaID; }
            set { criteriaID = value; }
        }
    }

    public class LevelCriteria
    {
        private int levelCriteriaID;
        private int criteriaID;
        private string description;
        private float valueScore;

        public float ValueScore
        {
            get { return valueScore; }
            set { valueScore = value; }
        }


        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        public int CriteriaID
        {
            get { return criteriaID; }
            set { criteriaID = value; }
        }


        public int LevelCriteriaID
        {
            get { return levelCriteriaID; }
            set { levelCriteriaID = value; }
        }

    }
}