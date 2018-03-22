using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITAwards
{
    public class ScoreModel
    {
        public int ScoreID { get; set; }
        public int ProjectID { get; set; }
        public float Score { get; set; }
        public int CriteriaID { get; set; }
        public int UserID { get; set; }
        public string Comment { get; set; }

    }
}