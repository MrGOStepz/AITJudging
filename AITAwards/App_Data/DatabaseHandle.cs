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


        public int UpdateNewUser(string userName, string password, string email, int userID)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("UPDATE ");
                stringSQL.Append(TABLE_USER);
                stringSQL.Append(" SET username = @userName, password = @password, email = @email, is_active = @isActive ");
                stringSQL.Append("WHERE user_ID = @userID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);
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

    public class AdminDB : DatabaseHandle, IAdminDatabase
    {

        public int AddCriteria(CriteriaDetail criteriaDetail)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();

                stringSQL = new StringBuilder();
                stringSQL.Append("INSERT INTO ");
                stringSQL.Append(TABLE_CRITERIA);
                stringSQL.Append(" (name, category_id)");
                stringSQL.Append(" VALUES (@name, @categoryID);");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@name", criteriaDetail.Name);
                cmd.Parameters.AddWithValue("@categoryID", criteriaDetail.CategoryID);
                cmd.ExecuteNonQuery();

                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int AddEvent(AITEvent aitEvent)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();
                DatabaseOpen();

                stringSQL = new StringBuilder();
                stringSQL.Append("INSERT INTO ");
                stringSQL.Append(TABLE_EVENT);
                stringSQL.Append(" (name, start_at, end_at, address, is_active, path_image)");
                stringSQL.Append(" VALUES (@name, @startAt, @endAt, @address, @isActive, @path_image);");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@name", aitEvent.Name);
                cmd.Parameters.AddWithValue("@startAt", aitEvent.StartAt);
                cmd.Parameters.AddWithValue("@endAt", aitEvent.EndAt);
                cmd.Parameters.AddWithValue("@address", aitEvent.Address);
                cmd.Parameters.AddWithValue("@isActive", aitEvent.IsActive);
                cmd.Parameters.AddWithValue("@path_image", aitEvent.PathFile);
                cmd.ExecuteNonQuery();

                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int AddCategory(AITCategories aitCategories)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();
                DatabaseOpen();

                stringSQL = new StringBuilder();
                stringSQL.Append("INSERT INTO ");
                stringSQL.Append(TABLE_CATEGORY);
                stringSQL.Append(" (name, event_id, path_image)");
                stringSQL.Append(" VALUES (@name, @eventID, @pathImage);");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@name", aitCategories.Name);
                cmd.Parameters.AddWithValue("@eventID", aitCategories.EventID);
                cmd.Parameters.AddWithValue("@pathImage", aitCategories.PathFile);

                cmd.ExecuteNonQuery();

                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int AddLevelCriteria(List<LevelCriteria> lstLevelCriteria)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();
                DatabaseOpen();

                for (int i = 0; i < lstLevelCriteria.Count; i++)
                {
                    stringSQL = new StringBuilder();
                    stringSQL.Append("INSERT INTO ");
                    stringSQL.Append(TABLE_LEVEL_CRITERIA);
                    stringSQL.Append(" (criteria_id, description, value)");
                    stringSQL.Append(" VALUES (@criteriaID, @description, @value);");

                    MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                    cmd.Parameters.AddWithValue("@criteriaID", lstLevelCriteria[i].CriteriaID);
                    cmd.Parameters.AddWithValue("@description", lstLevelCriteria[i].Description);
                    cmd.Parameters.AddWithValue("@value", lstLevelCriteria[i].ValueScore);

                    cmd.ExecuteNonQuery();
                }

                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<CriteriaDetail> GetCriteriaDetailByCategoryID(int categoryID)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();
                List<CriteriaDetail> lstCriteriaDetail = new List<CriteriaDetail>();
                CriteriaDetail criteriaDetail = new CriteriaDetail();

                DatabaseOpen();
                stringSQL.Append("SELECT * FROM ");
                stringSQL.Append(TABLE_CRITERIA);
                stringSQL.Append(" WHERE category_id = @categoryID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    criteriaDetail = new CriteriaDetail();
                    criteriaDetail.CategoryID = (int)reader["category_id"];
                    criteriaDetail.Name = reader["name"].ToString();
                    criteriaDetail.CriteriaID = (int)reader["ceiteria_id"];
                    lstCriteriaDetail.Add(criteriaDetail);

                }
                cmd.Dispose();
                DatabaseClose();

                return lstCriteriaDetail;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public AITEvent GetEventDetailByEventID(int eventID)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();
                AITEvent aitEvent = new AITEvent();

                DatabaseOpen();
                stringSQL.Append("SELECT * FROM ");
                stringSQL.Append(TABLE_EVENT);
                stringSQL.Append(" WHERE event_id = @event_id;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@event_id", eventID);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    aitEvent.EventID = (int)reader["event_id"];
                    aitEvent.Name = reader["name"].ToString();
                    aitEvent.StartAt = (DateTime)reader["start_at"];
                    aitEvent.EndAt = (DateTime)reader["end_at"];
                    aitEvent.Address = reader["address"].ToString();
                    aitEvent.IsActive = (int)reader["is_active"];
                    aitEvent.PathFile = reader["path_image"].ToString();
                }
                cmd.Dispose();
                DatabaseClose();

                return aitEvent;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<LevelCriteria> GetLevelCriteriaByCriteriaDetail(int criteriaID)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();
                List<LevelCriteria> lstLevelCriteria = new List<LevelCriteria>();
                LevelCriteria levelCriteria;

                DatabaseOpen();
                stringSQL.Append("SELECT * FROM ");
                stringSQL.Append(TABLE_LEVEL_CRITERIA);
                stringSQL.Append(" WHERE criteria_id = @criteriaID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@criteriaID", criteriaID);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    levelCriteria = new LevelCriteria();

                    levelCriteria.LevelCriteriaID = (int)reader["level_criteria_id"];
                    levelCriteria.CriteriaID = (int)reader["criteria_id"];
                    levelCriteria.Description = reader["description"].ToString();
                    levelCriteria.ValueScore = (float)reader["value"];

                    lstLevelCriteria.Add(levelCriteria);
                }
                cmd.Dispose();
                DatabaseClose();

                return lstLevelCriteria;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<AITCategories> GetListCategoryByEventID(int eventID)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();
                List<AITCategories> lstaitCateories = new List<AITCategories>();
                AITCategories aitCategories;

                DatabaseOpen();
                stringSQL.Append("SELECT * FROM ");
                stringSQL.Append(TABLE_CATEGORY);
                stringSQL.Append(" WHERE event_id = @eventID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@eventID", eventID);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    aitCategories = new AITCategories();
                    aitCategories.CategoryID = (int)reader["category_id"];
                    aitCategories.Name = reader["name"].ToString();
                    aitCategories.EventID = (int)reader["event_id"];
                    aitCategories.PathFile = reader["path_image"].ToString();

                    lstaitCateories.Add(aitCategories);

                }
                cmd.Dispose();
                DatabaseClose();

                return lstaitCateories;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<AITEvent> GetListEvent()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();
                List<AITEvent> lstAITEvent = new List<AITEvent>();
                AITEvent aitEvent = new AITEvent();

                DatabaseOpen();
                stringSQL.Append("SELECT * FROM ");
                stringSQL.Append(TABLE_EVENT);

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    aitEvent = new AITEvent();
                    aitEvent.EventID = (int)reader["event_id"];
                    aitEvent.Name = reader["name"].ToString();

                    aitEvent.StartAt = (DateTime)reader["start_at"];
                    aitEvent.EndAt = (DateTime)reader["end_at"];
                    aitEvent.Address = reader["address"].ToString();
                    aitEvent.IsActive = (int)reader["is_active"];
                    aitEvent.PathFile = reader["path_image"].ToString();

                    lstAITEvent.Add(aitEvent);

                }
                cmd.Dispose();
                DatabaseClose();

                return lstAITEvent;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int UpdateEvent(AITEvent aitEvent)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("UPDATE ");
                stringSQL.Append(TABLE_EVENT);
                stringSQL.Append(" SET name = @name, start_at = @startAt, end_at = @endAt, address = @address, is_active = @isActive" +
                    ", path_image = @pathImage ");
                stringSQL.Append("WHERE event_id = @eventID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@name", aitEvent.Name);
                cmd.Parameters.AddWithValue("@startAt", aitEvent.StartAt);
                cmd.Parameters.AddWithValue("@endAt", aitEvent.EndAt);
                cmd.Parameters.AddWithValue("@address", aitEvent.Address);
                cmd.Parameters.AddWithValue("@isActive", aitEvent.IsActive);
                cmd.Parameters.AddWithValue("@pathImage", aitEvent.PathFile);
                cmd.Parameters.AddWithValue("@eventID", aitEvent.EventID);
                cmd.ExecuteNonQuery();

                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateCriteria(CriteriaDetail criteriaDetail)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("UPDATE ");
                stringSQL.Append(TABLE_CRITERIA);
                stringSQL.Append(" SET name = @name, category_id = @categoryID");
                stringSQL.Append("WHERE criteria_id = @criteriaID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@criteriaID", criteriaDetail.CriteriaID);
                cmd.Parameters.AddWithValue("@name", criteriaDetail.Name);
                cmd.Parameters.AddWithValue("@categoryID", criteriaDetail.CategoryID);

                cmd.ExecuteNonQuery();

                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateCategory(AITCategories aitCategories)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("UPDATE ");
                stringSQL.Append(TABLE_CATEGORY);
                stringSQL.Append(" SET name = @name, event_id = @eventID, path_image = @pathImage");
                stringSQL.Append(" WHERE category_id = @categoryID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@categoryID", aitCategories.CategoryID);
                cmd.Parameters.AddWithValue("@name", aitCategories.Name);
                cmd.Parameters.AddWithValue("@eventID", aitCategories.EventID);
                cmd.Parameters.AddWithValue("@pathImage", aitCategories.PathFile);

                cmd.ExecuteNonQuery();
                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateLevelCriteria(List<LevelCriteria> lstLevelCriteria)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                for (int i = 0; i < lstLevelCriteria.Count; i++)
                {
                    stringSQL = new StringBuilder();
                    stringSQL.Append("UPDATE ");
                    stringSQL.Append(TABLE_LEVEL_CRITERIA);
                    stringSQL.Append(" SET description = @description, criteria_id = @criteriaID, value = @value");
                    stringSQL.Append("WHERE level_criteria_id = @levelCriteriaID;");

                    MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                    cmd.Parameters.AddWithValue("@description", lstLevelCriteria[i].Description);
                    cmd.Parameters.AddWithValue("@criteriaID", lstLevelCriteria[i].CriteriaID);
                    cmd.Parameters.AddWithValue("@value", lstLevelCriteria[i].ValueScore);
                    cmd.Parameters.AddWithValue("@levelCriteriaID", lstLevelCriteria[i].LevelCriteriaID);

                    cmd.ExecuteNonQuery();
                }

                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public AITCategories GetCategoryByCategoryID(int categoryID)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();
                AITCategories aITCategories = new AITCategories();

                DatabaseOpen();
                stringSQL.Append("SELECT * FROM ");
                stringSQL.Append(TABLE_CATEGORY);
                stringSQL.Append(" WHERE category_id = @category_id;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@category_id", categoryID);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    aITCategories.EventID = (int)reader["event_id"];
                    aITCategories.Name = reader["name"].ToString();
                    aITCategories.CategoryID = (int)reader["category_id"];
                    aITCategories.PathFile = reader["path_image"].ToString();
                }
                cmd.Dispose();
                DatabaseClose();

                return aITCategories;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public class JudgeDB : DatabaseHandle, IJudgeDatabase
    {
        public int CheckJudgeCanAccessCategory(int categoryID, int userID)
        {
            try
            {
                int check = -1;
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT * FROM ");
                stringSQL.Append(TABLE_JUDGE_CAT);
                stringSQL.Append(" WHERE category_id = @categoryID AND user_id = @user_id;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    check = (reader.HasRows) ? 1 : -1;
                }
                cmd.Dispose();
                DatabaseClose();

                return check;
            }
            catch (Exception ex)
            {
                log.Error("GetListOfProjectByCategory Database", ex);
                return -1;
            }
        }

        public List<CriteriaDetail> GetCriteriaByCategoryID(int categoryID)
        {
            try
            {
                List<CriteriaDetail> lstCriteriaDetail = new List<CriteriaDetail>();
                CriteriaDetail criteriaDetail;
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT * FROM ");
                stringSQL.Append(TABLE_CRITERIA);
                stringSQL.Append(" WHERE category_id = @categoryID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    criteriaDetail = new CriteriaDetail();
                    criteriaDetail.CriteriaID = (int)reader["criteria_id"];
                    criteriaDetail.Name = reader["name"].ToString();
                    criteriaDetail.CategoryID = (int)reader["category_id"];
                    lstCriteriaDetail.Add(criteriaDetail);
                }

                cmd.Dispose();
                DatabaseClose();

                return lstCriteriaDetail;
            }
            catch (Exception ex)
            {
                log.Error("GetListOfCategoriesByUserIDAndEventID Database", ex);
                return null;
            }
        }

        public List<int> GetListProjectDoneByJudgeIDAndCategoryID(int judgeID, int categoryID)
        {
            try
            {
                List<int> lstProjectID = new List<int>();
                StringBuilder stringSQL = new StringBuilder();
                DatabaseOpen();
                stringSQL.Append("SELECT DISTINCT SC.project_id FROM ");
                stringSQL.Append(TABLE_SCORE + " AS SC");
                stringSQL.Append(" INNER JOIN ");
                stringSQL.Append(TABLE_CRITERIA + " AS CR");
                stringSQL.Append(" ON SC.criteria_id = CR.criteria_id");
                stringSQL.Append(" WHERE SC.user_id = @judgeID AND CR.category_id = @categoryID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@judgeID", judgeID);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("project_id")))
                        lstProjectID.Add((int)reader["project_id"]);
                }

                cmd.Dispose();
                DatabaseClose();

                return lstProjectID;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<JudgeCategory> GetListOfCategoriesByUserIDAndEventID(int userID, int eventID)
        {
            try
            {
                List<JudgeCategory> lstCategories = new List<JudgeCategory>();
                JudgeCategory judgeCategory;
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT JUDGECAT.judge_cat_id, JUDGECAT.user_id, JUDGECAT.category_id, JUDGECAT.is_mark, CATEGORY.path_image FROM ");
                stringSQL.Append(TABLE_JUDGE_CAT + " AS JUDGECAT");
                stringSQL.Append(" INNER JOIN category_tb AS CATEGORY ON CATEGORY.category_id = JUDGECAT.category_id");
                stringSQL.Append(" WHERE CATEGORY.event_id = @eventID AND JUDGECAT.user_id = @userID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@eventID", eventID);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    judgeCategory = new JudgeCategory();
                    judgeCategory.JudgeCategoryID = (int)reader["judge_cat_id"];
                    judgeCategory.UserID = (int)reader["user_id"];
                    judgeCategory.CategoryID = (int)reader["category_id"];
                    judgeCategory.IsMark = (int)reader["is_mark"];
                    judgeCategory.PathImage = reader["path_image"].ToString();
                    lstCategories.Add(judgeCategory);

                }

                cmd.Dispose();
                DatabaseClose();

                return lstCategories;
            }
            catch (Exception ex)
            {
                log.Error("GetListOfCategoriesByUserIDAndEventID Database", ex);
                return null;
            }
        }

        public List<LevelCriteria> GetListOfLevelCriteriaByCriteriaID(int criteriaID)
        {
            try
            {
                List<LevelCriteria> lstLevelCriteria = new List<LevelCriteria>();
                LevelCriteria levelCriteria;
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT * FROM ");
                stringSQL.Append(TABLE_LEVEL_CRITERIA);
                stringSQL.Append(" WHERE criteria_id = @criteriaID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@criteriaID", criteriaID);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    levelCriteria = new LevelCriteria();
                    levelCriteria.LevelCriteriaID = (int)reader["level_criteria_id"];
                    levelCriteria.CriteriaID = (int)reader["criteria_id"];
                    levelCriteria.Description = reader["description"].ToString();
                    levelCriteria.ValueScore = (float)reader["value"];
                    
                    lstLevelCriteria.Add(levelCriteria);
                }

                cmd.Dispose();
                DatabaseClose();

                return lstLevelCriteria;
            }
            catch (Exception ex)
            {
                log.Error("GetListOfCategoriesByUserIDAndEventID Database", ex);
                return null;
            }
        }

        public List<ProjectDetail> GetListOfProjectByCategory(int categoryID)
        {
            try
            {
                List<ProjectDetail> lstProject = new List<ProjectDetail>();
                ProjectDetail projectDetail;
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT * FROM ");
                stringSQL.Append(TABLE_PROJECT);
                stringSQL.Append(" WHERE category_id = @categoryID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //TODO Resolve Comment
                    projectDetail = new ProjectDetail();
                    projectDetail.ProjectID = (int)reader["project_id"];
                    projectDetail.Name = reader["name"].ToString();
                    projectDetail.CategoryID = (int)reader["category_id"];
                    //projectDetail.UserID = (int)reader["user_id"];
                    //projectDetail.UploadAt = (DateTime)reader["upload_at"];
                    //projectDetail.ScoreID = (int)reader["score_id"];
                    projectDetail.PathFile = reader["path_file"].ToString();
                    projectDetail.TypeFileID = (int)reader["type_file_id"];
                    lstProject.Add(projectDetail);

                }

                cmd.Dispose();
                DatabaseClose();

                return lstProject;
            }
            catch (Exception ex)
            {
                log.Error("GetListOfProjectByCategory Database", ex);
                return null;
            }
        }

        public ProjectDetail GetProjectbyProjectID(int projectID)
        {
            try
            {
                ProjectDetail projectDetail = new ProjectDetail();
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT * FROM ");
                stringSQL.Append(TABLE_PROJECT);
                stringSQL.Append(" WHERE project_id = @projectID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@projectID", projectID);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    projectDetail = new ProjectDetail();
                    projectDetail.ProjectID = (int)reader["project_id"];
                    projectDetail.Name = reader["name"].ToString();
                    projectDetail.CategoryID = (int)reader["category_id"];
                    //projectDetail.UserID = (int)reader["user_id"];
                    //projectDetail.UploadAt = (DateTime)reader["upload_at"];
                    //projectDetail.ScoreID = (int)reader["score_id"];
                    projectDetail.PathFile = reader["path_file"].ToString();
                    projectDetail.Description = reader["description"].ToString();
                    projectDetail.TypeFileID = (int)reader["type_file_id"];

                }

                cmd.Dispose();
                DatabaseClose();

                return projectDetail;
            }
            catch (Exception ex)
            {
                log.Error("GetListOfProjectByCategory Database", ex);
                return null;
            }
        }

        public int InsertProjectScore(List<ScoreModel> lstScoreModel)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                for (int i = 0; i < lstScoreModel.Count; i++)
                {
                    stringSQL = new StringBuilder();
                    stringSQL.Append("INSERT INTO ");
                    stringSQL.Append(TABLE_SCORE);
                    stringSQL.Append(" (project_id, score, criteria_id, user_id, comment)");
                    stringSQL.Append(" VALUES (@projectID, @score, @criteriaID, @userID, @comment);");

                    MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                    cmd.Parameters.AddWithValue("@projectID", lstScoreModel[i].ProjectID);
                    cmd.Parameters.AddWithValue("@score", lstScoreModel[i].Score);
                    cmd.Parameters.AddWithValue("@criteriaID", lstScoreModel[i].CriteriaID);
                    cmd.Parameters.AddWithValue("@userID", lstScoreModel[i].UserID);
                    cmd.Parameters.AddWithValue("@comment", lstScoreModel[i].Comment);
                    cmd.ExecuteNonQuery();
                }

                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int AddJudgeByCategory(List<JudgeCategory> lstJudgeCat)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                for (int i = 0; i < lstJudgeCat.Count; i++)
                {
                    stringSQL = new StringBuilder();
                    stringSQL.Append("INSERT INTO ");
                    stringSQL.Append(TABLE_JUDGE_CAT);
                    stringSQL.Append(" (user_id, category_id)");
                    stringSQL.Append(" VALUES (@userID, @categoryID);");

                    MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                    cmd.Parameters.AddWithValue("@userID", lstJudgeCat[i].UserID);
                    cmd.Parameters.AddWithValue("@categoryID", lstJudgeCat[i].CategoryID);

                    cmd.ExecuteNonQuery();
                }

                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public float GetTotalScoreByCategoryID(List<int> lstCriteria)
        {
            try
            {
                float TotalScore = 0.0f;
                List<int> lstCriteriaID = new List<int>();
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                for (int i = 0; i < lstCriteria.Count; i++)
                {
                    stringSQL = new StringBuilder();
                    stringSQL.Append("SELECT MAX(value) FROM ");
                    stringSQL.Append(TABLE_LEVEL_CRITERIA);
                    stringSQL.AppendFormat(" WHERE criteria_id = {0} ", lstCriteria[i]);

                    MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);

                    TotalScore += (float)cmd.ExecuteScalar();

                }

                DatabaseClose();

                return TotalScore;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    

        public double GetTotalScoreByProjectID(int ProjectID)
        {
            try
            {
                float TotalScore = 0.0f;
                double tt = 0;
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT AVG(score) FROM ");
                stringSQL.Append(TABLE_SCOREDONE);
                stringSQL.Append(" WHERE project_id = @ProjectID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);


                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("AVG(score)")))
                    {
                        var t = reader["AVG(score)"];
                        tt = Convert.ToDouble(t);
                    }
                    else
                        tt = 0;

                }

                cmd.Dispose();
                DatabaseClose();

                return tt;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int CountJudgeScore(int projectID)
        {
            throw new NotImplementedException();
        }

        public List<int> GetCriteriaIDByCategory(int categoryID)
        {
            try
            {
                List<int> lstCriteriaID = new List<int>();
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT criteria_id FROM ");
                stringSQL.Append(TABLE_CRITERIA);
                stringSQL.Append(" WHERE category_id = @categoryID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstCriteriaID.Add((int)reader["criteria_id"]);

                }

                cmd.Dispose();
                DatabaseClose();

                return lstCriteriaID;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int AddTotalScoreProjectByJudge(int projectID, int judgeID, float score)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL = new StringBuilder();
                stringSQL.Append("INSERT INTO ");
                stringSQL.Append(TABLE_SCOREDONE);
                stringSQL.Append(" (project_id, judge_id, score)");
                stringSQL.Append(" VALUES (@projectID, @judgeID ,@score);");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@projectID", projectID);
                cmd.Parameters.AddWithValue("@judgeID", judgeID);
                cmd.Parameters.AddWithValue("@score", score);

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