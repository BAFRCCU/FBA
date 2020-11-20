using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook_Anaytics
{
    public class Casedata
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string PathToFolder { get; set; }
        public string PathToFile { get; set; }
        public string Label { get; set; }
        public string Username { get; set; }
        public string PathToPicture { get; set; }
        public string Category { get; set; }
        public Image ImageProfile {get; set;}
        public string NbreId { get; set; }
        public string GroupeName { get; set; }
        public string GroupeUsername { get; set; }
        public string GroupeUrl { get; set; }
        public List<Casedata> collection { get; set; }
    }
}
