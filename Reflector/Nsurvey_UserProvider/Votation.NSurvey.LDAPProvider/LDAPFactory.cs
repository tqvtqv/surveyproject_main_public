using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;

namespace Votation.NSurvey.LDAPProvider
{
    public class LDAPFactory
    {
        /// <summary>
        /// Creates a new instance of the Userprovider class as 
        /// specified in the .config file
        /// </summary>
        public static ILDAPProvider Create()
        {
            NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
            if (config == null)
            {
                config = ConfigurationManager.AppSettings;
            }
            string typeName = config["LDAPProviderClass"];
            if (String.IsNullOrEmpty(typeName)) typeName = "Votations.NSurvey.UserProvider.CommonLDAPProvider";
            string assemblyString = config["LDAPProviderAssembly"];
            if (String.IsNullOrEmpty(typeName)) typeName = "Votations.NSurvey.LDAPProvider";
            return (ILDAPProvider)Assembly.Load(assemblyString).CreateInstance(typeName);
        }
    }
}
