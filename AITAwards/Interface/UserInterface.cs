using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITAwards
{
    interface IAdmin
    {

    }

    interface IJudging
    {
        string GetListOfCategories(int userID);
    }
}
