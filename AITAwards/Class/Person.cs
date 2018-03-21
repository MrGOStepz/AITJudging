using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITAwards.Class
{
    public class User
    {
        
    }

    public class Admin : User, IAdmin
    {

    }

    public class Judge : User, IJudging
    {
        public string GetListOfCategories(int userID)
        {
            throw new NotImplementedException();
        }
    }


}