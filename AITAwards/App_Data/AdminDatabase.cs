using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AITAwards
{
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

        public List<TypeFileDetail> GetTypeFileDetail()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();
                List<TypeFileDetail> lstTypeFile = new List<TypeFileDetail>();
                TypeFileDetail typeFile = new TypeFileDetail();

                DatabaseOpen();
                stringSQL.Append("SELECT * FROM ");
                stringSQL.Append(TABLE_TYPEFILE);

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    typeFile = new TypeFileDetail();
                    typeFile.TypeFileID = (int)reader["type_file_id"];
                    typeFile.TypeFileName = reader["name"].ToString();

                    lstTypeFile.Add(typeFile);
                }

                cmd.Dispose();
                DatabaseClose();

                return lstTypeFile;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int AddProject(ProjectDetail projecDetail)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL = new StringBuilder();
                stringSQL.Append("INSERT INTO ");
                stringSQL.Append(TABLE_PROJECT);
                stringSQL.Append(" (name, description, category_id, path_file, pre_image, type_file_id)");
                stringSQL.Append(" VALUES (@name, @description ,@category_id, @path_file, @preImage, @type_file_id);");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@name", projecDetail.Name);
                cmd.Parameters.AddWithValue("@description", projecDetail.Description);
                cmd.Parameters.AddWithValue("@category_id", projecDetail.CategoryID);
                cmd.Parameters.AddWithValue("@path_file", projecDetail.PathFile);
                cmd.Parameters.AddWithValue("@type_file_id", projecDetail.TypeFileID);
                cmd.Parameters.AddWithValue("@preImage", projecDetail.PreImage);

                cmd.ExecuteNonQuery();

                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateProject(ProjectDetail porjectDetail)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();
                DatabaseOpen();
                
                stringSQL = new StringBuilder();
                stringSQL.Append("UPDATE ");
                stringSQL.Append(TABLE_PROJECT);
                stringSQL.Append(" SET name = @name, description = @description, category_id = @categoryID, path_file = @pathFile, type_file_id = @typeFileID , pre_image = @preImage");
                stringSQL.Append(" WHERE project_id = @project_id;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@description", porjectDetail.Description);
                cmd.Parameters.AddWithValue("@name", porjectDetail.Name);
                cmd.Parameters.AddWithValue("@categoryID", porjectDetail.CategoryID);
                cmd.Parameters.AddWithValue("@pathFile", porjectDetail.PathFile);
                cmd.Parameters.AddWithValue("@typeFileID", porjectDetail.TypeFileID);
                cmd.Parameters.AddWithValue("@project_id", porjectDetail.ProjectID);
                cmd.Parameters.AddWithValue("@preImage", porjectDetail.PreImage);

                cmd.ExecuteNonQuery();
                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int DeleteProject(int projectID)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();
                List<int> lstScoreID = new List<int>();


                DatabaseOpen();
                stringSQL = new StringBuilder();
                stringSQL.Append("SELECT score_id FROM ");
                stringSQL.Append(TABLE_SCORE);
                stringSQL.Append(" WHERE project_id = @projectID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@projectID", projectID);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lstScoreID.Add((int)reader["score_id"]);
                }

                DatabaseClose();
                DatabaseOpen();

                for (int i = 0; i < lstScoreID.Count; i++)
                {
                    stringSQL = new StringBuilder();
                    stringSQL.Append("DELETE FROM ");
                    stringSQL.Append(TABLE_SCORE);
                    stringSQL.Append(" WHERE score_id = @scoreID;");

                    cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                    cmd.Parameters.AddWithValue("@scoreID", lstScoreID[i]);

                    cmd.ExecuteNonQuery();
                }

                stringSQL = new StringBuilder();
                stringSQL.Append("DELETE FROM ");
                stringSQL.Append(TABLE_PROJECT);
                stringSQL.Append(" WHERE project_id = @projectID;");

                cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@projectID", projectID);

                cmd.ExecuteNonQuery();
                DatabaseClose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public string GetListOfProject()
        {
            try
            {
                List<ProjectDetail> lstProject = new List<ProjectDetail>();
                ProjectDetail projectDetail;
                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();
                stringSQL.Append("SELECT project_tb.project_id, project_tb.name, project_tb.description, category_tb.name AS cname FROM ");
                stringSQL.Append(TABLE_PROJECT);
                stringSQL.Append(" INNER JOIN category_tb ON project_tb.category_id = category_tb.category_id;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    projectDetail = new ProjectDetail();
                    projectDetail.ProjectID = (int)reader["project_id"];
                    projectDetail.Name = reader["name"].ToString();
                    projectDetail.Description = reader["description"].ToString();
                    projectDetail.CategoryName = reader["cname"].ToString();
                    lstProject.Add(projectDetail);

                }

                cmd.Dispose();
                DatabaseClose();
                string json = JsonConvert.SerializeObject(lstProject);
                return json;
            }
            catch (Exception ex)
            {
                log.Error("GetListOfProjectByCategory Database", ex);
                return null;
            }
        }

        public ProjectDetail GetProjectDetailByID(int projectID)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();
                ProjectDetail projectDetail = new ProjectDetail();

                DatabaseOpen();
                stringSQL.Append("SELECT P.project_id, P.name, P.description, P.category_id, P.path_file, P.pre_image, ");
                stringSQL.Append("P.type_file_id, C.event_id AS eventid FROM ");
                stringSQL.Append(TABLE_PROJECT + " AS P");
                stringSQL.Append(" INNER JOIN category_tb AS C ON P.category_ID = C.category_id");
                stringSQL.Append(" WHERE P.project_id = @projectID;");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@projectID", projectID);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    projectDetail.Name = reader["name"].ToString();
                    projectDetail.Description = reader["description"].ToString();
                    projectDetail.CategoryID = (int)reader["category_id"];
                    projectDetail.PathFile = reader["path_file"].ToString();
                    projectDetail.PreImage = reader["pre_image"].ToString();
                    projectDetail.TypeFileID = (int)reader["type_file_id"];
                    projectDetail.EventID = (int)reader["eventid"];
                    projectDetail.ProjectID = (int)reader["project_id"];
                }
                cmd.Dispose();
                DatabaseClose();

                return projectDetail;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int AddRubric(RubricDetail rubricDetail)
        {
            try
            {
                List<CriteriaDetail> lstCriteriaDetails = new List<CriteriaDetail>();
                List<LevelCriteria> lstLevelCriterias = new List<LevelCriteria>();
                CriteriaDetail criteriaDetail = new CriteriaDetail();
                LevelCriteria levelCriteria = new LevelCriteria();

                StringBuilder stringSQL = new StringBuilder();

                DatabaseOpen();

                for (int i = 0; i < rubricDetail.ListCriteriaDetail.Count; i++)
                {
                    criteriaDetail = new CriteriaDetail();
                    criteriaDetail = rubricDetail.ListCriteriaDetail[i];
                    for (int j = 0; j < criteriaDetail.LevelCritieria.Count; j++)
                    {
                        
                    }
                }

                stringSQL = new StringBuilder();
                stringSQL.Append("INSERT INTO ");
                stringSQL.Append(TABLE_PROJECT);
                stringSQL.Append(" (name, description, category_id, path_file, pre_image, type_file_id)");
                stringSQL.Append(" VALUES (@name, @description ,@category_id, @path_file, @preImage, @type_file_id);");

                MySqlCommand cmd = new MySqlCommand(stringSQL.ToString(), _conn);
                cmd.Parameters.AddWithValue("@name", projecDetail.Name);
                cmd.Parameters.AddWithValue("@description", projecDetail.Description);
                cmd.Parameters.AddWithValue("@category_id", projecDetail.CategoryID);
                cmd.Parameters.AddWithValue("@path_file", projecDetail.PathFile);
                cmd.Parameters.AddWithValue("@type_file_id", projecDetail.TypeFileID);
                cmd.Parameters.AddWithValue("@preImage", projecDetail.PreImage);

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