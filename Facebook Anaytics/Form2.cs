using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Facebook_Anaytics
{
    public partial class Form2 : Form
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
        public string ForAnalytics = "";
        public IList<string> ALLForAnalytics = new List<string>();
        public UserControl1 userControl;
        public Fiche fiche;
        public string UsernameForSaving = "";
        public string IDForSaving = "";
        public string ImageProfileForSaving = "";
        public Initcontrol _init;
        public List<Casedata> groups = new List<Casedata>();
        public Dictionary<string, List<Casedata>> Preparedata = new Dictionary<string, List<Casedata>>();
        public List<Image> images = new List<Image>();



        public int hits = 0;
        //Dictionary<string, Analytics> Analytics = new Dictionary<string, Analytics>();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        public Form2()
        {
            InitializeComponent();
            fiche = new Fiche(label2);
            //_init = init;
            //_init.Refresh();
            //_init.Show();
            //_init.Update();



            //InitializeGridTargets();
        }

        public Casedata Import(string fichier)
        {

            Casedata casedata = new Casedata();
            string sauvegarde = File.ReadAllText(fichier);

            string[] ecase = sauvegarde.Substring(sauvegarde.IndexOf("<Case>\n") + 7, (sauvegarde.IndexOf("</Case>\n") - (sauvegarde.IndexOf("<Case>\n") + 7))).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)[0].Split(';');
            string[] pictureProfile = sauvegarde.Substring(sauvegarde.IndexOf("<PictureProfile>\n") + 16, (sauvegarde.IndexOf("</PictureProfile>\n") - (sauvegarde.IndexOf("<PictureProfile>\n") + 16))).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                //string[] datagridviewmessenger = sauvegarde.Substring(sauvegarde.IndexOf("<DataGridViewMessenger>\n") + 24).Split(new string[] { "</DataGridViewMessenger>\n" }, StringSplitOptions.RemoveEmptyEntries);
                  IDForSaving = ecase[0].Trim().Replace("https://www.facebook.com/", "") + " - ID : " + ecase[5].Trim();
                casedata.Label = IDForSaving;
                casedata.Url = ecase[0].Trim().Replace("https://www.facebook.com/", "");
                casedata.Id = ecase[5].Trim();
                casedata.Username = pictureProfile[0].Replace(".\\","").Replace(".jpg","");

            }
            catch
            {

            }



            FileInfo fich = new FileInfo(fichier);
            if(pictureProfile.Count() > 0)
            if (pictureProfile[0] != "label3" && File.Exists(fich.Directory + "\\" + pictureProfile[0]))
            {
                try
                {
                    casedata.ImageProfile = Image.FromFile(fich.Directory + "\\" + pictureProfile[0]);
                    
                }
                catch (Exception ex)
                {

                }

            }







            return casedata;


        }


        public bool IsTargetFound(string target, string casee)
        {
            if (File.Exists(casee))
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

        public int InitializeGridTargets(string fichi)
        {
            dataGridViewTargets.Rows.Clear();
            
            if (File.Exists(fichi))
            {
                string[] lignes = File.ReadAllLines(fichi);
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

                foreach (string s in targ.Values)
                {
                    string URL = s.Split(';')[1];
                    string Username = s.Split(';')[0];
                    bool deja = false;

                    if (Username.Contains("&sk=photos"))
                        Username = Username.Split(new string[] { "&sk=photos" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    //on verifie si url deja presente

                    foreach (DataGridViewRow ro in dataGridViewTargets.Rows)
                    {
                        if (ro.Cells[0].Value.ToString() == URL || ro.Cells[1].Value.ToString() == URL)
                        {
                            deja = true;
                            break;
                        }


                    }
                    if (!deja)
                        dataGridViewTargets.Rows.Add(Username, URL);
                }

                
            }

            return dataGridViewTargets.Rows.Count;
           
        }

        //private void AutomaticMode()
        //{
        //    labelUSERNAMES.Visible = false;
        //    dataGridViewTargets.ColumnHeadersVisible = false;
        //    dataGridViewTargets.BackgroundColor = Color.Gainsboro;
        //    dataGridViewTargets.Enabled = false;
        //    dataGridViewTargets.BorderStyle = BorderStyle.None;
        //    dataGridViewTargets.DefaultCellStyle.SelectionBackColor = Color.Gainsboro;
        //    dataGridViewTargets.DefaultCellStyle.SelectionForeColor = Color.Black;
        //    button1.Visible = false;
        //    button2.Visible = false;
        //    dataGridViewTargets.CellBorderStyle = DataGridViewCellBorderStyle.None;
        //    dataGridViewTargets.Rows[0].Selected = true;
        //    pictureBoxwaiting.Image = null;


        //    if (File.Exists(pathToSave + "\\TXT\\ForAnalytics.txt"))
        //    {
        //        string[] lignes = File.ReadAllLines(pathToSave + "\\TXT\\ForAnalytics.txt");
        //        ForAnalytics = File.ReadAllText(pathToSave + "\\TXT\\ForAnalytics.txt");

        //    }

        //    var sorted = Directory.GetFiles(pathToSave, "*.fbv").Select(fn => new FileInfo(fn)).OrderBy(f => f.CreationTime);
        //    FileInfo[] fichiers = sorted.ToArray();

        //    string sauvegarde = File.ReadAllText(fichiers[0].FullName);
        //    string[] ecase = sauvegarde.Substring(sauvegarde.IndexOf("<Case>\n") + 7, (sauvegarde.IndexOf("</Case>\n") - (sauvegarde.IndexOf("<Case>\n") + 7))).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)[0].Split(';');

        //    labelID.Text = "Identifiant provenant du profil de " + ecase[5].Trim();
        //    //userControl = new UserControl1();



        //    string targ = dataGridViewTargets.SelectedRows[0].Cells[0].Value.ToString() + ";" + (dataGridViewTargets.SelectedRows[0].Cells[1].Value.ToString() == "aucune" ? "null" : dataGridViewTargets.SelectedRows[0].Cells[1].Value.ToString());

        //    if (IsTargetFound(targ))
        //    {
        //        userControl = new UserControl1();
        //        userControl.SetAutomaticMode(true);
        //        FindResults(targ);
        //        PopulateTargetsFound(targ.Split(';')[0]);

        //        Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxwaiting.Image")));
        //        dataGridViewResults.Rows.Add(img, targ.Replace(";", "@").Replace("null", "aucune"), hits.ToString());

        //    }
        //    else
        //    {
        //        Image img = global::Facebook_Anaytics.Properties.Resources.ko;
        //        dataGridViewResults.Rows.Add(img, targ.Replace(";", "@").Replace("null", "aucune"), "0");

        //    }
        //}

        //public string FindResults(string target)
        //{
        //    //on parcourt toutes les lignes et récupère les résultats par rapport aux catégories
        //    if (File.Exists(pathToSave + "\\TXT\\ForAnalytics.txt"))
        //    {
        //        string[] lignes = File.ReadAllLines(pathToSave + "\\TXT\\ForAnalytics.txt");
        //        hits = 0;

        //        foreach (string li in lignes)
        //        {
        //            string Category = li.Split(';')[0];
        //            string Username = li.Split(';')[1];
        //            //Username = Username.Substring(Username.IndexOf("Key=") + 4).Split('@')[0];

        //            //string URL = li.Split(';')[1];
        //            //Username = Username.Substring(Username.IndexOf("@") + 1).Split('@')[0];

        //            //dataGridViewTargets.Rows.Add(Username, URL);


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
        //            userControl.FillResume("FRIENDS", FindFRIENDS(target).ToString());
        //        }
        //        else
        //            userControl.FillResume("FRIENDS", "0");

        //        if (HOMEPAGE.Count > 0)
        //        {
        //            userControl.FillResume("HOMEPAGE",FindHOMEPAGE(target).ToString());
        //        }
        //        else
        //            userControl.FillResume("HOMEPAGE", "0");

        //        if (TAGS.Count > 0)
        //        {
        //            userControl.FillResume("TAGS", FindTAGS(target).ToString());
        //        }
        //        else
        //            userControl.FillResume("TAGS", "0");

        //        if (PICTURESLIKES.Count > 0)
        //        {
        //            userControl.FillResume("LIKES", FindPICTURESLIKES(target).ToString());
        //        }
        //        else
        //            userControl.FillResume("LIKES", "0");

        //        if (PICTURESCOMMENTS.Count > 0)
        //        {
        //            userControl.FillResume("COMMENTSIMAGES",FindPICTURESCOMMENTS(target).ToString());
        //        }
        //        else
        //            userControl.FillResume("COMMENTSIMAGES", "0");

        //        if (LIKEPAGES.Count > 0)
        //        {
        //            userControl.FillResume("LIKEPAGES", FindLIKEPAGES(target).ToString());
        //        }
        //        else
        //            userControl.FillResume("LIKEPAGES", "0");

        //        if (COMMENTS.Count > 0)
        //        {
        //            userControl.FillResume("COMMENTS", FindCOMMENTS(target).ToString());
        //        }
        //        else
        //            userControl.FillResume("COMMENTS", "0");

        //        if (COMMENTSSCREENSHOTS.Count > 0)
        //        {
        //            userControl.FillResume("COMMENTSSCREENSHOTS", FindCOMMENTSSCREENSHOTS(target).ToString());
        //        }
        //        else
        //            userControl.FillResume("COMMENTSSCREENSHOTS", "0");

        //        if (MESSENGER.Count > 0)
        //        {
        //            userControl.FillResume("MESSENGER",FindMESSENGERS(target).ToString());
        //        }
        //        else
        //            userControl.FillResume("MESSENGER", "0");

        //        if (FOLLOWERS.Count > 0)
        //        {
        //            FindFOLLOWERS(target);
        //        }
                

        //        if (GROUPS.Count > 0)
        //        {
        //            FindGROUPS(target);
        //        }
        //    }
        //    else
        //    {
        //        return "";
        //    }

        //    //pictureBoxwaiting.Visible = true;
        //    return "test";
        //}

        public string InitializeCaseData(UserControCase casecontrol, string casee)
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


            //on parcourt toutes les string targetlignes et récupère les résultats par rapport aux catégories
            if (File.Exists(casee))
            {
                string[] lignes = File.ReadAllLines(casee);
                hits = 0;

                foreach (string li in lignes)
                {
                    string Category = li.Split(';')[0];
                    string Username = li.Split(';')[1];
                    //Username = Username.Substring(Username.IndexOf("Key=") + 4).Split('@')[0];

                    //string URL = li.Split(';')[1];
                    //Username = Username.Substring(Username.IndexOf("@") + 1).Split('@')[0];

                    //dataGridViewTargets.Rows.Add(Username, URL);

                    casecontrol.ALL.Add(li);

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
                    casecontrol.FillFRIENDS(FRIENDS);
                }
                

                if (HOMEPAGE.Count > 0)
                {
                    casecontrol.FillHOMEPAGE(HOMEPAGE);
                }
                

                if (TAGS.Count > 0)
                {
                    casecontrol.FillTAGS(TAGS);
                }


                if (PICTURESLIKES.Count > 0)
                {
                    casecontrol.FillPICTURESLIKES(PICTURESLIKES);
                }
                

                if (PICTURESCOMMENTS.Count > 0)
                {
                    casecontrol.FillPICTURESCOMMENTS(PICTURESCOMMENTS);
                }

                if (LIKEPAGES.Count > 0)
                {
                    casecontrol.FillLIKEPAGES(LIKEPAGES);
                }
               

                if (COMMENTS.Count > 0)
                {
                    casecontrol.FillCOMMENTS(COMMENTS);
                }
               

                if (COMMENTSSCREENSHOTS.Count > 0)
                {
                    casecontrol.FillCOMMENTSSCREENSHOTS(COMMENTSSCREENSHOTS);
                }
               

                if (MESSENGER.Count > 0)
                {
                    casecontrol.FillMESSENGER(MESSENGER);
                }
               

                if (FOLLOWERS.Count > 0)
                {
                    casecontrol.FillFOLLOWERS(FOLLOWERS);
                }


                if (GROUPS.Count > 0)
                {
                    casecontrol.FillGROUPS(GROUPS);
                }
            }
            else
            {
                return "";
            }

            //pictureBoxwaiting.Visible = true;
            return "test";
        }


        //public bool PopulateTargetsFound(Dictionary<string,string> targets)
        //{
        //   foreach(Control c in tabUsers.Controls)
        //    {
        //        tabUsers.Controls.Remove(c);
        //    }
                        
        //    foreach(string target in targets.Values)
        //    {
        //        TabPage tab = new TabPage(target);
                
        //        userControl.Dock = DockStyle.Fill;
        //        tab.Controls.Add(userControl);

        //        tabUsers.TabPages.Add(tab);
        //    }

        //    //tabUsers.Controls.Remove(tabUsers.Controls["tabPage3"]);
        //    tabUsers.Controls.Remove(tabUsers.Controls["tabPage4"]);

        //    return true;
        //}

        //public bool PopulateTargetsFound(string target)
        //{
            
            
        //        TabPage tab = new TabPage(target);
                           

        //        userControl.Dock = DockStyle.Fill;

        //    //Panel line = new Panel();
        //    //line.Size = new Size(80, 10);
        //    //line.Location = new Point(tab., 3);
        //    //line.BackColor = Color.DeepSkyBlue;
        //    //tab.Controls.Add(line);
        //    userControl.SetForSaving(lignesForSavingCase, target, IDForSaving);
        //    tab.Controls.Add(userControl);            
        //    tabUsers.TabPages.Add(tab);
               
                        

        //    //tabUsers.Controls.Remove(tabUsers.Controls["tabPage3"]);
        //    //tabUsers.Controls.Remove(tabUsers.Controls["tabPage4"]);

        //    return true;
        //}
        private void FindGROUPS(string target)
        {
 
        }

        private void FindFOLLOWERS(string target)
        {
  
        }

        private int FindMESSENGERS(string t, UserControCase userc)
        {
            IList<string> lignes = new List<string>();

            foreach (string ligne in userc.MESSENGER)
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

        private int FindCOMMENTSSCREENSHOTS(string t, UserControCase userc)
        {
            IList<string> lignes = new List<string>();
            Dictionary<string, string> targetFound = new Dictionary<string, string>();


            foreach (string ligne in userc.COMMENTSSCREENSHOTS)
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

        private int FindCOMMENTS(string t, UserControCase userc)
        {
            IList<string> lignes = new List<string>();
            Dictionary<string, string> targetFound = new Dictionary<string, string>();


            foreach (string ligne in userc.COMMENTS)
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

        private int FindLIKEPAGES(string t, UserControCase userc)
        {
            IList<string> lignes = new List<string>();
            Dictionary<string, string> targetFound = new Dictionary<string, string>();


            foreach (string ligne in userc.LIKEPAGES)
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

        private int FindPICTURESCOMMENTS(string t, UserControCase userc, string dir)
        {
            IList<string> lignes = new List<string>();


            foreach (string ligne in userc.PICTURESCOMMENTS)
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

        private int FindPICTURESLIKES(string t, UserControCase userc, string dir)
        {
            IList<string> lignes = new List<string>();

            
                foreach (string ligne in userc.PICTURESLIKES)
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

        private int FindTAGS(string t, UserControCase userc)
        {
            IList<string> lignes = new List<string>();
            Dictionary<string, string> targetFound = new Dictionary<string, string>();

            
                foreach (string ligne in userc.TAGS)
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

        private int FindHOMEPAGE(string t, UserControCase userc, string dir)
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



            foreach (string ligne in userc.HOMEPAGE)
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

        private int FindFRIENDS(string t, UserControCase userc)
        {
            IList<string> lignes = new List<string>();
           
            foreach (string ligne in userc.FRIENDS)
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

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    pictureBoxwaiting.Visible = false;
        //    string[] byUsernames = textBoxUSernames.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        //    //string[] byUrls = textBoxUrls.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        //    dataGridViewResults.Rows.Clear();
        //    lignesForSavingCase = new List<string>();
        //   //string[] allTargets = new string[1];

        //    ALLTARGETS = new Dictionary<string, string>();
        //    FRIENDS = new List<String>();
        //    HOMEPAGE = new List<String>();
        //    TAGS = new List<String>();
        //    PICTURESLIKES = new List<String>();
        //    PICTURESCOMMENTS = new List<String>();
        //    LIKEPAGES = new List<String>();
        //    COMMENTS = new List<String>();
        //    COMMENTSSCREENSHOTS = new List<String>();
        //    MESSENGER = new List<String>();
        //    FOLLOWERS = new List<String>();
        //    GROUPS = new List<String>();
        //    //Array.Resize(ref allTargets, (allTargets.Length + byUsernames.Length + byUrls.Length));

        //    //foreach (Control c in tabUsers.Controls)
        //    //{
        //    //    tabUsers.Controls.Remove(c);
        //    //}
        //    tabUsers.TabPages.Clear();

        //    foreach (string s in byUsernames)
        //    {
        //        if (!ALLTARGETS.ContainsKey(s))
        //            ALLTARGETS.Add(s, s);               
               
        //    }

        //    foreach(DataGridViewRow row in dataGridViewTargets.SelectedRows)
        //    {
        //        if (!ALLTARGETS.ContainsKey(row.Cells[0].Value.ToString() + row.Cells[1].Value.ToString()))
        //            ALLTARGETS.Add(row.Cells[0].Value.ToString() + row.Cells[0].Value.ToString(), row.Cells[0].Value.ToString() +";" + (row.Cells[1].Value.ToString() == "aucune" ? "null" : row.Cells[1].Value.ToString()));
        //    }

        //    //foreach (string s in byUrls)
        //    //{
        //    //    if (!ALLTARGETS.ContainsKey(s))
        //    //        ALLTARGETS.Add(s, s);
        //    //}


        //    foreach (string targ in ALLTARGETS.Values)
        //    {

        //        ALLTARGETS = new Dictionary<string, string>();
        //        FRIENDS = new List<String>();
        //        HOMEPAGE = new List<String>();
        //        TAGS = new List<String>();
        //        PICTURESLIKES = new List<String>();
        //        PICTURESCOMMENTS = new List<String>();
        //        LIKEPAGES = new List<String>();
        //        COMMENTS = new List<String>();
        //        COMMENTSSCREENSHOTS = new List<String>();
        //        MESSENGER = new List<String>();
        //        FOLLOWERS = new List<String>();
        //        GROUPS = new List<String>();
        //        lignesForSavingCase = new List<string>();



        //        if (IsTargetFound(targ))
        //        {
        //            userControl = new UserControl1();
        //            FindResults(targ);
        //            PopulateTargetsFound(targ.Split(';')[0]);

        //            Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxwaiting.Image")));
        //            dataGridViewResults.Rows.Add(img, targ.Replace(";", "@").Replace("null", "aucune"), hits.ToString());

        //        }
        //        else
        //        {
        //            Image img = global::Facebook_Anaytics.Properties.Resources.ko;
        //            dataGridViewResults.Rows.Add(img, targ, "0");
        //            continue;
        //        }

        //    }

        //    //PopulateTargetsFound(ALLTARGETS);
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    dataGridViewResults.Rows.Clear();
        //}

        //private void dataGridViewTargets_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    ALLTARGETS = new Dictionary<string, string>();
        //    FRIENDS = new List<String>();
        //    HOMEPAGE = new List<String>();
        //    TAGS = new List<String>();
        //    PICTURESLIKES = new List<String>();
        //    PICTURESCOMMENTS = new List<String>();
        //    LIKEPAGES = new List<String>();
        //    COMMENTS = new List<String>();
        //    COMMENTSSCREENSHOTS = new List<String>();
        //    MESSENGER = new List<String>();
        //    FOLLOWERS = new List<String>();
        //    GROUPS = new List<String>();

        //    tabUsers.TabPages.Clear();
        //    dataGridViewResults.Rows.Clear();


        //    //foreach (DataGridViewRow row in dataGridViewTargets.SelectedRows)
        //    //{
        //    //    if (!ALLTARGETS.ContainsKey(row.Cells[0].Value.ToString() + row.Cells[1].Value.ToString()))
        //    //        ALLTARGETS.Add(row.Cells[0].Value.ToString() + row.Cells[0].Value.ToString(), row.Cells[0].Value.ToString() + ";" + row.Cells[1].Value.ToString());
        //    //}
        //    // if (!ALLTARGETS.ContainsKey(row.Cells[0].Value.ToString() + row.Cells[1].Value.ToString()))
        //    //ALLTARGETS.Add(row.Cells[0].Value.ToString() + row.Cells[0].Value.ToString(), row.Cells[0].Value.ToString() + ";" + (row.Cells[1].Value.ToString() == "aucune" ? "null" : row.Cells[1].Value.ToString()));

        //    string targ = dataGridViewTargets.SelectedRows[0].Cells[0].Value.ToString() + ";" + (dataGridViewTargets.SelectedRows[0].Cells[1].Value.ToString() == "aucune" ? "null" : dataGridViewTargets.SelectedRows[0].Cells[1].Value.ToString());

        //    if (IsTargetFound(targ))
        //    {
        //        userControl = new UserControl1();
        //        FindResults(targ);
        //        PopulateTargetsFound(targ.Split(';')[0]);

        //        Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxwaiting.Image")));
        //        dataGridViewResults.Rows.Add(img, targ.Replace(";", "@").Replace("null","aucune"),hits.ToString());

        //    }
        //    else
        //    {
        //        Image img = global::Facebook_Anaytics.Properties.Resources.ko;
        //        dataGridViewResults.Rows.Add(img, targ.Replace(";", "@").Replace("null", "aucune"),"0");

        //    }
        //}

        //private void dataGridViewResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    foreach (TabPage tab in tabUsers.TabPages)
        //    {
        //        if (tab.Text == dataGridViewResults.Rows[e.RowIndex].Cells[1].Value.ToString().Split('@')[0])
        //        {
        //            tabUsers.SelectedTab = tab;
        //            TabPage pg = tabUsers.SelectedTab;
        //            Main.SelectedTab = Main.TabPages[1];
        //            //Main.SelectedTab = Main.TabPages[1];
        //            break;
        //        }



        //    }
        //}

        //private void tabUsers_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //string targ = tabUsers.SelectedTab.Text;


        //    //if (IsTargetFound(targ))
        //    //{
        //    //    userControl = new UserControl1();
        //    //    FindResults(targ + ";null");
        //    //    PopulateTargetsFound(targ.Split(';')[0]);

        //    //    Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxwaiting.Image")));
        //    //    dataGridViewResults.Rows.Add(img, targ.Replace(";", "@").Replace("null", "aucune"), hits.ToString());

        //    //}
        //    //else
        //    //{
        //    //    Image img = global::Facebook_Anaytics.Properties.Resources.ko;
        //    //    dataGridViewResults.Rows.Add(img, targ, "0");
                
        //    //}
        //}

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {

                Cases.Controls.Clear();
                textBoxRootDir.Text = folderBrowserDialog1.SelectedPath;

                IsANewThread(folderBrowserDialog1.SelectedPath);
                pictureBoxwaiting.Visible = true;
                pictureBoxlogofacebook.Visible = true;

                pathToSave = folderBrowserDialog1.SelectedPath;
                //FillCases(folderBrowserDialog1.SelectedPath);
            }
        }

        public void IsANewThread(string param)
        {
            if (backgroundWorker1 != null && backgroundWorker1.IsBusy)
                return;

            else
                    if (backgroundWorker1 != null)
                backgroundWorker1.RunWorkerAsync(param);
            else
            {
                //Reset();

                backgroundWorker1 = new BackgroundWorker();

                backgroundWorker1.WorkerReportsProgress = true;

                backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

                backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);

                backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);

                backgroundWorker1.WorkerSupportsCancellation = true;
                backgroundWorker1.RunWorkerAsync(param);
            }
        }

        private void FillCases(string rootDir)
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
            FRIENDS = new List<String>();

            foreach (string dir in Directory.GetDirectories(rootDir))
            {
                DirectoryInfo directory = new DirectoryInfo(dir);

                if (File.Exists(directory.FullName + "\\TXT\\ForAnalytics.txt"))
                {
                    string[] lignes = File.ReadAllLines(directory.FullName + "\\TXT\\ForAnalytics.txt");
                    ForAnalytics = File.ReadAllText(directory.FullName + "\\TXT\\ForAnalytics.txt");


                    foreach (string ss in lignes)
                        ALLForAnalytics.Add(ss.Replace("&sk=photos","") + dir);



                }
                else
                    return;


                var sorted = Directory.GetFiles(directory.FullName, "*.fbv").Select(fn => new FileInfo(fn)).OrderBy(f => f.CreationTime);
                FileInfo[] fichiers = sorted.ToArray();

                foreach (FileInfo fichier in fichiers)
                {

                    Casedata ca = Import(fichier.FullName);

                   

                    ca.NbreId = InitializeGridTargets(directory.FullName + "\\TXT\\ForAnalytics.txt").ToString();
                    ca.PathToFolder = dir;

                    backgroundWorker1.ReportProgress(-1, ca);

                    //UserControCase casee = new UserControCase(ca);

                    //InitializeCaseData(casee, directory.FullName + "\\TXT\\ForAnalytics.txt");

                    //Cases.Controls.Add(casee);

                }

               

            }
            
            //backgroundWorker1.ReportProgress(-2);
            //Thread.Sleep(1000);

            //on remplit la grille

            Dictionary<string, Casedata> hits = new Dictionary<string, Casedata>();
            foreach (string ss in ALLForAnalytics)
            {
                string toFind = ss.Split(';')[1];
                string dir = ss.Split(';')[5];


                foreach (Control c in Cases.Controls)
                {
                    UserControCase casee = (UserControCase)c;
                    if (toFind.Contains("anonymous"))
                        continue;

                    List<string> resultatsRecherche = casee.ALL.FindAll(x => x.Contains(toFind));

                    foreach (string r in resultatsRecherche)
                    {
                        string[] parameters = r.Split(';');
                        Casedata data = new Casedata();

                        string trouve = r.Split(';')[1];
                        trouve = trouve.Substring(trouve.IndexOf("Key=") + 4).Split('@')[0];

                        string url = r.Split(';')[1].Substring(r.Split(';')[1].IndexOf("@") + 1).Split('@')[0];

                        data.Category = parameters[0];
                        data.PathToFile = parameters[4];
                        data.Label = casee.Name;
                        data.Username = trouve;
                        data.Url = url;
                        data.PathToFolder = dir;



                        if (!hits.ContainsKey(data.Username.Trim() + data.Url.Trim() + data.Label.Trim()))
                            hits.Add(data.Username.Trim() + data.Url.Trim() + data.Label.Trim(), data);


                    }
                    //FindResults(targ,casee);
                }
            }

            foreach (Casedata t in hits.Values)
            {

                dataGridView1.Rows.Add(t.Username, t.Url, t.Label, t.Category, t.PathToFolder + t.PathToFile + ";" + t.PathToFolder);
            }

            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);

            int i = 0;
            Dictionary<string, string> hitss = new Dictionary<string, string>();
            Dictionary<string, string> hitsToShow = new Dictionary<string, string>();

            foreach (DataGridViewRow ro in dataGridView1.Rows)
            {
                if (i > dataGridView1.Rows.Count)
                    break;



                if (i < dataGridView1.Rows.Count - 1)
                    if ((dataGridView1.Rows[i].Cells[0].Value.ToString() == dataGridView1.Rows[i + 1].Cells[0].Value.ToString()) && (dataGridView1.Rows[i].Cells[2].Value.ToString() != dataGridView1.Rows[i + 1].Cells[2].Value.ToString()))
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        dataGridView1.Rows[i + 1].DefaultCellStyle.BackColor = Color.Red;

                        if (!hitss.ContainsKey(dataGridView1.Rows[i].Cells[0].Value.ToString() + dataGridView1.Rows[i].Cells[1].Value.ToString() + dataGridView1.Rows[i].Cells[2].Value.ToString()))
                            hitss.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + dataGridView1.Rows[i].Cells[1].Value.ToString() + dataGridView1.Rows[i].Cells[2].Value.ToString(), dataGridView1.Rows[i].Cells[0].Value.ToString() + ";" + dataGridView1.Rows[i].Cells[1].Value.ToString() + ";" + dataGridView1.Rows[i].Cells[2].Value.ToString() + ";" + dataGridView1.Rows[i].Cells[3].Value.ToString() + ";" + dataGridView1.Rows[i].Cells[4].Value.ToString());

                        if (!hitss.ContainsKey(dataGridView1.Rows[i + 1].Cells[0].Value.ToString() + dataGridView1.Rows[i + 1].Cells[1].Value.ToString() + dataGridView1.Rows[i + 1].Cells[2].Value.ToString()))
                            hitss.Add(dataGridView1.Rows[i + 1].Cells[0].Value.ToString() + dataGridView1.Rows[i + 1].Cells[1].Value.ToString() + dataGridView1.Rows[i + 1].Cells[2].Value.ToString(), dataGridView1.Rows[i + 1].Cells[0].Value.ToString() + ";" + dataGridView1.Rows[i + 1].Cells[1].Value.ToString() + ";" + dataGridView1.Rows[i + 1].Cells[2].Value.ToString() + ";" + dataGridView1.Rows[i + 1].Cells[3].Value.ToString() + ";" + dataGridView1.Rows[i + 1].Cells[4].Value.ToString());

                        



                        //Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxwaiting.Image")));
                        //dataGridViewResults.Rows.Add(img, dataGridView1.Rows[i].Cells[0].Value);

                        //if (dataGridView1.Rows[i].Cells[1].Value.ToString().Substring(dataGridView1.Rows[i].Cells[1].Value.ToString().LastIndexOf("/") + 1) == dataGridView1.Rows[i].Cells[2].Value.ToString().Split(new string[] { " -" }, StringSplitOptions.RemoveEmptyEntries)[0])
                        //{
                        //    i++;
                        //    continue;
                        //}



                        //dataGridView2.Rows.Add(dataGridView1.Rows[i].Cells[0].Value, dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value, dataGridView1.Rows[i].Cells[3].Value, dataGridView1.Rows[i].Cells[4].Value);
                        //dataGridView2.Rows.Add(dataGridView1.Rows[i+1].Cells[0].Value, dataGridView1.Rows[i+1].Cells[1].Value, dataGridView1.Rows[i+1].Cells[2].Value, dataGridView1.Rows[i+1].Cells[3].Value, dataGridView1.Rows[i+1].Cells[4].Value);
                        ////i++;
                        //dataGridView2.Rows.Add(dataGridView1.Rows[i + 1]);
                    }

                i++;

            }

            string firstUser = "";
            Dictionary<string, string> waiting = new Dictionary<string, string>();
            foreach (string resu in hitss.Values)
            {
                string[] pa = resu.Split(';');
                string debutUsername = pa[1].Substring(pa[1].LastIndexOf("/") + 1);
                string finEcase = pa[2].Split(new string[] { " -" }, StringSplitOptions.RemoveEmptyEntries)[0];
                Casedata casee = new Casedata();

                if (debutUsername.Trim() == finEcase.Trim())
                    continue;

                Color coleur = Color.Orange;
                Color coleurautre = Color.Green;

                //if (!waiting.ContainsKey(pa[0] + pa[2]))
                //{
                //    groups.Add(casee);
                //    waiting.Add(pa[0] + pa[2], pa[0] + pa[2]);

                //}


                if (firstUser != pa[0])
                {
                    dataGridView2.Rows.Add("", "", "", "", "");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.White;
                    dataGridView2.Rows.Add(pa[0], pa[1], pa[2], pa[3], pa[4]);
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;

                    casee.GroupeUsername = finEcase;
                    casee.Username = pa[0];
                    casee.Url = pa[1];
                    casee.GroupeName = pa[2];
                    casee.PathToFolder = pa[5];

                    //if (!waiting.ContainsKey(pa[0] + pa[2]))
                    //{
                    //    groups.Add(casee);
                    //    waiting.Add(pa[0] + pa[2], pa[0] + pa[2]);

                    //}

                    groups.Add(casee);
                    firstUser = pa[0];
                }
                else
                {
                    dataGridView2.Rows.Add("", pa[1], pa[2], pa[3], pa[4]);
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0].Style.ForeColor = Color.White;

                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.White;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Style.BackColor = Color.LightGray;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[4].Style.BackColor = Color.LightGray;
                    casee.GroupeUsername = finEcase;
                    casee.Username = pa[0];
                    casee.Url = pa[1];
                    casee.GroupeName = pa[2];
                    casee.PathToFolder = pa[5];

                    //if (!waiting.ContainsKey(pa[0] + pa[2]))
                    //{
                    //    groups.Add(casee);
                    //    waiting.Add(pa[0] + pa[2], pa[0] + pa[2]);

                    //}

                    groups.Add(casee);
                }
                

                if (!hitsToShow.ContainsKey(pa[0] + pa[1]))
                {
                    hitsToShow.Add(pa[0] + pa[1], pa[0]);
                    backgroundWorker1.ReportProgress(-22, pa[0]);
                }

            }

            //foreach (string resuv in hitsToShow.Values)
            //{
            //    string[] pa = resuv.Split(';');

            //    Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxwaiting.Image")));
            //    dataGridViewResults.Rows.Add(img, pa[0]);
            //}

            //backgroundWorker1.CancelAsync();
            //Thread.Sleep(1000);

        }

        public string FindResults(string target, string pathToSave)
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

            string[] para = target.Split(';');
            pathToSave = para[2];
            target = para[0] + ";" + para[1];

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
                    userControl.FillResume("HOMEPAGE", FindHOMEPAGE(target).ToString());
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
                    userControl.FillResume("COMMENTSIMAGES", FindPICTURESCOMMENTS(target).ToString());
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
                    userControl.FillResume("MESSENGER", FindMESSENGERS(target).ToString());
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

            //pictureBoxwaiting.Visible = true;
            return "test";
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
                string[] parames = ligne.Split(';');



                string tofind1 = t.Split(';')[0];
                string tofind2 = t.Split(';')[1];

                if (ligne.Contains("Key=" + tofind1) || ligne.Contains("@" + tofind2 + "@"))
                {

                    resultatsRechercheSuite = resultatsRecherche.FindAll(x => x.Contains(parames[4]));

                    if (resultatsRechercheSuite.Count > 0)
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

        private void dataGridViewTargets_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
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
            //tabUsers.TabPages.Clear();

            foreach (string s in byUsernames)
            {
                if (!ALLTARGETS.ContainsKey(s))
                    ALLTARGETS.Add(s, s);

            }

            Dictionary<string, Casedata> hits = new Dictionary<string, Casedata>();
            foreach(string ss in ALLForAnalytics)
            {
                string toFind = ss.Split(';')[1];
                
                
                foreach (Control c in Cases.Controls)
                {
                    UserControCase casee = (UserControCase)c;
                    if (toFind.Contains("Régis Du Bois"))
                        ;

                    List<string> resultatsRecherche = casee.ALL.FindAll(x => x.Contains(toFind));

                    foreach (string r in resultatsRecherche)
                    {
                        string[] parameters = r.Split(';');
                        Casedata data = new Casedata();

                        string trouve = r.Split(';')[1];
                        trouve = trouve.Substring(trouve.IndexOf("Key=") + 4).Split('@')[0];

                        string url = r.Split(';')[1].Substring(r.Split(';')[1].IndexOf("@") + 1).Split('@')[0];

                        data.Category = parameters[0];
                        data.PathToFile = parameters[4];
                        data.Label = casee.Name;
                        data.Username = trouve;
                        data.Url = url;

                     

                        if (!hits.ContainsKey(data.Username.Trim() + data.Url.Trim() + data.Label.Trim()))
                            hits.Add(data.Username.Trim() + data.Url.Trim() + data.Label.Trim(), data);

                                               
                    }
                    //FindResults(targ,casee);
                }
            }

            foreach(Casedata t in hits.Values)
            {

              dataGridView1.Rows.Add(t.Username,t.Url,t.Label, t.Category, t.PathToFolder + t.PathToFile);
            }

            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);

            int i = 0;
            foreach(DataGridViewRow ro in dataGridView1.Rows)
            {
                if (i > dataGridView1.Rows.Count)
                    break;

               

                if (i < dataGridView1.Rows.Count - 1)
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == dataGridView1.Rows[i + 1].Cells[0].Value.ToString())
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        dataGridView1.Rows[i+1].DefaultCellStyle.BackColor = Color.Red;
                    }

                i++;

            }


            return;

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

                


                //if (IsTargetFound(targ))
                //{
                    userControl = new UserControl1();
                    //FindResults(targ);
                    //PopulateTargetsFound(targ.Split(';')[0]);

                    Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxwaiting.Image")));
                    dataGridViewResults.Rows.Add(img, targ.Replace(";", "@").Replace("null", "aucune"), hits.ToString());

                //}
                //else
                //{
                //    Image img = global::Facebook_Anaytics.Properties.Resources.ko;
                //    dataGridViewResults.Rows.Add(img, targ, "0");
                //    continue;
                //}

            }
        }

        private void dataGridViewResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            //try
            //{
            //    //FillJournalView();
            //    //Thread.Sleep(500);
            //    _init.GetProgressBar().Value += 1;
            //    _init.GetProgressBar().Update();
            //    //Thread.Sleep(500);

            //    //FillPicturesView();
            //    //Thread.Sleep(500);
            //    _init.GetProgressBar().Value += 1;
            //    _init.GetProgressBar().Update();

            //    foreach (Form f in Application.OpenForms)
            //    {

            //        if (f.Name == "Initcontrol")
            //        {

            //            f.Close();


            //            this.WindowState = FormWindowState.Maximized;
            //            break;
            //        }

                    

            //    }

            //    //foreach (Form f in Application.OpenForms)
            //    //{

            //    //    if (f.Name == "Form1")
            //    //    {

            //    //        f.Close();


            //    //        this.WindowState = FormWindowState.Maximized;
            //    //        break;
            //    //    }



            //    //}
            //    this.Opacity = 100;
            //    this.Show();
            //}
            //catch
            //{
            //    foreach (Form f in Application.OpenForms)
            //    {

            //        if (f.Name == "Initcontrol")
            //        {

            //            f.Close();


            //            this.WindowState = FormWindowState.Maximized;
            //            break;
            //        }



            //    }

            //    foreach (Form f in Application.OpenForms)
            //    {

            //        if (f.Name == "Form1")
            //        {

            //            f.Close();


            //            this.WindowState = FormWindowState.Maximized;
            //            break;
            //        }



            //    }
            //    this.Opacity = 100;
            //    this.Show();
            //}
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker1.CancellationPending)
            {
                FillCases(folderBrowserDialog1.SelectedPath);
                PopulateTargetsFound();
            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == -1)
            {
                Casedata id = ((Casedata)e.UserState);

                UserControCase casee = new UserControCase(id);

                DirectoryInfo directory = new DirectoryInfo(id.PathToFolder);

                InitializeCaseData(casee, directory.FullName + "\\TXT\\ForAnalytics.txt");

                Cases.Controls.Add(casee);

            }

            if (e.ProgressPercentage == -22)
            {
                string id = ((string)e.UserState);

                Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxwaiting.Image")));
                dataGridViewResults.Rows.Add(img,id);

            }

            if(e.ProgressPercentage == -666)
            {
                tabTarget.TabPages.Add(((TabPage)e.UserState));
                //Main.TabPages.Add(tabTarget);
                

            }

            if (e.ProgressPercentage == -2)
            {
                //on remplit la grille

                Dictionary<string, Casedata> hits = new Dictionary<string, Casedata>();
                foreach (string ss in ALLForAnalytics)
                {
                    string toFind = ss.Split(';')[1];


                    foreach (Control c in Cases.Controls)
                    {
                        UserControCase casee = (UserControCase)c;
                        if (toFind.Contains("anonymous"))
                            continue;

                        List<string> resultatsRecherche = casee.ALL.FindAll(x => x.Contains(toFind));

                        foreach (string r in resultatsRecherche)
                        {
                            string[] parameters = r.Split(';');
                            Casedata data = new Casedata();

                            string trouve = r.Split(';')[1];
                            trouve = trouve.Substring(trouve.IndexOf("Key=") + 4).Split('@')[0];

                            string url = r.Split(';')[1].Substring(r.Split(';')[1].IndexOf("@") + 1).Split('@')[0];

                            data.Category = parameters[0];
                            data.PathToFile = parameters[4];
                            data.Label = casee.Name;
                            data.Username = trouve;
                            data.Url = url;



                            if (!hits.ContainsKey(data.Username.Trim() + data.Url.Trim() + data.Label.Trim()))
                                hits.Add(data.Username.Trim() + data.Url.Trim() + data.Label.Trim(), data);


                        }
                        //FindResults(targ,casee);
                    }
                }

                foreach (Casedata t in hits.Values)
                {

                    dataGridView1.Rows.Add(t.Username, t.Url, t.Label, t.Category, t.PathToFolder + t.PathToFile);
                }

                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);

                int i = 0;
                Dictionary<string, string> hitss = new Dictionary<string, string>();
                Dictionary<string, string> hitsToShow = new Dictionary<string, string>();

                foreach (DataGridViewRow ro in dataGridView1.Rows)
                {
                    if (i > dataGridView1.Rows.Count)
                        break;



                    if (i < dataGridView1.Rows.Count - 1)
                        if ((dataGridView1.Rows[i].Cells[0].Value.ToString() == dataGridView1.Rows[i + 1].Cells[0].Value.ToString()) && (dataGridView1.Rows[i].Cells[2].Value.ToString() != dataGridView1.Rows[i + 1].Cells[2].Value.ToString()))
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            dataGridView1.Rows[i + 1].DefaultCellStyle.BackColor = Color.Red;

                            if (!hitss.ContainsKey(dataGridView1.Rows[i].Cells[0].Value.ToString() + dataGridView1.Rows[i].Cells[1].Value.ToString() + dataGridView1.Rows[i].Cells[2].Value.ToString()))
                                hitss.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + dataGridView1.Rows[i].Cells[1].Value.ToString() + dataGridView1.Rows[i].Cells[2].Value.ToString(), dataGridView1.Rows[i].Cells[0].Value.ToString() + ";" + dataGridView1.Rows[i].Cells[1].Value.ToString() + ";" + dataGridView1.Rows[i].Cells[2].Value.ToString() + ";" + dataGridView1.Rows[i].Cells[3].Value.ToString() + ";" + dataGridView1.Rows[i].Cells[4].Value.ToString());

                            if (!hitss.ContainsKey(dataGridView1.Rows[i + 1].Cells[0].Value.ToString() + dataGridView1.Rows[i + 1].Cells[1].Value.ToString() + dataGridView1.Rows[i + 1].Cells[2].Value.ToString()))
                                hitss.Add(dataGridView1.Rows[i + 1].Cells[0].Value.ToString() + dataGridView1.Rows[i + 1].Cells[1].Value.ToString() + dataGridView1.Rows[i + 1].Cells[2].Value.ToString(), dataGridView1.Rows[i + 1].Cells[0].Value.ToString() + ";" + dataGridView1.Rows[i + 1].Cells[1].Value.ToString() + ";" + dataGridView1.Rows[i + 1].Cells[2].Value.ToString() + ";" + dataGridView1.Rows[i + 1].Cells[3].Value.ToString() + ";" + dataGridView1.Rows[i + 1].Cells[4].Value.ToString());


                            if (!hitsToShow.ContainsKey(dataGridView1.Rows[i].Cells[0].Value.ToString() + dataGridView1.Rows[i].Cells[1].Value.ToString()))
                                hitsToShow.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + dataGridView1.Rows[i].Cells[1].Value.ToString(), dataGridView1.Rows[i].Cells[0].Value.ToString() + ";" + dataGridView1.Rows[i].Cells[1].Value.ToString());
                            //Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxwaiting.Image")));
                            //dataGridViewResults.Rows.Add(img, dataGridView1.Rows[i].Cells[0].Value);

                            //if (dataGridView1.Rows[i].Cells[1].Value.ToString().Substring(dataGridView1.Rows[i].Cells[1].Value.ToString().LastIndexOf("/") + 1) == dataGridView1.Rows[i].Cells[2].Value.ToString().Split(new string[] { " -" }, StringSplitOptions.RemoveEmptyEntries)[0])
                            //{
                            //    i++;
                            //    continue;
                            //}



                            //dataGridView2.Rows.Add(dataGridView1.Rows[i].Cells[0].Value, dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value, dataGridView1.Rows[i].Cells[3].Value, dataGridView1.Rows[i].Cells[4].Value);
                            //dataGridView2.Rows.Add(dataGridView1.Rows[i+1].Cells[0].Value, dataGridView1.Rows[i+1].Cells[1].Value, dataGridView1.Rows[i+1].Cells[2].Value, dataGridView1.Rows[i+1].Cells[3].Value, dataGridView1.Rows[i+1].Cells[4].Value);
                            ////i++;
                            //dataGridView2.Rows.Add(dataGridView1.Rows[i + 1]);
                        }

                    i++;

                }


                foreach (string resu in hitss.Values)
                {
                    string[] pa = resu.Split(';');
                    dataGridView2.Rows.Add(pa[0], pa[1], pa[2], pa[3], pa[4]);

                    

                }

                foreach(string resuv in hitsToShow.Values)
                {
                    string [] pa = resuv.Split(';');
                    
                    Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxwaiting.Image")));
                    dataGridViewResults.Rows.Add(img, pa[0]);
                }


            }

            if (e.ProgressPercentage == -3)
            {

            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBoxwaiting.Visible = false;
            pictureBoxlogofacebook.Visible = false;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView2.Rows[e.RowIndex].Cells[0].Style.SelectionForeColor = Color.DodgerBlue;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] pa = label2.Text.Split(';');
            pathToSave = pa[2] + "\\";
            userControl = new UserControl1(pa[2]);
            FindResults(label2.Text,"");
            PopulateTargetsFound(label2.Text.Split(';')[0]);



            //fiche = new Fiche(label2);
            //TabPage tab = new TabPage("test");


            //fiche.Dock = DockStyle.Fill;

            ////Panel line = new Panel();
            ////line.Size = new Size(80, 10);
            ////line.Location = new Point(tab., 3);
            ////line.BackColor = Color.DeepSkyBlue;
            ////tab.Controls.Add(line);
            ////userControl.SetForSaving(lignesForSavingCase, target, IDForSaving);
            //tab.Controls.Add(fiche);
            //Main.TabPages.Add(tab);
        }

        public bool PopulateTargetsFound(string target)
        {
            foreach (TabPage t in tabUsers.TabPages)
            {
                if (t.Text == target)
                {
                    tabUsers.TabPages.Remove(t);
                }
            }


            TabPage tab = new TabPage(target);

            string[] pa = target.Split(';');

           
            userControl.Dock = DockStyle.Fill;

            //Panel line = new Panel();
            //line.Size = new Size(80, 10);
            //line.Location = new Point(tab., 3);
            //line.BackColor = Color.DeepSkyBlue;
            //tab.Controls.Add(line);
            userControl.SetForSaving(lignesForSavingCase, target, IDForSaving);
            tab.Controls.Add(userControl);
            tabUsers.TabPages.Add(tab);

            


            tabUsers.SelectedTab = tabUsers.TabPages[tabUsers.TabCount - 1];
            Main.SelectedTab = Main.TabPages["byUser"];

            //tabUsers.Controls.Remove(tabUsers.Controls["tabPage3"]);
            //tabUsers.Controls.Remove(tabUsers.Controls["tabPage4"]);

            return true;
        }

        public bool PopulateTargetsFound()
        {
            if (groups.Count == 0)
            {
                backgroundWorker1.CancelAsync();
                return false;
            }
                

            foreach (Control c in Cases.Controls)
            {
                UserControCase casee = (UserControCase)c;
                List<Casedata> groupeByName = new List<Casedata>();
                List<Casedata> groupeByNameTmp = new List<Casedata>();

                fiche = new Fiche(label2);

                string firstUser = groups[0].Username;
                string urll = groups[0].Url;
                string clepourprepa = "";
                foreach (Casedata caseee in groups)
                {
                    //groupeByNameTmp.Add(caseee);

                    if (caseee.Username != firstUser && caseee.Url != urll)
                    {
                        foreach(Casedata tmp in groupeByNameTmp)
                        {
                            if (tmp.Username == "Frank Bast")
                                ;

                            if (tmp.GroupeName == casee.Name)
                            {
                                foreach (Casedata tmpp in groupeByNameTmp)
                                {
                                    foreach (Control cc in Cases.Controls)
                                    {
                                        UserControCase cass = (UserControCase)cc;
                                        if(tmpp.GroupeName == cass.Name)
                                        {
                                            tmpp.ImageProfile = cass.ImageProfile;
                                            //images.Add(tmpp.ImageProfile);
                                            break;

                                        }

                                    }

                                    clepourprepa += tmpp.GroupeName+";";
                                    //groupeByName.Add(groupeByNameTmp[0]);
                                }

                                //groupeByName.Add(groupeByNameTmp[0]);
                            }
                        }


                        //string firstUserB = ""; //groupeByNameTmp[0].Username;
                        //string urllB = ""; //groupeByNameTmp[0].Url;
                        Dictionary<string, string> tmppp = new Dictionary<string, string>();

                        foreach (Casedata dd in groupeByNameTmp)
                        {
                            if(! tmppp.ContainsKey(dd.Username + dd.Url))
                            {
                                groupeByName.Add(dd);
                                //urllB = dd.Url;
                                //firstUserB = dd.Username;
                                tmppp.Add(dd.Username + dd.Url , dd.Username + dd.Url );
                            }
                        }

                        //groupeByName.Add(groupeByNameTmp[0]);
                        firstUser = caseee.Username;
                        urll = caseee.Url;
                        groupeByNameTmp = new List<Casedata>();

                        if(groupeByName.Count > 0)
                        {
                            //fiche.AddTableLayout(groupeByName);
                            //fiche.AddTableLayout(groupeByName);
                            if (clepourprepa != "")
                                PrepareDataForControl(clepourprepa, groupeByName);
                            groupeByName = new List<Casedata>();
                            clepourprepa = "";
                            
                        }

                        
                    }

                    groupeByNameTmp.Add(caseee);
                }

                if (groupeByNameTmp.Count > 0)
                {
                    foreach (Casedata tmp in groupeByNameTmp)
                    {
                        if (tmp.GroupeName == casee.Name)
                        {
                            foreach (Casedata tmpp in groupeByNameTmp)
                            {
                                foreach (Control cc in Cases.Controls)
                                {
                                    UserControCase cass = (UserControCase)cc;
                                    if (tmpp.GroupeName == cass.Name)
                                    {
                                        tmpp.ImageProfile = cass.ImageProfile;
                                        //images.Add(tmpp.ImageProfile);
                                        break;

                                    }

                                }

                                clepourprepa += tmpp.GroupeName + ";";
                                //groupeByName.Add(groupeByNameTmp[0]);
                            }

                            //groupeByName.Add(groupeByNameTmp[0]);
                        }
                    }


                    Dictionary<string, string> ttmp = new Dictionary<string, string>();

                    foreach (Casedata dd in groupeByNameTmp)
                    {
                        if (!ttmp.ContainsKey(dd.Username + dd.Url))
                        {
                            groupeByName.Add(dd);
                            //urllB = dd.Url;
                            //firstUserB = dd.Username;
                            ttmp.Add(dd.Username + dd.Url, dd.Username + dd.Url);
                        }
                    }

                    //groupeByName.Add(groupeByNameTmp[0]);
                    //fiche.AddTableLayout(groupeByName);
                    //fiche.AddTableLayout(groupeByName);
                    if(clepourprepa !="")
                        PrepareDataForControl(clepourprepa, groupeByName);

                    groupeByName = new List<Casedata>();
                    clepourprepa = "";

                }


                if (Preparedata.Keys.Count > 0)
                {
                    //foreach (List<Casedata> collec in Preparedata.Values)
                    //{

                    //    fiche.AddTableLayout(collec, images);
                    //}




                    
                    foreach (string cle in Preparedata.Keys)
                    {
                        string[] para = cle.Split(new string [] { ";"},StringSplitOptions.RemoveEmptyEntries);
                        Image profil = null;
                        Dictionary<string, string> tmp = new Dictionary<string, string>();

                        foreach (string cc in para)
                        {
                            foreach (Control ccc in Cases.Controls)
                            {
                                UserControCase cass = (UserControCase)ccc;
                                if (cc != casee.Name && cass.Name == cc)
                                {
                                    if (!tmp.ContainsKey(cass.Name))
                                    {
                                        images.Add(cass.ImageProfile);
                                        tmp.Add(cass.Name, cass.Name);
                                        break;
                                    }
                                   

                                }
                                else
                                {
                                    continue;
                                }

                                

                            }


                        }

                        string t = "" ;

                        foreach (string keyy in Preparedata.Keys)
                        {
                            if (keyy == cle)
                            {
                                string[] paraa = keyy.Split(';');

                                foreach(string s in para)
                                {
                                    if (s != casee.Name)
                                    {
                                        t += s + ";";
                                    }
                                }
                                
                               
                                break;
                            }
                              
                        }
                        

                        fiche.AddTableLayout(Preparedata[cle], images, t);
                        images = new List<Image>();
                    }
                }



                TabPage tab = new TabPage(casee.url.ToUpper());

                fiche.SetProfilePicture(casee.ImageProfile);

                //string idd = casee.Name.Substring(casee.Name.IndexOf("ID : ") + 5);
                //string username = casee.Name.Split(new string[] { " -" }, StringSplitOptions.RemoveEmptyEntries)[0];

                fiche.SetProfile(casee.username, casee.idd, casee.url);
                
                fiche.Dock = DockStyle.Fill;

                //Panel line = new Panel();
                //line.Size = new Size(80, 10);
                //line.Location = new Point(tab., 3);
                //line.BackColor = Color.DeepSkyBlue;
                //tab.Controls.Add(line);
                //userControl.SetForSaving(lignesForSavingCase, target, IDForSaving);
               
                tab.Controls.Add(fiche);
                

                backgroundWorker1.ReportProgress(-666, tab);
                Preparedata = new Dictionary<string, List<Casedata>>();
                images = new List<Image>();
                //Main.TabPages.Add(tab);
                //Preparedata = new Dictionary<string, List<Casedata>>();

            }

            
            //if(Preparedata.Count > 0)
            //{
            //    foreach(List<Casedata> collec in Preparedata.Values)
            //    {
            //        fiche.AddTableLayout(collec);
            //    }
            //}



            //TabPage tab = new TabPage(target);


            //fiche.Dock = DockStyle.Fill;

            ////Panel line = new Panel();
            ////line.Size = new Size(80, 10);
            ////line.Location = new Point(tab., 3);
            ////line.BackColor = Color.DeepSkyBlue;
            ////tab.Controls.Add(line);
            ////userControl.SetForSaving(lignesForSavingCase, target, IDForSaving);
            //tab.Controls.Add(fiche);

            //Main.TabPages.Add(tab);



            //tabUsers.Controls.Remove(tabUsers.Controls["tabPage3"]);
            //tabUsers.Controls.Remove(tabUsers.Controls["tabPage4"]);
            backgroundWorker1.CancelAsync();
            return true;
        }

        private void PrepareDataForControl(string cle,List<Casedata> groupeByName)
        {
            
            string[] para = cle.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            int hit = 0;
            int nbreCle = 0;
            bool addto = false;

           foreach (string k in Preparedata.Keys)
           {
                
                string cletmp = "";
                string[] parakeys = k.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                hit = 0;
                nbreCle = parakeys.Count();

                if (nbreCle > para.Count())
                {
                    addto = false;
                    continue;
                }
                   

                foreach (string p in para)
                {
                    if (k.Contains(p))
                    {
                        cletmp += p + ";";
                        hit++;
                    }
                        
                }

                if (hit == para.Count() /*&& para.Count() == nbreCle*/)
                //if(Preparedata.ContainsKey(cletmp))
                {
                    List<Casedata> tmp = Preparedata[k];
                    foreach (Casedata casee in groupeByName)
                    {
                        tmp.Add(casee);
                    }
                    hit = 0;
                    addto = true;
                    break;
                }
                //else
                //{
                //    if (hit == nbreCle && !Preparedata.ContainsKey(cle))
                //        addto = true;//Preparedata.Add(cle, groupeByName);
                //}

            }

            if (!addto && !Preparedata.ContainsKey(cle))
            {
                Preparedata.Add(cle, groupeByName);
            }
            else
            if (hit != 0 && hit < para.Count() && !Preparedata.ContainsKey(cle))
            {
                Preparedata.Add(cle, groupeByName);
            }




            if (Preparedata.Keys.Count == 0)
                Preparedata.Add(cle, groupeByName);

            //if (Preparedata.ContainsKey(cle))
            //{
            //    List<Casedata> tmp = Preparedata[cle];
            //    foreach(Casedata casee in groupeByName)
            //    {
            //        tmp.Add(casee);
            //    }
            //}
            //else
            //{
            //    Preparedata.Add(cle, groupeByName);
            //}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_TextChanged(object sender, EventArgs e)
        {

            if (label2.Text == "")
                return;
            //DirectoryInfo dir = new DirectoryInfo(pathToSave);
            //DirectoryInfo[] dirs = dir.GetDirectories();

            //if (hitss.Conta)
            //{

            //}


            //    foreach (DirectoryInfo dirr in dirs)
            //{
            //    if(File.Exists(dirr + "\\TXT\\ForAnalytics.txt"))
            //    {

            //    }
            //}
            
            
            
            string[] pa = label2.Text.Split(';');
            pathToSave = pa[2] + "\\";
            userControl = new UserControl1(pa[2]);
            FindResults(label2.Text, "");
            PopulateTargetsFound(label2.Text.Split(';')[0]);

            label2.Text = "";
        }

        private void label2_Validating(object sender, CancelEventArgs e)
        {

        }
    }
}
