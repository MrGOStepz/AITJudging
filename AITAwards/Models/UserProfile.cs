using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITAwards
{
    public class UserProfile
    {
        private int userID;
        private string userName;
        private string firstName;
        private string lastName;
        private int userLevel;
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }


        public int UserLevel
        {
            get { return userLevel; }
            set { userLevel = value; }
        }


        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }


        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }



        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }


        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

    }
}