using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using MySql.Data.MySqlClient;


namespace AITAwards
{
    public class DatabaseHandle
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string VIEW_USER = "user_v";
        private const string TABLE_USER = "user_tb";
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


    }

    public class AdminDB : DatabaseHandle, IAdminDatabase
    {
        public int LoginbyUserAndPassword(string userName, string password)
        {
            try
            {
                return 1;
            }
            catch (Exception ex)
            {
                log.Error("Login Database",ex);
                return -1;
            }
        }
    }

    public class JudgeDB : DatabaseHandle, IJudgeDatabase
    {
        private const string TABLE_JUDGE_CAT = "judge_cat_tb";
        private const string TABLE_PROJECT = "project_tb";
        private const string TABLE_CRITERIA = "criteria_tb";
        private const string TABLE_LEVEL_CRITERIA = "level_criteria_tb";
        private const string TABLE_SCORE = "score_tb";

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
                stringSQL.Append("SELECT JUDGECAT.judge_cat_id, JUDGECAT.user_id, JUDGECAT.category_id, JUDGECAT.is_mark FROM ");
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
                    projectDetail.UploadAt = (DateTime)reader["upload_at"];
                    //projectDetail.ScoreID = (int)reader["score_id"];
                    //projectDetail.PathFile = reader["path_file"].ToString();
                    //projectDetail.TypeFileID = (int)reader["type_file_id"];
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
                    projectDetail.UploadAt = (DateTime)reader["upload_at"];
                    //projectDetail.ScoreID = (int)reader["score_id"];
                    //projectDetail.PathFile = reader["path_file"].ToString();
                    //projectDetail.TypeFileID = (int)reader["type_file_id"];
                    projectDetail.PathFile = "Images/Temp/ProjectImage.png";

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
    }
}