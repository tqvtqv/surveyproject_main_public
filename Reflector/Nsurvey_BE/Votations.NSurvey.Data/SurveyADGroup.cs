using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votations.NSurvey.Data
{
    public class SurveyADGroup
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string GroupName { get; set; }
        public int GroupLevel { get; set; }
    }
}
