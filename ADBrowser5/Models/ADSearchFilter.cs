using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBrowser5.Models
{
    public class ADSearchFilter
    {
        public string OUFilter { get; set; }
        public string CNFilter { get; set; }
        public SearchResultCollection Results { get; set; }
        public IList<String> Paths { get; set; }
    }
}
