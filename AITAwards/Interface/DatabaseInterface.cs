using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITAwards
{
    interface IAdminDatabase
    {
        int AddEvent(AITEvent aitEvent);
        int UpdateEvent(AITEvent aitEvent);
        AITEvent GetEventDetailByEventID(int eventID);
        List<AITEvent> GetListEvent();

        int AddCategory(AITCategories aitCategories);
        int UpdateCategory(AITCategories aitCategories);
        AITCategories GetCategoryByCategoryID(int categoryID);
        List<AITCategories> GetListCategoryByEventID(int eventID);

        int AddCriteria(CriteriaDetail criteriaDetail);
        int AddLevelCriteria(List<LevelCriteria> lstLevelCriteria);

        int UpdateCriteria(CriteriaDetail criteriaDetail);
        int UpdateLevelCriteria(List<LevelCriteria> lstLevelCriteria);

        List<CriteriaDetail> GetCriteriaDetailByCategoryID(int categoryID);
        List<LevelCriteria> GetLevelCriteriaByCriteriaDetail(int criteriaID);
    }

    interface IJudgeDatabase
    {
        List<JudgeCategory> GetListOfCategoriesByUserIDAndEventID(int userID, int eventID);
        List<ProjectDetail> GetListOfProjectByCategory(int categoryID);
        ProjectDetail GetProjectbyProjectID(int projectID);

        List<LevelCriteria> GetListOfLevelCriteriaByCriteriaID(int criteriaID);
        List<CriteriaDetail> GetCriteriaByCategoryID(int categoryID);

        List<int> GetListProjectDoneByJudgeIDAndCategoryID(int judgeID, int categoryID);

        int CheckJudgeCanAccessCategory(int categoryID, int userID);
        int InsertProjectScore(List<ScoreModel> scoreModel);
    }
}
