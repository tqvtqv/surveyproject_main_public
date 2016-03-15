namespace Votations.NSurvey.Web.Security
{
    using System;
    using System.Security.Principal;
    /// <summary>
    /// NSurveyIdentity interface for those who need to create 
    /// a custom nsurvey identity type
    /// </summary>
    public interface INSurveyIdentity : IIdentity
    {


        bool HasAllSurveyAccess { get; }

        bool IsAdmin { get; }


        int UserId { get; }

        string FirstName { get; }

        string LastName { get; }
    }
}

