using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace AITAwards
{
    public class DatabaseHandle
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected const string VIEW_USER = "user_v";
        protected const string TABLE_USER = "user_tb";
        protected const string TABLE_JUDGE_CAT = "judge_cat_tb";
        protected const string TABLE_PROJECT = "project_tb";
        protected const string TABLE_CRITERIA = "criteria_tb";
        protected const string TABLE_LEVEL_CRITERIA = "level_criteria_tb";
        protected const string TABLE_SCORE = "score_tb";
        protected const string TABLE_EVENT = "event_tb";
        protected const string TABLE_INVITATION = "invitation_tb";
        protected const string TABLE_CATEGORY = "category_tb";
        protected const string TABLE_SCOREDONE = "scoredone_tb";
        protected const string TABLE_TYPEFILE = "type_file_tb";

        private string DATABASE_CONNECTION = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        protected MySqlConnection _conn;

        #region Database Open/Close
        protected void DatabaseOpen()
        {
            string connectionPath = DATABASE_CONNECTION;
            _conn = new MySqlConnection(connectionPath);
            _conn.Open();
        }

        protected void DatabaseClose()
        {
            _conn.Close();
        }
        #endregion

        public UserProfile LoginbyUserAndPassword(string userName, string password)
        {
            try
            {
                UserProfile userProfile = new UserProfile();
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT * FROM ");
                stringSQL.Append(TABLE_USER);
                stringSQL.Append(" WHERE username LIKE @userName AND password LIKE @password");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userProfile.UserID = (int)reader["user_id"];
                    userProfile.UserName = reader["username"].ToString();
                    userProfile.FirstName = reader["first_name"].ToString();
                    userProfile.LastName = reader["last_name"].ToString();
                    userProfile.UserLevel = (int)reader["user_level_id"];
                    userProfile.Email = reader["email"].ToString();
                }

                cmd.Dispose();
                DatabaseClose();

                return userProfile;
            }
            catch (Exception ex)
            {
                log.Error("Login Database", ex);
                return null;
            }
        }

        public string GetListJudge(int categoryID)
        {
            try
            {
                List<UserProfile> lstUserProfile = new List<UserProfile>();
                UserProfile userProfile = new UserProfile();
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT distinct user_tb.user_ID, user_tb.username, user_tb.email FROM ");
                stringSQL.Append(TABLE_USER);
                stringSQL.Append(" LEFT JOIN ");
                stringSQL.Append(TABLE_JUDGE_CAT);
                stringSQL.Append(" ON user_tb.user_ID = judge_cat_tb.user_id");
                stringSQL.Append(" WHERE user_tb.user_level_id = @userLevelID AND user_tb.user_ID NOT IN(SELECT user_id FROM judge_cat_tb WHERE category_id = @categoryID);");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@userLevelID", 2);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userProfile = new UserProfile();
                    userProfile.UserID = (int)reader["user_ID"];
                    userProfile.UserName = reader["username"].ToString();
                    userProfile.Email = reader["email"].ToString();
                    lstUserProfile.Add(userProfile);
                }

                string json = JsonConvert.SerializeObject(lstUserProfile);
                cmd.Dispose();
                DatabaseClose();

                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GetListJudgeInCat(int categoryID)
        {
            try
            {
                List<UserProfile> lstUserProfile = new List<UserProfile>();
                UserProfile userProfile = new UserProfile();
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT distinct user_tb.user_ID, user_tb.username, user_tb.email FROM ");
                stringSQL.Append(TABLE_USER);
                stringSQL.Append(" LEFT JOIN ");
                stringSQL.Append(TABLE_JUDGE_CAT);
                stringSQL.Append(" ON user_tb.user_ID = judge_cat_tb.user_id");
                stringSQL.Append(" WHERE judge_cat_tb.category_id = @categoryID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@userLevelID", 2);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userProfile = new UserProfile();
                    userProfile.UserID = (int)reader["user_ID"];
                    userProfile.UserName = reader["username"].ToString();
                    userProfile.Email = reader["email"].ToString();
                    lstUserProfile.Add(userProfile);
                }

                string json = JsonConvert.SerializeObject(lstUserProfile);
                cmd.Dispose();
                DatabaseClose();

                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<int> GetListJudgeID(int categoryID)
        {
            try
            {
                List<int> lstJudgeID = new List<int>();
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT distinct user_tb.user_ID FROM ");
                stringSQL.Append(TABLE_USER);
                stringSQL.Append(" LEFT JOIN ");
                stringSQL.Append(TABLE_JUDGE_CAT);
                stringSQL.Append(" ON user_tb.user_ID = judge_cat_tb.user_id");
                //stringSQL.Append(" WHERE judge_cat_tb.user_id IS NULL AND user_level_id = @userLevelID;");

                stringSQL.Append(" WHERE user_tb.user_level_id = @userLevelID AND user_tb.user_ID NOT IN(SELECT user_id FROM judge_cat_tb WHERE category_id = @categoryID);");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@userLevelID", 2);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstJudgeID.Add((int)reader["user_ID"]);
                }
                cmd.Dispose();
                DatabaseClose();

                return lstJudgeID;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<int> GetListJudgeIDInCat(int categoryID)
        {
            try
            {
                List<int> lstJudgeID = new List<int>();
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT user_ID FROM ");
                stringSQL.Append(TABLE_JUDGE_CAT);
                stringSQL.Append(" WHERE category_id = @categoryID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstJudgeID.Add((int)reader["user_ID"]);
                }
                cmd.Dispose();
                DatabaseClose();

                return lstJudgeID;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public int UpdateNewUser(string userName, string password,  int userID)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("UPDATE ");
                stringSQL.Append(TABLE_USER);
                stringSQL.Append(" SET username = @userName, password = @password, is_active = @isActive ");
                stringSQL.Append("WHERE user_ID = @userID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@isActive", 1);

                cmd.ExecuteNonQuery();

                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int AddNewUser(string userName, string password, string email, string guid)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL = new StringBuilder();
                stringSQL.Append("INSERT INTO ");
                stringSQL.Append(TABLE_USER);
                stringSQL.Append(" (username, password, email,guid)");
                stringSQL.Append(" VALUES (@username, @password, @email, @guid);");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@username", userName);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@guid", guid);

                cmd.ExecuteNonQuery();

                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public int CheckUserKey(string guid)
        {
            try
            {
                int userID = -1;
                int isActive = -1;
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT user_ID, is_active FROM ");
                stringSQL.Append(TABLE_USER);
                stringSQL.Append(" WHERE guid LIKE @guid;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@guid", guid);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userID = (int)reader["user_ID"];
                    isActive = (int)reader["is_active"];
                }

                if (isActive > 0)
                    userID = -1;
                cmd.Dispose();
                DatabaseClose();

                return userID;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int AddNewUserKey(string email, string guid)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL = new StringBuilder();
                stringSQL.Append("INSERT INTO ");
                stringSQL.Append(TABLE_INVITATION);
                stringSQL.Append(" (email, guid, is_gone, expire_at)");
                stringSQL.Append(" VALUES (@email, @guid, @is_gone, @expire_at);");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@guid", guid);
                cmd.Parameters.AddWithValue("@is_gone", 0);
                cmd.Parameters.AddWithValue("@expire_at", DateTime.Now);

                cmd.ExecuteNonQuery();

                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        

    }
   
}