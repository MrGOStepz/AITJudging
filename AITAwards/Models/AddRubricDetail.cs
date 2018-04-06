using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITAwards
{
    public class AddRubricDetail
    {
        public int EventID { get; set; }
        public int CategoryID { get; set; }
        public int CountCriteria { get; set; }
        public List<string> lstCriteria { get; set; }
        public List<int> lstLevelCriteria;
    }
}