using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITAwards
{
    interface IAdminDatabase
    {
        int LoginbyUserAndPassword(string userName, string password);
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
