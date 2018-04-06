using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AITAwards
{
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

        public List<ProjectDetail> GetListOfProjectBySupCategory(int categoryID)
        {
            try
            {
                List<ProjectDetail> lstProject = new List<ProjectDetail>();
                ProjectDetail projectDetail;
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT * FROM ");
                stringSQL.Append(TABLE_PROJECT);
                stringSQL.Append(" WHERE sup_cat_id = @subCatID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@subCatID", categoryID);

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
                    projectDetail.SupCatID = (int)reader["sup_cat_id"];
                    lstProject.Add(projectDetail);

                }

                cmd.Dispose();
                DatabaseClose();

                return lstProject;
            }
            catch (Exception ex)
            {
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

        public int DeleteJudgeInCategory(List<JudgeCategory> lstJudgeCat)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                for (int i = 0; i < lstJudgeCat.Count; i++)
                {
                    stringSQL = new StringBuilder();
                    stringSQL.Append("DELETE FROM ");
                    stringSQL.Append(TABLE_JUDGE_CAT);
                    stringSQL.Append(" WHERE user_id = @judgeID AND category_id = @categoryID;");

                    MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                    cmd.Parameters.AddWithValue("@categoryID", lstJudgeCat[i].CategoryID);
                    cmd.Parameters.AddWithValue("@judgeID", lstJudgeCat[i].UserID);

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

        public List<int> GetListProjectDoneByJudgeIDAndSubCategoryID(int judgeID, int categoryID)
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
    }
}