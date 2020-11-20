using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facebook_Anaytics
{
    public partial class UserControCase : UserControl
    {

        public IList<string> FRIENDS = new List<String>();
        public IList<string> HOMEPAGE = new List<String>();
        public IList<string> TAGS = new List<String>();
        public IList<string> PICTURESLIKES = new List<String>();
        public IList<string> PICTURESCOMMENTS = new List<String>();
        public IList<string> LIKEPAGES = new List<String>();
        public IList<string> COMMENTS = new List<String>();
        public IList<string> COMMENTSSCREENSHOTS = new List<String>();
        public IList<string> MESSENGER = new List<String>();
        public IList<string> FOLLOWERS = new List<String>();
        public IList<string> GROUPS = new List<String>();
        public List<string> ALL = new List<String>();
        public string Name = "";
        public string Path = "";
        public Image ImageProfile;
        public string url = "";
        public string username = "";
        public string idd = "";

        public UserControCase(Casedata casee)
        {
            InitializeComponent();
            pictureBox1.Image = casee.ImageProfile;
            labelProfil.Text = casee.Label;
            label1.Text = "Identifiants : " + casee.NbreId;
            Name = casee.Label;
            Path = casee.PathToFolder;
            ImageProfile = casee.ImageProfile;
            url = casee.Url;
            username = casee.Username;
            idd = casee.Id;
        }

        private void UserControCase_Load(object sender, EventArgs e)
        {

        }

        public int FillFRIENDS(IList<string> data)
        {
            FRIENDS = data;

            return FRIENDS.Count();
        }

        public Image GetProfilePicture()
        {
            return ImageProfile;
        }
        public int FillALL(List<string> data)
        {
            ALL = data;

            return ALL.Count();
        }

        public int FillHOMEPAGE(IList<string> data)
        {
            HOMEPAGE = data;

            return HOMEPAGE.Count();
        }

        public int FillTAGS(IList<string> data)
        {
            TAGS = data;

            return TAGS.Count();
        }

        public int FillPICTURESLIKES(IList<string> data)
        {
            PICTURESLIKES = data;

            return PICTURESLIKES.Count();
        }

        public int FillPICTURESCOMMENTS(IList<string> data)
        {
            PICTURESCOMMENTS = data;

            return PICTURESCOMMENTS.Count();
        }

        public int FillLIKEPAGES(IList<string> data)
        {
            LIKEPAGES = data;

            return LIKEPAGES.Count();
        }

        public int FillCOMMENTS(IList<string> data)
        {
            COMMENTS = data;

            return COMMENTS.Count();
        }

        public int FillCOMMENTSSCREENSHOTS(IList<string> data)
        {
            COMMENTSSCREENSHOTS = data;

            return COMMENTSSCREENSHOTS.Count();
        }

        public int FillMESSENGER(IList<string> data)
        {
            COMMENTSSCREENSHOTS = data;

            return COMMENTSSCREENSHOTS.Count();
        }

        public int FillFOLLOWERS(IList<string> data)
        {
            FOLLOWERS = data;

            return FOLLOWERS.Count();
        }

        public int FillGROUPS(IList<string> data)
        {
            GROUPS = data;

            return GROUPS.Count();
        }
    }
}
