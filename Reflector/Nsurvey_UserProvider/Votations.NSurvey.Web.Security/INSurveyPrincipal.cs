namespace Votations.NSurvey.Web.Security
{
    using System.Security.Principal;
    using Votations.NSurvey.Data;

    /// <summary>
    /// NSurveyPrincipal interface for those who wants 
    /// to create a custom nsurvey principal 
    /// </summary>
    public interface INSurveyPrincipal: IPrincipal
    {
        bool HasRight(NSurveyRights right);

        new INSurveyIdentity Identity { get; }
    }
}

