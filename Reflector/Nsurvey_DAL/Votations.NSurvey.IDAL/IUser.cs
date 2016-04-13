namespace Votations.NSurvey.IDAL
{
    using System;
    using System.Data;
    using Votations.NSurvey.Data;

    /// <summary>
    /// User interface for the user DAL.
    /// </summary>
    public interface IUser
    {
        void AddUser(NSurveyUserData newUser);
        void AddUser(NSurveyUserData newUser, string group, string parentGroup);
        void AddUserSettings(UserSettingData newSettings);
        void DeleteUserById(int userId);
        int GetAdminCount();
        UserData GetAllUsersList();
        NSurveyUserData GetNSurveyUserData(string userName, string password);
        NSurveyUserData GetUserById(int userId);
        int GetUserByIdFromUserName(string userName);
        int[] GetUserSecurityRights(int userId);
        UserSettingData GetUserSettings(int userId);
        bool IsAdministrator(int userId);
        void UpdateUser(NSurveyUserData updatedUser);
        void UpdateUserSettings(UserSettingData updatedSettings);

        DataSet GetChildGroups(int? parentId);
        DataSet GetGroupById(int groupid);
        DataSet GetGroupByName(string groupName, int? parentId);
        int DeleteGroupById(int groupid);
        int UpdateGroup(DataRow grouprow);
        int CreateGroup(DataRow grouprow);
    }
}

