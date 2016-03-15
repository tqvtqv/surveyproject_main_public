namespace Votations.NSurvey.UserProvider
{
    using System;
    using System.Security.Claims;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.Web.Security;

    /// <summary>
    /// To those who wants to implement an other user provider 
    /// than the default one
    /// </summary>
    public interface IUserProvider
    {
        INSurveyPrincipal CreatePrincipal(ClaimsIdentity user);
        UserData GetAllUsersList();
    }
}

