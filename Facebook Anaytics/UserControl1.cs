using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace Facebook_Anaytics
{
    public partial class UserControl1 : UserControl
    {
        public string pathToSave = AppDomain.CurrentDomain.BaseDirectory;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl1));
        bool profilSet = false;
        bool AutomaticMode = false;
        IList<string> lignesForSavingCase = new List<string>();
        IList<string> FilesForSaving = new List<string>();
        public string TargetForSaving;
        public string ProfileImageForSaving = "";
        public string UserNameForSaving = "";
        public string IDForSaving = "";
        public UserControl1()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            tabControl1.Controls.Remove(tabControl1.TabPages["ANNEXEPV"]);

            


        }

        public UserControl1(string path)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            tabControl1.Controls.Remove(tabControl1.TabPages["ANNEXEPV"]);
            pathToSave = path;




        }

        public void SetAutomaticMode(bool auto)
        {
            AutomaticMode = auto;

            if (auto)
                button18.Visible = false;
        }

        public void FillResume(string category, string nombre)
        {
            //if (nombre == "0")
            //    return;
            
            if (category== "HOMEPAGE")
            {
                if (nombre == "0")
                {
                    tabControl1.Controls.Remove(tabControl1.TabPages["JOURNAL"]);
                    return;
                }
                    


                Image img = ((System.Drawing.Image)(resources.GetObject("pictureBox31.Image")));
                dataGridViewResume.Rows.Add(img, nombre, "JOURNAL");
                //labelJournal.Text = "JOURNAL : " + GetNumberHomePage();

            }

            if (category == "TAGS")
            {
                if (nombre == "0")
                {
                    tabControl1.Controls.Remove(tabControl1.TabPages["TAGS"]);
                    return;
                }
                    

                Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxTags.Image")));
                dataGridViewResume.Rows.Add(img, nombre, "TAGS");
                //labelJournal.Text = "JOURNAL : " + GetNumberHomePage();

            }

            if (category == "LIKES")
            {
                if (nombre == "0")
                {
                    tabControl1.Controls.Remove(tabControl1.TabPages["LIKESIMAGES"]);
                    return;
                }
                   

                Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxLikes.Image")));
                dataGridViewResume.Rows.Add(img, nombre, "LIKES IMAGES");
                //labelJournal.Text = "JOURNAL : " + GetNumberHomePage();

            }

            if (category == "COMMENTS")
            {
                if (nombre == "0")
                {
                    tabControl1.Controls.Remove(tabControl1.TabPages["COMMENTS"]);
                    return;
                }
                    

                Image img = ((System.Drawing.Image)(resources.GetObject("pictureBox33.Image")));
                dataGridViewResume.Rows.Add(img, nombre, "COMMENTS");
                //labelJournal.Text = "JOURNAL : " + GetNumberHomePage();

            }

            if (category == "COMMENTSSCREENSHOTS")
            {
                if (nombre == "0")
                {
                    tabControl1.Controls.Remove(tabControl1.TabPages["COMMENTSSCREENSHOTS"]);
                    return;
                }


                Image img = ((System.Drawing.Image)(resources.GetObject("pictureBox33.Image")));
                dataGridViewResume.Rows.Add(img, nombre, "COMMENTSSCREENSHOTS");
                //labelJournal.Text = "JOURNAL : " + GetNumberHomePage();

            }

            if (category == "LIKEPAGES")
            {
                if (nombre == "0")
                {
                    tabControl1.Controls.Remove(tabControl1.TabPages["LIKESPAGES"]);
                    return;
                }

                Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxLikes.Image")));
                dataGridViewResume.Rows.Add(img, nombre, "LIKES PAGES");
                //labelJournal.Text = "JOURNAL : " + GetNumberHomePage();

            }

            if (category == "LIKEIMAGES")
            {
                if (nombre == "0")
                {
                    tabControl1.Controls.Remove(tabControl1.TabPages["LIKESIMAGES"]);
                    return;
                }

                Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxLikes.Image")));
                dataGridViewResume.Rows.Add(img, nombre, "LIKES PAGES");
                //labelJournal.Text = "JOURNAL : " + GetNumberHomePage();

            }

            if (category == "COMMENTSIMAGES")
            {
                if (nombre == "0")
                {
                    tabControl1.Controls.Remove(tabControl1.TabPages["COMMENTSIMAGES"]);
                    return;
                }

                Image img = ((System.Drawing.Image)(resources.GetObject("pictureBoxLikes.Image")));
                dataGridViewResume.Rows.Add(img, nombre, "COMMENTS IMAGES");
                //labelJournal.Text = "JOURNAL : " + GetNumberHomePage();

            }

            if (category == "FRIENDS")
            {
                if (nombre == "0")
                {
                    tabControl1.Controls.Remove(tabControl1.TabPages["AMIS"]);
                    return;
                }

                Image img = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
                dataGridViewResume.Rows.Add(img, nombre, "AMIS");
                //labelJournal.Text = "JOURNAL : " + GetNumberHomePage();

            }

            if (category == "MESSENGER")
            {
                if (nombre == "0")
                {
                    tabControl1.Controls.Remove(tabControl1.TabPages["MESSENGER"]);
                    return;
                }

                Image img = ((System.Drawing.Image)(resources.GetObject("pictureBox32.Image")));
                dataGridViewResume.Rows.Add(img, nombre, "MESSENGER");
                //labelJournal.Text = "JOURNAL : " + GetNumberHomePage();

            }



        }

        public void SetForSaving(IList<string> lignesForSavingCas, string target, string ID)
        {
            lignesForSavingCase = lignesForSavingCas;
            TargetForSaving = target;
            IDForSaving = ID;
            UserNameForSaving = target;
        }
        public int FillJournal(IList<string> lignes)
        {
            dataGridViewHomepage.Rows.Clear();
                   



            foreach (string li in lignes)
            {
                string fich = li.Split(';')[0];
                string comm = li.Split(';')[1];

                if (File.Exists(pathToSave + fich))
                {
                    FileInfo fichierr = new FileInfo(pathToSave + fich);
                    Image img = ResizeImage(fichierr.FullName, 200, 200, false);
                    dataGridViewHomepage.Rows.Add(img, comm, pathToSave + fich);
                    FilesForSaving.Add(fichierr.FullName);
                }

            }
            
            
            return dataGridViewHomepage.Rows.Count;
        }
        public int FillLikes(IList<string> lignes)
        {
            dataGridViewLikesPages.Rows.Clear();

            foreach (string li in lignes)
            {
                string fich = li.Split(';')[0];
                string comm = li.Split(';')[1];

                if (File.Exists(pathToSave + fich))
                {
                    FileInfo fichierr = new FileInfo(pathToSave + fich);
                    Image img = ResizeImage(fichierr.FullName, 150, 150, false);
                    dataGridViewLikesPages.Rows.Add(img, comm, pathToSave + fich);
                    FilesForSaving.Add(fichierr.FullName);
                }

            }

            if(!profilSet)
            try
            {
                FileInfo fichierr = new FileInfo(pathToSave + lignes[0].Split(';')[0]);
                pictureBox30.Image = ResizeImage(fichierr.FullName, 80, 80, false);
                ProfileImageForSaving = fichierr.FullName;

                profilSet = true;
            }
            catch
            {

            }

            return dataGridViewLikesPages.Rows.Count;
        }

        public int FillTags(IList<string> lignes)
        {
            dataGridViewTAGS.Rows.Clear();

            foreach (string li in lignes)
            {
                if (File.Exists(pathToSave + "PICTURES_TAGGED\\" + li))
                {
                    FileInfo fichierr = new FileInfo(pathToSave + "PICTURES_TAGGED\\" + li);
                    Image img = ResizeImage(fichierr.FullName, 400, 400, false);
                    dataGridViewTAGS.Rows.Add(img, "", pathToSave + "PICTURES_TAGGED\\" + li);
                    FilesForSaving.Add(fichierr.FullName);
                }

            }


            return dataGridViewTAGS.Rows.Count;
        }

        public int FillCommentsscreenshot(IList<string> lignes)
        {
            dataGridViewCommentsscreenshots.Rows.Clear();

            foreach (string li in lignes)
            {
                if (File.Exists(pathToSave + "COMMENTS\\SCREENSHOTS\\" + li.Replace("/","_").Replace("//","__")))
                {
                    FileInfo fichierr = new FileInfo(pathToSave + "COMMENTS\\SCREENSHOTS\\" + li.Replace("/", "_").Replace("//", "__"));
                    Image img = ResizeImage(fichierr.FullName, 400, 400, false);
                    dataGridViewCommentsscreenshots.Rows.Add(img, "", pathToSave + "COMMENTS\\SCREENSHOTS\\" + li.Replace("/", "_").Replace("//", "__"));
                    FilesForSaving.Add(fichierr.FullName);
                }

            }


            return dataGridViewCommentsscreenshots.Rows.Count;
        }

        public int FillFriends(IList<string> lignes)
        {
            dataGridViewFriends.Rows.Clear();

            foreach (string li in lignes)
            {

                string fich = li.Split(';')[1];
                string user = li.Split(';')[0];
                string url = li.Split(';')[2];

                if (File.Exists(pathToSave  + fich))
                {
                    FileInfo fichierr = new FileInfo(pathToSave + fich);
                    Image img = ResizeImage(fichierr.FullName, 80, 80, false);
                    dataGridViewFriends.Rows.Add(img, url, user, pathToSave + li);
                    FilesForSaving.Add(fichierr.FullName);
                }

            }

            try
            {
                FileInfo fichierr = new FileInfo(pathToSave + lignes[0].Split(';')[1]);
                pictureBox30.Image = ResizeImage(fichierr.FullName, 80, 80, true);
                ProfileImageForSaving = fichierr.FullName;
                profilSet = true;
            }
            catch
            {

            }
            

            return dataGridViewFriends.Rows.Count;
        }
        public int FillMessenger(IList<string> lignes)
        {
            dataGridViewContactMessenger.Rows.Clear();

            foreach (string li in lignes)
            {

                string fich = li.Split(';')[1];
                string user = li.Split(';')[0];
                string url = li.Split(';')[2];

                if (File.Exists(pathToSave + fich))
                {
                    FileInfo fichierr = new FileInfo(pathToSave + fich);
                    Image img = ResizeImage(fichierr.FullName, 80, 80, false);
                    dataGridViewContactMessenger.Rows.Add(img, url, user, pathToSave + li);
                    FilesForSaving.Add(fichierr.FullName);
                }

            }

            try
            {
                FileInfo fichierr = new FileInfo(pathToSave + lignes[0].Split(';')[1]);
                pictureBox30.Image = ResizeImage(fichierr.FullName, 80, 80, false);
                profilSet = true;
            }
            catch
            {

            }


            return dataGridViewFriends.Rows.Count;
        }

        public int FillLikesImages(IList<string> lignes)
        {

            foreach (DataGridViewRow row in dataGridViewLikesImages.Rows)
            {
                Image img = (Image)row.Cells[0].Value;
                img.Dispose();


            }
            GC.Collect();


            dataGridViewLikesImages.Rows.Clear();

            foreach (string li in lignes)
            {
                if (File.Exists(li))
                {
                   

                    try
                    {
                        FileInfo fichierr = new FileInfo(li);
                        Image img = resizeImage(Image.FromFile(fichierr.FullName), new Size(200, 200));
                        //Image img = Image.FromFile(fichierr.FullName);
                        dataGridViewLikesImages.Rows.Add(img, "", li);
                        FilesForSaving.Add(fichierr.FullName);
                    }
                    catch
                    {
                        MessageBox.Show("Veuillez réduire votre sélection");
                        return 0;
                    }

                }

            }


            return dataGridViewLikesImages.Rows.Count;
        }

        private static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.Low;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        public int FillPicturesComments(IList<string> lignes)
        {
            dataGridViewCommentsImages.Rows.Clear();

            foreach (string li in lignes)
            {
                if (File.Exists(li))
                {
                    FileInfo fichierr = new FileInfo(li);
                    Image img = ResizeImage(fichierr.FullName, 400, 400, false);
                    dataGridViewCommentsImages.Rows.Add(img, "", li);
                    FilesForSaving.Add(fichierr.FullName);
                }

            }


            return dataGridViewCommentsImages.Rows.Count;
        }

        public int FillComments(IList<string> lignes)
        {
            dataGridViewComments.Rows.Clear();

            foreach (string li in lignes)
            {
                string fich = li.Split(';')[0];
                string comm = li.Split(';')[1];



                if (File.Exists(pathToSave + fich))
                {
                    FileInfo fichierr = new FileInfo(pathToSave + fich);
                    Image img = ResizeImage(fichierr.FullName, 150, 150, false);
                    dataGridViewComments.Rows.Add(img, comm, pathToSave + fich);
                    FilesForSaving.Add(fichierr.FullName);
                }

            }


            return dataGridViewHomepage.Rows.Count;
        }

        public static Image ResizeImage(string file,
                                     int width,
                                     int height,
                                     bool onlyResizeIfWider)
        {
            try
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
            catch(Exception ex)
            {
                MessageBox.Show("Veuillez réduire votre sélection");
                return null;
            }

            
            
        }

        //private void dataGridViewHomepage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dataGridViewHomepage.Rows.Count == 0)
        //        return;

        //    Process.Start(dataGridViewHomepage.Rows[e.RowIndex].Cells[2].Value.ToString());
        //}

        private void dataGridViewTAGS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewTAGS.Rows.Count == 0)
                return;

            Process.Start(dataGridViewTAGS.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void dataGridViewLikesImages_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridViewLikesPages.Rows.Count == 0)
            //    return;

            //Process.Start(dataGridViewLikesPages.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void dataGridViewFriends_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewFriends.Rows.Count == 0)
                return;

            Process.Start(dataGridViewFriends.Rows[e.RowIndex].Cells[1].Value.ToString());
        }

        private void dataGridViewCommentsscreenshots_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCommentsscreenshots.Rows.Count == 0)
                return;

            Process.Start(dataGridViewCommentsscreenshots.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void dataGridViewResume_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridViewResume.Rows[e.RowIndex].Cells[2].Value.ToString() == "AMIS")
            {
                tabControl1.SelectedTab = tabControl1.TabPages["AMIS"];//tabControl1.Controls["AMIS"]
                return;
            }



            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (tab.Text == dataGridViewResume.Rows[e.RowIndex].Cells[2].Value.ToString())
                {
                    tabControl1.SelectedTab = tab;
                    return;
                }

                

            }
        }

        private void dataGridViewContactMessenger_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewContactMessenger.Rows.Count == 0)
                return;

            Process.Start(dataGridViewContactMessenger.Rows[e.RowIndex].Cells[1].Value.ToString());
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if(lignesForSavingCase.Count > 0)            
                SaveTargetCase(TargetForSaving);
        }

        private void SaveTargetCase(string target)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {

                if (!Directory.Exists(folderBrowserDialog1.SelectedPath + "\\" + "FacebookAnalyticsCases\\" + target.Replace(":", "").Replace("'", "").Replace("/", "").Replace("\\", "").Replace("\"", "").Replace("|", "").Replace("<", "").Replace(">", "").Replace("*", "").Replace("?", "").Replace("&nbsp;", "")))
                    Directory.CreateDirectory(folderBrowserDialog1.SelectedPath + "\\" + "FacebookAnalyticsCases\\" + target.Replace(":", "").Replace("'", "").Replace("/", "").Replace("\\", "").Replace("\"", "").Replace("|", "").Replace("<", "").Replace(">", "").Replace("*", "").Replace("?", "").Replace("&nbsp;", ""));
                else
                {

                    if (MessageBox.Show("Ce dossier existe déjà. Voulez-vous le supprimer ? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        EraseDirectory(folderBrowserDialog1.SelectedPath + "\\" + "FacebookAnalyticsCases\\" + target.Replace(":", "").Replace("'", "").Replace("/", "").Replace("\\", "").Replace("\"", "").Replace("|", "").Replace("<", "").Replace(">", "").Replace("*", "").Replace("?", "").Replace("&nbsp;", ""), true);
                    }


                }
            }

              

           CreateFBV(folderBrowserDialog1.SelectedPath + "\\" + "FacebookAnalyticsCases\\" + target.Replace(":", "").Replace("'", "").Replace("/", "").Replace("\\", "").Replace("\"", "").Replace("|", "").Replace("<", "").Replace(">", "").Replace("*", "").Replace("?", "").Replace("&nbsp;", ""));

            if (!Directory.Exists(folderBrowserDialog1.SelectedPath + "\\" + "FacebookAnalyticsCases\\" + target.Replace(":", "").Replace("'", "").Replace("/", "").Replace("\\", "").Replace("\"", "").Replace("|", "").Replace("<", "").Replace(">", "").Replace("*", "").Replace("?", "").Replace("&nbsp;", "") + "\\TXT"))
                Directory.CreateDirectory(folderBrowserDialog1.SelectedPath + "\\" + "FacebookAnalyticsCases\\" + target.Replace(":", "").Replace("'", "").Replace("/", "").Replace("\\", "").Replace("\"", "").Replace("|", "").Replace("<", "").Replace(">", "").Replace("*", "").Replace("?", "").Replace("&nbsp;", "") + "\\TXT");


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(folderBrowserDialog1.SelectedPath + "\\" + "FacebookAnalyticsCases\\" + target.Replace(":", "").Replace("'", "").Replace("/", "").Replace("\\", "").Replace("\"", "").Replace("|", "").Replace("<", "").Replace(">", "").Replace("*", "").Replace("?", "").Replace("&nbsp;", "") + "\\TXT\\ForAnalytics.txt", true))
            {
                string li = "";
                foreach (string ligne in lignesForSavingCase)
                {
                    li += ligne + "\n";
                }

                file.Write(li);

            }

            CopyData(folderBrowserDialog1.SelectedPath + "\\" + "FacebookAnalyticsCases\\" + target.Replace(":", "").Replace("'", "").Replace("/", "").Replace("\\", "").Replace("\"", "").Replace("|", "").Replace("<", "").Replace(">", "").Replace("*", "").Replace("?", "").Replace("&nbsp;", ""));
            MessageBox.Show("Votre ECASE a été sauvegardé.");

            //lignesForSavingCase = new List<string>();

        }

        public static bool EraseDirectory(string folderPath, bool recursive)
        {
            //Safety check for directory existence.
            if (!Directory.Exists(folderPath))
                return false;

            foreach (string file in Directory.GetFiles(folderPath))
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                    FileInfo filee = new FileInfo(file);

                    Process pro = new Process();
                    pro.StartInfo.UseShellExecute = false;
                    pro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pro.StartInfo.CreateNoWindow = true;
                    pro.StartInfo.RedirectStandardOutput = true;
                    pro.StartInfo.FileName = "cmd.exe";
                    pro.StartInfo.Arguments = "/C del /S \"" + filee.FullName + "\"";
                    pro.Start();
                    //Console.WriteLine(Process.StandardOutput.ReadToEnd());
                    pro.WaitForExit();
                    pro.Close();
                }


            }

            //Iterate to sub directory only if required.
            if (recursive)
            {
                foreach (string dir in Directory.GetDirectories(folderPath))
                {
                    EraseDirectory(dir, recursive);
                }
            }
            //Delete the parent directory before leaving

            try
            {
                Directory.Delete(folderPath);
                return true;
            }
            catch
            {
                return true;
            }

        }
        private void CopyData(string Destination)
        {
            if(FilesForSaving.Count > 0)
            {
                foreach (string fichierinRoot in FilesForSaving)
                {
                    FileInfo fich = new FileInfo(fichierinRoot);
                    DirectoryInfo dir = fich.Directory;

                    if (!Directory.Exists(Destination + "\\"  + dir.FullName.Substring(dir.FullName.IndexOf(pathToSave) + pathToSave.Length)))
                        Directory.CreateDirectory(Destination + "\\" + dir.FullName.Substring(dir.FullName.IndexOf(pathToSave) + pathToSave.Length));

                    try
                    {
                        fich.CopyTo(Destination + "\\" + dir.FullName.Substring(dir.FullName.IndexOf(pathToSave) + pathToSave.Length) + "\\" + fich.Name, true);
                    }
                    catch
                    {
                        Process pro = new Process();
                        pro.StartInfo.UseShellExecute = false;
                        pro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        pro.StartInfo.CreateNoWindow = true;
                        pro.StartInfo.RedirectStandardOutput = true;
                        pro.StartInfo.FileName = "cmd.exe";
                        pro.StartInfo.Arguments = "/C copy \"" + fich.FullName + "\" \"" + Destination + "\\" + dir.FullName.Substring(dir.FullName.IndexOf(pathToSave) + pathToSave.Length) + "\\" + fich.Name + "\"";
                        pro.Start();
                        //Console.WriteLine(Process.StandardOutput.ReadToEnd());
                        pro.WaitForExit();
                        pro.Close();
                    }

                }
            }

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "FacebookAnalytics.exe"))
            {
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "FacebookAnalytics.exe", Destination + "\\FacebookAnalytics.exe", true);
                
            }

            if (File.Exists(ProfileImageForSaving))
            {
                FileInfo fich = new FileInfo(ProfileImageForSaving);
                
                File.Copy(ProfileImageForSaving, Destination + "\\" + fich.Name, true);

            }

            //FilesForSaving = new List<string>();
            //lignesForSavingCase = new List<string>();
        }

        private void CreateFBV(string Destination)
        {
            string ligne = "<Case>\n";
            string profile = "";

            if (File.Exists(ProfileImageForSaving))
            {
                FileInfo fich = new FileInfo(ProfileImageForSaving);
                profile = fich.Name;


            }
            //Données dans les champs
            if (!Directory.Exists(Destination))
                Directory.CreateDirectory(Destination);




            ligne += UserNameForSaving + ";" + UserNameForSaving + ";" + UserNameForSaving + ";" + UserNameForSaving + ";" + UserNameForSaving + ";" + IDForSaving + "\n";
                ligne += "</Case>\n";
                ligne += "<PictureProfile>\n" + profile + "\n</PictureProfile>\n";

            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Destination + "\\CASE.fbv", false))
                {
                    file.Write(ligne);
                }

                
            }
            catch
            {

            }

        }

        public Button GetSaveButton()
        {
            button18.Visible = false;
            return button18;
        }

        private void dataGridViewCommentsImages_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCommentsImages.Rows.Count == 0)
                return;

            Process.Start(dataGridViewCommentsImages.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void dataGridViewLikesImages_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewLikesImages.Rows.Count == 0)
                return;

            Process.Start(dataGridViewLikesImages.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void dataGridViewHomepage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewHomepage.Rows.Count == 0)
                return;

            Process.Start(dataGridViewHomepage.Rows[e.RowIndex].Cells[2].Value.ToString());
        }
    }
}
