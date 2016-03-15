namespace Votations.NSurvey.Web.Security
{
    using System;
    using System.Collections;
    using System.Security.Claims;
    using Votations.NSurvey.Data;

    /// <summary>
    /// NSurveyPrincipal to access and manage the web admin 
    /// section of nsurvey.
    /// </summary>
    public class NSurveyFormPrincipal : ClaimsPrincipal, INSurveyPrincipal
    {
        private INSurveyIdentity _identity;
        private Hashtable _rights = new Hashtable();

        public NSurveyFormPrincipal(INSurveyIdentity identity, string[] rights)
        {
            this._identity = identity;
            if ((rights != null) && (rights.Length > 0))
            {
                foreach (string str in rights)
                {
                    if (str.Length > 0)
                    {
                        NSurveyRights key = (NSurveyRights)Enum.Parse(typeof(NSurveyRights), str);
                        this._rights.Add(key, key);
                    }
                }
            }
        }

        public bool HasRight(NSurveyRights right)
        {
            return this._rights.ContainsKey(right);
        }



        new public INSurveyIdentity Identity
        {
            get
            {
                return this._identity;
            }
        }
    }
}

