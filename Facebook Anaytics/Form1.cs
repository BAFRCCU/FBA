using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Facebook_Anaytics
{
    public partial class Form1 : Form
    {
        public string pathToSave = AppDomain.CurrentDomain.BaseDirectory;
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
        public Dictionary<string,string> ALLTARGETS = new Dictionary<string,string>();
        IList<string> lignesForSavingCase = new List<string>();
        public List<string> sortedListForSearching = new System.Collections.Generic.List<string>();
        public string USERNAME = "Machine : " + Environment.MachineName + " - OS Version : " + Environment.OSVersion + " - User : " + Environment.UserName + " - UserDomain : " + Environment.UserDomainName;
        public string ForAnalytics = "";
        public UserControl1 userControl = new UserControl1();
        public string UsernameForSaving = "";
        public string IDForSaving = "";
        public string ImageProfileForSaving = "";
        public int hits = 0;
        public Initcontrol _init;
        //Dictionary<string, Analytics> Analytics = new Dictionary<string, Analytics>();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        public Form1()
        {
            InitializeComponent();
            //_init = init;
            //_init.Refresh();
            //_init.Show();
            //_init.Update();
            toolTip1.SetToolTip(button3, "Rechercher dans plusieurs ecases");

            if (File.Exists(pathToSave + "\\TXT\\ForAnalytics.txt"))
            {
                string[] lignes = File.ReadAllLines(pathToSave + "\\TXT\\ForAnalytics.txt");
                ForAnalytics = File.ReadAllText(pathToSave + "\\TXT\\ForAnalytics.txt");

                var sorted = Directory.GetFiles(pathToSave, "*.fbv").Select(fn => new FileInfo(fn)).OrderBy(f => f.CreationTime);
                FileInfo[] fichiers = sorted.ToArray();

                foreach (FileInfo fichier in fichiers)
                {

                    Import(fichier.FullName);

                }

                InitializeGridTargets();
                FillComments();

            }
            else
            {
                //Form2 formallcases = new Form2();
                //formallcases.Show();
                //this.Visible = false;
                //return;
                //this.Close();

                dataGridViewTargets.Visible = false;
                dataGridViewResults.Visible = false;
                labelUSERNAMES.Visible = false;
                label1.Visible = false;
                button2.Visible = false;
                pictureBoxwaiting.Visible = false;
                Main.Controls.Remove(Main.Controls["tabPage2"]);
                Main.Controls.Remove(Main.Controls["tabPage3"]);

            }

            //var sorted = Directory.GetFiles(pathToSave, "*.fbv").Select(fn => new FileInfo(fn)).OrderBy(f => f.CreationTime);
            //FileInfo[] fichiers = sorted.ToArray();

            //foreach (FileInfo fichier in fichiers)
            //{

            //    Import(fichier.FullName);

            //}

            //InitializeGridTargets();
            //FillComments();

            
        }

        public void Import(string fichier)
        {

            string sauvegarde = File.ReadAllText(fichier);

            string[] ecase = sauvegarde.Substring(sauvegarde.IndexOf("<Case>\n") + 7, (sauvegarde.IndexOf("</Case>\n") - (sauvegarde.IndexOf("<Case>\n") + 7))).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)[0].Split(';');
            string[] pictureProfile = sauvegarde.Substring(sauvegarde.IndexOf("<PictureProfile>\n") + 16, (sauvegarde.IndexOf("</PictureProfile>\n") - (sauvegarde.IndexOf("<PictureProfile>\n") + 16))).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                //string[] datagridviewmessenger = sauvegarde.Substring(sauvegarde.IndexOf("<DataGridViewMessenger>\n") + 24).Split(new string[] { "</DataGridViewMessenger>\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] datagridviewmessenger = sauvegarde.Substring(sauvegarde.IndexOf("<DataGridViewMessenger>\n") + 24, (sauvegarde.IndexOf("</DataGridViewMessenger>\n") - (sauvegarde.IndexOf("<DataGridViewMessenger>\n") + 24))).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] datagridviewFriends = sauvegarde.Substring(sauvegarde.IndexOf("<DataGridViewFriends>\n") + 21, (sauvegarde.IndexOf("</DataGridViewFriends>\n") - (sauvegarde.IndexOf("<DataGridViewFriends>\n") + 21))).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] datagridviewFollowers = sauvegarde.Substring(sauvegarde.IndexOf("<DataGridViewFollowers>\n") + 24, (sauvegarde.IndexOf("</DataGridViewFollowers>\n") - (sauvegarde.IndexOf("<DataGridViewFollowers>\n") + 24))).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] datagridviewLikes = sauvegarde.Substring(sauvegarde.IndexOf("<DataGridViewLikes>\n") + 19, (sauvegarde.IndexOf("</DataGridViewLikes>\n") - (sauvegarde.IndexOf("<DataGridViewLikes>\n") + 19))).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                IDForSaving = ecase[0].Trim().Replace("https://www.facebook.com/", "") + " - ID : " + ecase[5].Trim();

            }
            catch
            {

            }

            
            try
            {
                
                labelID.Text = "Recherches dans le profil de " + ecase[0].Trim().Replace("https://www.facebook.com/","") + " - ID : " + ecase[5].Trim();
                if (labelID.Text != "")
                {
                    labelID.Visible = true;
                    labelID.Refresh();

                }
                else
                    labelID.Visible = false;
            }
            catch
            {
                labelID.Visible = false;

            }


            if(pictureProfile.Count() > 0)
            if (pictureProfile[0] != "label3")
            {
                try
                {
                    pictureBox30.Image = Image.FromFile(pictureProfile[0]);
                    
                }
                catch (Exception ex)
                {

                }

            }
           

            


            
            



        }



        public bool IsTargetFound(string target)
        {
            if (File.Exists(pathToSave + "\\TXT\\ForAnalytics.txt"))
            {
               


                if (ForAnalytics.Contains(target) )
                    return true;
            }

            return false;
        }
        //public string FindResults (Dictionary<string, string> ALLTARGETS)
        //{
        //    //on parcourt toutes les lignes et récupère les résultats par rapport aux catégories
        //    if (File.Exists(pathToSave + "\\TXT\\ForAnalytics.txt"))
        //    {
        //        string[] lignes = File.ReadAllLines(pathToSave + "\\TXT\\ForAnalytics.txt");

        //        foreach (string li in lignes)
        //        {
        //            string Category = li.Split(';')[0];
        //            switch (Category)
        //            {
        //                case "FRIENDS":
        //                    FRIENDS.Add(li);
        //                    break;

        //                case "HOMEPAGE":
        //                    HOMEPAGE.Add(li);
        //                    break;

        //                case "TAGS":
        //                    TAGS.Add(li);
        //                    break;

        //                case "PICTURESLIKES":
        //                    PICTURESLIKES.Add(li);
        //                    break;

        //                case "PICTURESCOMMENTS":
        //                    PICTURESCOMMENTS.Add(li);
        //                    break;

        //                case "LIKEPAGES":
        //                    LIKEPAGES.Add(li);
        //                    break;

        //                case "COMMENTS":
        //                    COMMENTS.Add(li);
        //                    break;

        //                case "COMMENTSSCREENSHOTS":
        //                    COMMENTSSCREENSHOTS.Add(li);
        //                    break;

        //                case "MESSENGER":
        //                    MESSENGER.Add(li);
        //                    break;

        //                case "FOLLOWERS":
        //                    FOLLOWERS.Add(li);
        //                    break;

        //                case "GROUPS":
        //                    GROUPS.Add(li);
        //                    break;
        //            }
        //        }

        //        //maintenant par rapport aux resultats

        //        if (FRIENDS.Count > 0)
        //        {
        //            FindFRIENDS(ALLTARGETS);
        //        }

        //        if (HOMEPAGE.Count > 0)
        //        {
        //            labelJournal.Text = "JOURNAL : " + FindHOMEPAGE(ALLTARGETS);
        //        }

        //        if (TAGS.Count > 0)
        //        {
        //            FindTAGS(ALLTARGETS);
        //        }

        //        if (PICTURESLIKES.Count > 0)
        //        {
        //            FindPICTURESLIKES(ALLTARGETS);
        //        }

        //        if (PICTURESCOMMENTS.Count > 0)
        //        {
        //            FindPICTURESCOMMENTS(ALLTARGETS);
        //        }

        //        if (LIKEPAGES.Count > 0)
        //        {
        //            FindLIKEPAGES(ALLTARGETS);
        //        }

        //        if (COMMENTS.Count > 0)
        //        {
        //            FindCOMMENTS(ALLTARGETS);
        //        }

        //        if (COMMENTSSCREENSHOTS.Count > 0)
        //        {
        //            FindCOMMENTSSCREENSHOTS(ALLTARGETS);
        //        }

        //        if (MESSENGER.Count > 0)
        //        {
        //            FindMESSENGERS(ALLTARGETS);
        //        }

        //        if (FOLLOWERS.Count > 0)
        //        {
        //            FindFOLLOWERS(ALLTARGETS);
        //        }

        //        if (GROUPS.Count > 0)
        //        {
        //            FindGROUPS(ALLTARGETS);
        //        }
        //    }
        //    else
        //    {
        //        return "";
        //    }

        //    pictureBoxwaiting.Visible = true;
        //    return "test";
        //}

        public void InitializeGridTargets()
        {
            if (File.Exists(pathToSave + "\\TXT\\ForAnalytics.txt"))
            {
                string[] lignes = File.ReadAllLines(pathToSave + "\\TXT\\ForAnalytics.txt");
                Dictionary<string, string> targ = new Dictionary<string, string>();


                foreach (string li in lignes)
                {
                    string Category = li.Split(';')[0];
                    string Username = li.Split(';')[1];
                    Username = Username.Substring(Username.IndexOf("Key=") + 4).Split('@')[0];

                    string URL = li.Split(';')[1];
                    URL = URL.Substring(URL.IndexOf("@") + 1).Split('@')[0];

                    if (!targ.ContainsKey(Username))
                        targ.Add(Username, Username + ";" + URL);
                   
                }

                foreach(string s in targ.Values)
                {
                    string URL = s.Split(';')[1];
                    string Username = s.Split(';')[0];
                    bool deja = false;

                    if (Username.Contains("&sk=photos"))
                        Username = Username.Split(new string[] { "&sk=photos" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    //on verifie si url deja presente

                    foreach(DataGridViewRow ro in dataGridViewTargets.Rows)
                    {
                        if (ro.Cells[0].Value.ToString() == URL || ro.Cells[1].Value.ToString() == URL)
                        {
                            deja = true;
                            break;
                        }
                            
                            
                    }
                    if(!deja)
                    dataGridViewTargets.Rows.Add(Username, URL);
                }

                dataGridViewTargets.Sort(dataGridViewTargets.Columns[0], ListSortDirection.Ascending);
            }

            if (dataGridViewTargets.Rows.Count == 1)
                AutomaticMode();
                
        }

        private void AutomaticMode()
        {
            labelUSERNAMES.Visible = false;
            dataGridViewTargets.ColumnHeadersVisible = false;
            dataGridViewTargets.BackgroundColor = Color.White;
            dataGridViewTargets.Enabled = false;
            dataGridViewTargets.BorderStyle = BorderStyle.None;
            dataGridViewTargets.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridViewTargets.DefaultCellStyle.SelectionForeColor = Color.Black;
            button1.Visible = false;
            button2.Visible = false;
            dataGridViewTargets.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewTargets.Rows[0].Selected = true;
            pictureBoxwaiting.Image = null;


            if (File.Exists(pathToSave + "\\TXT\\ForAnalytics.txt"))
            {
                string[] lignes = File.ReadAllLines(pathToSave + "\\TXT\\ForAnalytics.txt");
                ForAnalytics = File.ReadAllText(pathToSave + "\\TXT\\ForAnalytics.txt");

            }

            var sorted = Directory.GetFiles(pathToSave, "*.fbv").Select(fn => new FileInfo(fn)).OrderBy(f => f.CreationTime);
            FileInfo[] fichiers = sorted.ToArray();

            string sauvegarde = File.ReadAllText(fichiers[0].FullName);
            string[] ecase = sauvegarde.Substring(sauvegarde.IndexOf("<Case>\n") + 7, (sauvegarde.IndexOf("</Case>\n") - (sauvegarde.IndexOf("<Case>\n") + 7))).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)[0].Split(';');

            labelID.Text = "Identifiant provenant du profil de " + ecase[5].Trim();
            //userControl = new UserControl1();



            string targ = dataGridViewTargets.SelectedRows[0].Cells[0].Value.ToString() + ";" + (dataGridViewTargets.SelectedRows[0].Cells[1].Value.ToString() == "aucune" ? "null" : dataGridViewTargets.SelectedRows[0].Cells[1].Value.ToString());

            if (IsTargetFound(targ))
            {
                userControl = new UserControl1();
                userControl.SetAutomaticMode(true);
                FindResults(targ);
                PopulateTargetsFound(targ.Split(';')[0]);

                Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxwaiting.Image")));
                dataGridViewResults.Rows.Add(img, targ.Replace(";", "@").Replace("null", "aucune"), hits.ToString());

            }
            else
            {
                Image img = global::Facebook_Anaytics.Properties.Resources.ko;
                dataGridViewResults.Rows.Add(img, targ.Replace(";", "@").Replace("null", "aucune"), "0");

            }
        }

        public string FindResults(string target)
        {
            FRIENDS = new List<String>();
            HOMEPAGE = new List<String>();
            TAGS = new List<String>();
            PICTURESLIKES = new List<String>();
            PICTURESCOMMENTS = new List<String>();
            LIKEPAGES = new List<String>();
            COMMENTS = new List<String>();
            COMMENTSSCREENSHOTS = new List<String>();
            MESSENGER = new List<String>();
            FOLLOWERS = new List<String>();
            GROUPS = new List<String>();
            FRIENDS = new List<String>();



            //on parcourt toutes les lignes et récupère les résultats par rapport aux catégories
            if (File.Exists(pathToSave + "\\TXT\\ForAnalytics.txt"))
            {
                string[] lignes = File.ReadAllLines(pathToSave + "\\TXT\\ForAnalytics.txt");
                hits = 0;

                foreach (string li in lignes)
                {
                    string Category = li.Split(';')[0];
                    string Username = li.Split(';')[1];
                    //Username = Username.Substring(Username.IndexOf("Key=") + 4).Split('@')[0];

                    //string URL = li.Split(';')[1];
                    //Username = Username.Substring(Username.IndexOf("@") + 1).Split('@')[0];

                    //dataGridViewTargets.Rows.Add(Username, URL);


                    switch (Category)
                    {
                        case "FRIENDS":
                            FRIENDS.Add(li);
                            break;

                        case "HOMEPAGE":
                            HOMEPAGE.Add(li);
                            break;

                        case "TAGS":
                            TAGS.Add(li);
                            break;

                        case "PICTURESLIKES":
                            PICTURESLIKES.Add(li);
                            break;

                        case "PICTURESCOMMENTS":
                            PICTURESCOMMENTS.Add(li);
                            break;

                        case "LIKEPAGES":
                            LIKEPAGES.Add(li);
                            break;

                        case "COMMENTS":
                            COMMENTS.Add(li);
                            break;

                        case "COMMENTSSCREENSHOTS":
                            COMMENTSSCREENSHOTS.Add(li);
                            break;

                        case "MESSENGER":
                            MESSENGER.Add(li);
                            break;

                        case "FOLLOWERS":
                            FOLLOWERS.Add(li);
                            break;

                        case "GROUPS":
                            GROUPS.Add(li);
                            break;
                    }
                }

                //maintenant par rapport aux resultats

                if (FRIENDS.Count > 0)
                {
                    userControl.FillResume("FRIENDS", FindFRIENDS(target).ToString());
                }
                else
                    userControl.FillResume("FRIENDS", "0");

                if (HOMEPAGE.Count > 0)
                {
                    userControl.FillResume("HOMEPAGE",FindHOMEPAGE(target).ToString());
                }
                else
                    userControl.FillResume("HOMEPAGE", "0");

                if (TAGS.Count > 0)
                {
                    userControl.FillResume("TAGS", FindTAGS(target).ToString());
                }
                else
                    userControl.FillResume("TAGS", "0");

                if (PICTURESLIKES.Count > 0)
                {
                    userControl.FillResume("LIKES", FindPICTURESLIKES(target).ToString());
                }
                else
                    userControl.FillResume("LIKES", "0");

                if (PICTURESCOMMENTS.Count > 0)
                {
                    userControl.FillResume("COMMENTSIMAGES",FindPICTURESCOMMENTS(target).ToString());
                }
                else
                    userControl.FillResume("COMMENTSIMAGES", "0");

                if (LIKEPAGES.Count > 0)
                {
                    userControl.FillResume("LIKEPAGES", FindLIKEPAGES(target).ToString());
                }
                else
                    userControl.FillResume("LIKEPAGES", "0");

                if (COMMENTS.Count > 0)
                {
                    userControl.FillResume("COMMENTS", FindCOMMENTS(target).ToString());
                }
                else
                    userControl.FillResume("COMMENTS", "0");

                if (COMMENTSSCREENSHOTS.Count > 0)
                {
                    userControl.FillResume("COMMENTSSCREENSHOTS", FindCOMMENTSSCREENSHOTS(target).ToString());
                }
                else
                    userControl.FillResume("COMMENTSSCREENSHOTS", "0");

                if (MESSENGER.Count > 0)
                {
                    userControl.FillResume("MESSENGER",FindMESSENGERS(target).ToString());
                }
                else
                    userControl.FillResume("MESSENGER", "0");

                if (FOLLOWERS.Count > 0)
                {
                    FindFOLLOWERS(target);
                }
                

                if (GROUPS.Count > 0)
                {
                    FindGROUPS(target);
                }
            }
            else
            {
                return "";
            }

            pictureBoxwaiting.Visible = true;
            return "test";
        }


        public bool PopulateTargetsFound(Dictionary<string,string> targets)
        {
           foreach(Control c in tabUsers.Controls)
            {
                tabUsers.Controls.Remove(c);
            }
                        
            foreach(string target in targets.Values)
            {
                TabPage tab = new TabPage(target);
                
                userControl.Dock = DockStyle.Fill;
                tab.Controls.Add(userControl);

                tabUsers.TabPages.Add(tab);
            }

            //tabUsers.Controls.Remove(tabUsers.Controls["tabPage3"]);
            tabUsers.Controls.Remove(tabUsers.Controls["tabPage4"]);

            return true;
        }

        public bool PopulateTargetsFound(string target)
        {
            
            
                TabPage tab = new TabPage(target);
                           

                userControl.Dock = DockStyle.Fill;

            //Panel line = new Panel();
            //line.Size = new Size(80, 10);
            //line.Location = new Point(tab., 3);
            //line.BackColor = Color.DeepSkyBlue;
            //tab.Controls.Add(line);
            userControl.SetForSaving(lignesForSavingCase, target, IDForSaving);
            tab.Controls.Add(userControl);            
            tabUsers.TabPages.Add(tab);
               
                        

            //tabUsers.Controls.Remove(tabUsers.Controls["tabPage3"]);
            //tabUsers.Controls.Remove(tabUsers.Controls["tabPage4"]);

            return true;
        }
        private void FindGROUPS(string target)
        {
 
        }

        private void FindFOLLOWERS(string target)
        {
  
        }

        private int FindMESSENGERS(string t)
        {
            IList<string> lignes = new List<string>();

            foreach (string ligne in MESSENGER)
            {
                string[] parames = ligne.Split(';');

                string tofind1 = t.Split(';')[0];
                string tofind2 = t.Split(';')[1];

                if (ligne.Contains("Key=" + tofind1) || ligne.Contains("@" + tofind2 + "@"))
                {
                    lignes.Add(parames[2] + ";" + parames[4] + ";" + parames[3]);
                    lignesForSavingCase.Add(ligne);
                }

            }

            if (lignes.Count > 0)
            {
                userControl.FillMessenger(lignes);
                //PopulateTargetsFound(t);
            }



            hits += lignes.Count();
            return lignes.Count();
        }

        private int FindCOMMENTSSCREENSHOTS(string t)
        {
            IList<string> lignes = new List<string>();
            Dictionary<string, string> targetFound = new Dictionary<string, string>();


            foreach (string ligne in COMMENTSSCREENSHOTS)
            {
                string[] parames = ligne.Split(';');

                string tofind1 = t.Split(';')[0];
                string tofind2 = t.Split(';')[1];

                if (ligne.Contains("Key=" + tofind1) || ligne.Contains("@" + tofind2 + "@"))
                {
                    lignes.Add(parames[4]);
                    lignesForSavingCase.Add(ligne);
                }

            }

            if (lignes.Count > 0)
            {
                userControl.FillCommentsscreenshot(lignes);
                //PopulateTargetsFound(t);
            }



            hits += lignes.Count();
            return lignes.Count();
        }

        private int FindCOMMENTS(string t)
        {
            IList<string> lignes = new List<string>();
            Dictionary<string, string> targetFound = new Dictionary<string, string>();


            foreach (string ligne in COMMENTS)
            {
                string[] parames = ligne.Split(';');

                //string cle = parames[1];
                //string username = cle.Substring(cle.IndexOf("Key=") + 4).Split('@')[0];
                //string url = cle.Substring(cle.IndexOf("@") + 1).Split('@')[0];

                //if (url == "")
                //    url = "404";

                string tofind1 = t.Split(';')[0];
                string tofind2 = t.Split(';')[1];

                if (ligne.Contains("Key=" + tofind1) || ligne.Contains("@" + tofind2 + "@"))
                {
                    lignes.Add(parames[4] + ";" + parames[5]);
                    lignesForSavingCase.Add(ligne);
                }

            }

            if (lignes.Count > 0)
            {
                userControl.FillComments(lignes);
                //PopulateTargetsFound(t);
            }



            hits += lignes.Count();
            return lignes.Count();
        }

        private int FindLIKEPAGES(string t)
        {
            IList<string> lignes = new List<string>();
            Dictionary<string, string> targetFound = new Dictionary<string, string>();


            foreach (string ligne in LIKEPAGES)
            {
                string[] parames = ligne.Split(';');

                string tofind1 = t.Split(';')[0];
                string tofind2 = t.Split(';')[1];

                if (ligne.Contains("Key=" + tofind1) || ligne.Contains("@" + tofind2 + "@"))
                {
                    lignes.Add(parames[4] + ";" + parames[5]);
                    lignesForSavingCase.Add(ligne);
                }

            }

            if (lignes.Count > 0)
            {
                userControl.FillLikes(lignes);
                //PopulateTargetsFound(t);
            }



            hits += lignes.Count();
            return lignes.Count();

        }

        private int FindPICTURESCOMMENTS(string t)
        {
            IList<string> lignes = new List<string>();


            foreach (string ligne in PICTURESCOMMENTS)
            {
                string[] parames = ligne.Split(';');

                string tofind1 = t.Split(';')[0];
                string tofind2 = t.Split(';')[1];

                if (ligne.Contains("Key=" + tofind1) || ligne.Contains("@" + tofind2 + "@"))
                {
                    lignes.Add(pathToSave + "SCREENSHOTSPICTURESCOMMENTS\\" + parames[4]);
                    lignesForSavingCase.Add(ligne);
                }

            }

            if (lignes.Count > 0)
            {
                userControl.FillPicturesComments(lignes);
                //PopulateTargetsFound(t);
            }



            hits += lignes.Count();
            return lignes.Count();
        }

        private int FindPICTURESLIKES(string t)
        {
            IList<string> lignes = new List<string>();

            
                foreach (string ligne in PICTURESLIKES)
                {
                    string[] parames = ligne.Split(';');

                string tofind1 = t.Split(';')[0];
                string tofind2 = t.Split(';')[1];

                    if (ligne.Contains("Key=" + tofind1) || ligne.Contains("@" + tofind2 + "@"))
                    {
                        lignes.Add(pathToSave + "SCREENSHOTSPICTURESCOMMENTS\\" + parames[4]);
                        lignesForSavingCase.Add(ligne);
                    }

                }

            if (lignes.Count > 0)
            {
                userControl.FillLikesImages(lignes);
                //PopulateTargetsFound(t);
            }



            hits += lignes.Count();
            return lignes.Count();

        }

        private int FindTAGS(string t)
        {
            IList<string> lignes = new List<string>();
            Dictionary<string, string> targetFound = new Dictionary<string, string>();

            
                foreach (string ligne in TAGS)
                {
                    string[] parames = ligne.Split(';');

                string tofind1 = t.Split(';')[0];
                string tofind2 = t.Split(';')[1];

                if (ligne.Contains("Key=" + tofind1) || ligne.Contains("@" + tofind2 + "@"))
                {
                        lignes.Add(parames[4]);
                        lignesForSavingCase.Add(ligne);
                }

                }

            if (lignes.Count > 0)
            {
                userControl.FillTags(lignes);
                //PopulateTargetsFound(t);
            }



            hits += lignes.Count();
            return lignes.Count();

        }

        private int FindHOMEPAGE(string t)
        {
            IList<string> lignes = new List<string>();
            List<string> sortedListForSearching = new List<string>();
            List<string> resultatsRecherche = new List<string>();
            List<string> resultatsRechercheSuite = new List<string>();

            if (File.Exists(pathToSave + "\\HOMEPAGE\\HomepageComments_With_Screenshots.txt"))
            {
                string[] ligness = File.ReadAllLines(pathToSave + "\\HOMEPAGE\\HomepageComments_With_Screenshots.txt");
                foreach (string li in ligness)
                {
                    if (li == "")
                        continue;

                    //lignesForSavingCase.Add(li);
                    sortedListForSearching.Add(li);                 


                }
                resultatsRecherche = sortedListForSearching.FindAll(x => x.Contains(t));
            }



            foreach (string ligne in HOMEPAGE)
            {
                    string [] parames = ligne.Split(';');

                

                string tofind1 = t.Split(';')[0];
                string tofind2 = t.Split(';')[1];

                if (ligne.Contains("Key=" + tofind1) || ligne.Contains("@" + tofind2 + "@"))
                    {
                        
                        resultatsRechercheSuite = resultatsRecherche.FindAll(x => x.Contains(parames[4]));

                    if(resultatsRechercheSuite.Count > 0)
                    {
                        foreach (string suite in resultatsRechercheSuite)
                        {
                            lignes.Add(parames[4] + ";" + suite.Split(';')[0]);
                            lignesForSavingCase.Add(ligne);
                        }
                    }
                    else
                    {
                        lignes.Add(parames[4] + ";" + "");
                        lignesForSavingCase.Add(ligne);
                    }
                    

                    

                }

            }

            if (lignes.Count > 0)
            {
                userControl.FillJournal(lignes);
                //PopulateTargetsFound(t);
            }



            hits += lignes.Count();
            return lignes.Count();
        }

        private int FindFRIENDS(string t)
        {
            IList<string> lignes = new List<string>();
           
            foreach (string ligne in FRIENDS)
            {
                string[] parames = ligne.Split(';');

                string tofind1 = t.Split(';')[0];
                string tofind2 = t.Split(';')[1];

                if (ligne.Contains("Key=" + tofind1) || ligne.Contains("@" + tofind2 + "@"))
                {
                    lignes.Add(parames[2] + ";" + parames[4] + ";" + parames[3]);
                    lignesForSavingCase.Add(ligne);
                }

            }

            if (lignes.Count > 0)
            {
                userControl.FillFriends(lignes);
                //PopulateTargetsFound(t);
            }



            hits += lignes.Count();
            return lignes.Count();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBoxwaiting.Visible = false;
            string[] byUsernames = textBoxUSernames.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            //string[] byUrls = textBoxUrls.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            dataGridViewResults.Rows.Clear();
            lignesForSavingCase = new List<string>();
           //string[] allTargets = new string[1];

            ALLTARGETS = new Dictionary<string, string>();
            FRIENDS = new List<String>();
            HOMEPAGE = new List<String>();
            TAGS = new List<String>();
            PICTURESLIKES = new List<String>();
            PICTURESCOMMENTS = new List<String>();
            LIKEPAGES = new List<String>();
            COMMENTS = new List<String>();
            COMMENTSSCREENSHOTS = new List<String>();
            MESSENGER = new List<String>();
            FOLLOWERS = new List<String>();
            GROUPS = new List<String>();
            //Array.Resize(ref allTargets, (allTargets.Length + byUsernames.Length + byUrls.Length));

            //foreach (Control c in tabUsers.Controls)
            //{
            //    tabUsers.Controls.Remove(c);
            //}
            tabUsers.TabPages.Clear();

            foreach (string s in byUsernames)
            {
                if (!ALLTARGETS.ContainsKey(s))
                    ALLTARGETS.Add(s, s);               
               
            }

            foreach(DataGridViewRow row in dataGridViewTargets.SelectedRows)
            {
                if (!ALLTARGETS.ContainsKey(row.Cells[0].Value.ToString() + row.Cells[1].Value.ToString()))
                    ALLTARGETS.Add(row.Cells[0].Value.ToString() + row.Cells[0].Value.ToString(), row.Cells[0].Value.ToString() +";" + (row.Cells[1].Value.ToString() == "aucune" ? "null" : row.Cells[1].Value.ToString()));
            }

            //foreach (string s in byUrls)
            //{
            //    if (!ALLTARGETS.ContainsKey(s))
            //        ALLTARGETS.Add(s, s);
            //}


            foreach (string targ in ALLTARGETS.Values)
            {

                ALLTARGETS = new Dictionary<string, string>();
                FRIENDS = new List<String>();
                HOMEPAGE = new List<String>();
                TAGS = new List<String>();
                PICTURESLIKES = new List<String>();
                PICTURESCOMMENTS = new List<String>();
                LIKEPAGES = new List<String>();
                COMMENTS = new List<String>();
                COMMENTSSCREENSHOTS = new List<String>();
                MESSENGER = new List<String>();
                FOLLOWERS = new List<String>();
                GROUPS = new List<String>();
                lignesForSavingCase = new List<string>();



                if (IsTargetFound(targ))
                {
                    userControl = new UserControl1();
                    FindResults(targ);
                    PopulateTargetsFound(targ.Split(';')[0]);

                    Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxwaiting.Image")));
                    dataGridViewResults.Rows.Add(img, targ.Replace(";", "@").Replace("null", "aucune"), hits.ToString());

                }
                else
                {
                    Image img = global::Facebook_Anaytics.Properties.Resources.ko;
                    dataGridViewResults.Rows.Add(img, targ, "0");
                    continue;
                }

            }

            //PopulateTargetsFound(ALLTARGETS);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridViewResults.Rows.Clear();
        }

        private void dataGridViewTargets_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ALLTARGETS = new Dictionary<string, string>();
            FRIENDS = new List<String>();
            HOMEPAGE = new List<String>();
            TAGS = new List<String>();
            PICTURESLIKES = new List<String>();
            PICTURESCOMMENTS = new List<String>();
            LIKEPAGES = new List<String>();
            COMMENTS = new List<String>();
            COMMENTSSCREENSHOTS = new List<String>();
            MESSENGER = new List<String>();
            FOLLOWERS = new List<String>();
            GROUPS = new List<String>();

            tabUsers.TabPages.Clear();
            dataGridViewResults.Rows.Clear();


            //foreach (DataGridViewRow row in dataGridViewTargets.SelectedRows)
            //{
            //    if (!ALLTARGETS.ContainsKey(row.Cells[0].Value.ToString() + row.Cells[1].Value.ToString()))
            //        ALLTARGETS.Add(row.Cells[0].Value.ToString() + row.Cells[0].Value.ToString(), row.Cells[0].Value.ToString() + ";" + row.Cells[1].Value.ToString());
            //}
            // if (!ALLTARGETS.ContainsKey(row.Cells[0].Value.ToString() + row.Cells[1].Value.ToString()))
            //ALLTARGETS.Add(row.Cells[0].Value.ToString() + row.Cells[0].Value.ToString(), row.Cells[0].Value.ToString() + ";" + (row.Cells[1].Value.ToString() == "aucune" ? "null" : row.Cells[1].Value.ToString()));

            string targ = dataGridViewTargets.SelectedRows[0].Cells[0].Value.ToString() + ";" + (dataGridViewTargets.SelectedRows[0].Cells[1].Value.ToString() == "aucune" ? "null" : dataGridViewTargets.SelectedRows[0].Cells[1].Value.ToString());

            if (IsTargetFound(targ))
            {
                userControl = new UserControl1();
                FindResults(targ);
                PopulateTargetsFound(targ.Split(';')[0]);

                Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxwaiting.Image")));
                dataGridViewResults.Rows.Add(img, targ.Replace(";", "@").Replace("null","aucune"),hits.ToString());

            }
            else
            {
                Image img = global::Facebook_Anaytics.Properties.Resources.ko;
                dataGridViewResults.Rows.Add(img, targ.Replace(";", "@").Replace("null", "aucune"),"0");

            }
        }

        private void dataGridViewResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (TabPage tab in tabUsers.TabPages)
            {
                if (tab.Text == dataGridViewResults.Rows[e.RowIndex].Cells[1].Value.ToString().Split('@')[0])
                {
                    tabUsers.SelectedTab = tab;
                    TabPage pg = tabUsers.SelectedTab;
                    Main.SelectedTab = Main.TabPages[1];
                    //Main.SelectedTab = Main.TabPages[1];
                    break;
                }



            }
        }

        private void tabUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string targ = tabUsers.SelectedTab.Text;


            //if (IsTargetFound(targ))
            //{
            //    userControl = new UserControl1();
            //    FindResults(targ + ";null");
            //    PopulateTargetsFound(targ.Split(';')[0]);

            //    Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxwaiting.Image")));
            //    dataGridViewResults.Rows.Add(img, targ.Replace(";", "@").Replace("null", "aucune"), hits.ToString());

            //}
            //else
            //{
            //    Image img = global::Facebook_Anaytics.Properties.Resources.ko;
            //    dataGridViewResults.Rows.Add(img, targ, "0");
                
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 formallcases = new Form2();
                formallcases.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SearchKeywords(textBox4.Text);
        }

        private void SearchKeywords(string keyword)
        {

            //dataGridViewSearchResults.Rows.Clear();
            dataGridView1.Rows.Clear();

            if (button5.Text == "FILTRER")
                button5.Text = "MONTRER TOUT";
            
            if (textBox4.Text == "")
            {
                flowLayoutPanel5.Controls.Clear();
                return;
            }

            try
            {
                flowLayoutPanel5.Controls.Clear();
                flowLayoutPanel5.Visible = false;

                foreach (DataGridViewRow ro in dataGridViewSearchResults.Rows)
                {

                    ro.DefaultCellStyle.BackColor = Color.White;

                }

                foreach (DataGridViewRow ro in dataGridViewSearchResults.Rows)
                {
                    if (dataGridViewSearchResults.Rows[ro.Index].Cells[1].Value.ToString().ToLower().Contains(textBox4.Text.ToLower()))
                    {
                        ro.DefaultCellStyle.BackColor = Color.Red;

                        LinkLabel link = new LinkLabel();
                        //link.Text = numeroLigne;
                        link.Text = ro.Index.ToString();
                        link.AutoSize = true;
                        link.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        //link.Name = "linkLabel" + indexx;
                        link.Name = "linkLabel" + ro.Index;
                        link.Size = new System.Drawing.Size(18, 20);
                        link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
                        flowLayoutPanel5.Controls.Add(link);

                        dataGridView1.Rows.Add(ro.Cells[0].Value, ro.Cells[1].Value, ro.Cells[2].Value);

                    }
                    
                }


                dataGridView1.Visible = true;
            }
            catch
            {
                return;
            }



        }

        private void FillComments()
        {
            if (Directory.Exists(pathToSave + "TXT") && File.Exists(pathToSave + "TXT\\commentsFromPictures.txt"))
            {
                dataGridViewSearchResults.Rows.Clear();
                foreach (string li in File.ReadAllLines(pathToSave + "TXT\\commentsFromPictures.txt"))
                {
                    string[] champ = li.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    

                    //foreach (string lii in champ)
                    //{
                    //string[] champp = lii.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        //if (File.Exists(pathToSave + "SCREENSHOTSPICTURESCOMMENTS\\" + champ[4]))
                        //{
                        //    FileInfo fichierr = new FileInfo(pathToSave + "SCREENSHOTSPICTURESCOMMENTS\\" + champ[4]);
                        //    Image img = ResizeImage(fichierr.FullName, 60, 60, false);
                            dataGridViewSearchResults.Rows.Add(champ[0], champ[1], pathToSave + "SCREENSHOTSPICTURESCOMMENTS\\" + champ[4]);
                        //}
                        //else
                        //{
                        //    //Image img = global::FacebookAnalyzer.Properties.Resources.anonymous;
                        //    //dataGridViewTags.Rows.Add(img, champ[0], champ[1], champ[2]);
                        //    continue;
                        //}
                    }
                    catch
                    {
                        //Image img = global::FacebookAnalyzer.Properties.Resources.anonymous;
                        //dataGridViewTags.Rows.Add(img, champ[0], champ[1], champ[2]);
                        continue;
                    }

                    //}

                }
            }

            if (File.Exists(pathToSave + "\\HOMEPAGE\\HomepageComments_With_Screenshots.txt"))
            {
                string[] lines = File.ReadAllLines(pathToSave + "\\HOMEPAGE\\HomepageComments_With_Screenshots.txt");

                foreach (string ligne in lines)
                {
                    string[] champ = ligne.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    //foreach (string lii in champ)
                    //{
                    //string[] champp = lii.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        dataGridViewSearchResults.Rows.Add(champ[1], champ[0], pathToSave + champ[3]);

                    }
                    catch (Exception ex)
                    {
                        //return;
                    }
                }

            }
            //if (Directory.Exists(pathToSave + "TXT") && File.Exists(pathToSave + "TXT\\pagesLiked.txt"))
            //{
            //    if (dataGridViewSearchResults.Rows.Count == 0)
            //        dataGridViewSearchResults.Rows.Clear();

            //    foreach (string li in File.ReadAllLines(pathToSave + "TXT\\pagesLiked.txt"))
            //    {
            //        string[] champ = li.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            //        //foreach (string lii in champ)
            //        //{
            //        //string[] champp = lii.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            //        try
            //        {
            //            if (File.Exists(pathToSave + champ[0]))
            //            {
            //                FileInfo fichierr = new FileInfo(pathToSave + champ[0]);
            //                Image img = ResizeImage(fichierr.FullName, 60, 60, false);
            //                dataGridViewSearchResults.Rows.Add(img, champ[1], champ[2]);
            //            }
            //            else
            //            {
            //                //Image img = global::FacebookAnalyzer.Properties.Resources.anonymous;
            //                //dataGridViewTags.Rows.Add(img, champ[0], champ[1], champ[2]);
            //                continue;
            //            }
            //        }
            //        catch
            //        {
            //            //Image img = global::FacebookAnalyzer.Properties.Resources.anonymous;
            //            //dataGridViewTags.Rows.Add(img, champ[0], champ[1], champ[2]);
            //            continue;
            //        }

            //        //}

            //    }
            //}

        }

        public static Image ResizeImage(string file,
                                    int width,
                                    int height,
                                    bool onlyResizeIfWider)
        {
            using (Image image = Image.FromFile(file))
            {
                // Prevent using images internal thumbnail
                image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                image.RotateFlip(RotateFlipType.Rotate180FlipNone);

                if (onlyResizeIfWider == true)
                {
                    if (image.Width <= width)
                    {
                        width = image.Width;
                    }
                }

                int newHeight = image.Height * width / image.Width;
                if (newHeight > height)
                {
                    // Resize with height instead
                    width = image.Width * height / image.Height;
                    newHeight = height;
                }

                Image NewImage = image.GetThumbnailImage(width,
                                                         newHeight,
                                                         null,
                                                         IntPtr.Zero);

                return NewImage;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dataGridViewSearchResults.ClearSelection();
            dataGridViewSearchResults.FirstDisplayedScrollingRowIndex = Int32.Parse(((LinkLabel)sender).Text);
            dataGridViewSearchResults.Focus();
            dataGridViewSearchResults.Rows[Int32.Parse(((LinkLabel)sender).Text)].DefaultCellStyle.BackColor = Color.Yellow;

        }

        private void dataGridViewSearchResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewSearchResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Process.Start(dataGridViewSearchResults.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Process.Start(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridViewSearchResults.ClearSelection();
            flowLayoutPanel5.Controls.Clear();
            textBox4.Text = "";

            foreach (DataGridViewRow ro in dataGridViewSearchResults.Rows)
            {

                ro.DefaultCellStyle.BackColor = Color.White;

            }

            dataGridViewSearchResults.FirstDisplayedScrollingRowIndex = 0;
            dataGridViewSearchResults.Focus();

            dataGridView1.Rows.Clear();
            dataGridView1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            dataGridView1.Visible = false;

            if (button5.Text == "FILTRER")
            {
                button5.Text = "MONTRER TOUT";
                dataGridView1.Visible = true;
                flowLayoutPanel5.Visible = false;
                return;
            }

            if (button5.Text == "MONTRER TOUT")
            {
                button5.Text = "FILTRER";
                dataGridView1.Visible = false;
                flowLayoutPanel5.Visible = true;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //try
            //{
            //    SendEmail(GetMacAddress());
            //}
            //catch
            //{

            //}
        }

        public static string GetMacAddress()
        {
            string macAddresses = "";

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                macAddresses = nic.GetPhysicalAddress().ToString();
                break;
            }
            return macAddresses;
        }

        private void SendEmail(string macadress)
        {
            string windowusername = "";
            try
            {
                windowusername = System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName;

            }
            catch
            {

            }


            MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add("facebookanalyzer20@gmail.com");

            message.Subject = "Fermeture Analytics : " + macadress;
            message.From = new System.Net.Mail.MailAddress("facebookanalyzer20@gmail.com");
            message.Body = "Fermeture Analytics : Mac Adresse : " + macadress + " - " + USERNAME + " - " + windowusername;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential("facebookanalyzer20@gmail.com", "Mat8804256$.");
            smtp.EnableSsl = true;

            smtp.Send(message);
        }
    }
}
