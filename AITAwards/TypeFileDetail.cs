using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITAwards
{
    public class TypeFileDetail
    {
        private int typeFileID;
        private string typeFileName;

        public string TypeFileName
        {
            get { return typeFileName; }
            set { typeFileName = value; }
        }


        public int TypeFileID
        {
            get { return typeFileID; }
            set { typeFileID = value; }
        }

    }
}