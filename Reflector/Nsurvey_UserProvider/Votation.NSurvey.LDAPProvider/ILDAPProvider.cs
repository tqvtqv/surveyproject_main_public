using System.Collections.Generic;
using Votations.NSurvey.Data;

namespace Votation.NSurvey.LDAPProvider
{
    public interface ILDAPProvider
    {
        VoterADInfo GetVoterInfo(string userName);
        IEnumerable<string> SearchAD(string filterPhase);
        IEnumerable<string> SearchOU(string filterPhase);
        IEnumerable<string> SearchCN(string filterPhase);

        IEnumerable<string> GetOUPath(string path, bool getFull);
        string GetFirstPath(string path, bool getFull);

    }
}
