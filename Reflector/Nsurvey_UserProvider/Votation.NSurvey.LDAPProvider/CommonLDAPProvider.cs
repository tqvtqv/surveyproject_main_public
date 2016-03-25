using System;
using System.Collections.Generic;
using System.DirectoryServices;
using Votation.NSurvey.LDAPProvider;
using Votations.NSurvey.Data;
using System.Linq;

namespace Votations.NSurvey.LDAPProvider
{
    public class CommonLDAPProvider : ILDAPProvider
    {
        DirectoryEntry entry;
        DirectorySearcher ds;
        public CommonLDAPProvider()
        {
            this.entry = new DirectoryEntry("LDAP://10.1.2.4:389/DC=cqtd,DC=vnpt,DC=vn", "tranquocviet", "g!vkGox2a", AuthenticationTypes.Secure);
            this.ds = new DirectorySearcher(this.entry);
        }
        public IEnumerable<string> SearchAD(string filterPhase)
        {
            if (ds != null && !String.IsNullOrEmpty(filterPhase))
            {
                ds.SearchScope = SearchScope.Subtree;
                ds.PageSize = 100;
                ds.CacheResults = false;
                ds.Filter = "(" + filterPhase + ")";
                foreach (SearchResult item in ds.FindAll())
                    yield return (string)item.Properties["distinguishedName"][0];
            }
        }

        public IEnumerable<string> SearchOU(string filterPhase)
        {
            if (ds != null && !String.IsNullOrEmpty(filterPhase))
                return SearchAD("OU=" + filterPhase);
            else return null;
        }

        public IEnumerable<string> SearchCN(string filterPhase)
        {
            if (ds != null && !String.IsNullOrEmpty(filterPhase))
                return SearchAD("CN=" + filterPhase);
            else return null;
        }

        public IEnumerable<string> GetOUPath(string path, bool getFull)
        {
            if (!string.IsNullOrEmpty(path))
            {
                string[] arr = path.Split(new char[] { ',' });
                for (int i = 0; i < arr.Length; i++)
                    if (arr[i].StartsWith("OU"))
                        yield return getFull ? arr[i] : arr[i].Substring(arr[i].IndexOf("=") + 1);
            }
        }

        public string GetFirstPath(string path, bool getFull)
        {
            string result = null;
            if (!string.IsNullOrEmpty(path))
            {
                result = path.Split(new char[] { ',' })[0];
                result = getFull ? result : result.Substring(result.IndexOf("=") + 1);
            }
            return result;
        }

        public VoterADInfo GetVoterInfo(string userName)
        {
            VoterADInfo result = null;
            if (ds != null && !String.IsNullOrEmpty(userName))
            {
                ds.SearchScope = SearchScope.Subtree;
                ds.PageSize = 100;
                ds.CacheResults = false;
                ds.Filter = "(CN=" + userName + ")";
                SearchResult user = ds.FindOne();
                if (user != null)
                {
                    var path = (string)user.Properties["distinguishedName"][0];
                    var ouPaths = GetOUPath(path, true);
                    result = new VoterADInfo
                    {
                        UserName = userName,
                        Path = String.Join(",", ouPaths),
                        ADGroup = GetFirstPath(path, false),
                        ADGroupLevel = ouPaths.Count(),
                        Email = (string)user.Properties["mail"][0]
                    };
                }
            }
            return result;
        }
    }
}
