using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.OleDb;
using System.Collections.ObjectModel;
using System.Threading;

using System.IO;

using System.Net;
using System.Diagnostics;
using System.Security.Policy;

namespace ViewerMarket
{
    public partial class Market : Form
    {

        //public string pathToSave = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string pathToSave = AppDomain.CurrentDomain.BaseDirectory;
        public Bitmap MyImage;
        
        



        public Market()
        {
            InitializeComponent();
            textBoxops.Focus();
            label2.Text = pathToSave;
            var sorted = Directory.GetFiles(pathToSave, "*.fbv").Select(fn => new FileInfo(fn)).OrderBy(f => f.CreationTime);
            FileInfo[] fichiers = sorted.ToArray();

            foreach (FileInfo fichier in fichiers)
            {

                Import(fichier.FullName);

            }

            FillMarketArticles();
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


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }
        public void Import(string fichier)
        {

            string sauvegarde = File.ReadAllText(fichier);

            string[] ecase = sauvegarde.Substring(sauvegarde.IndexOf("<Case>\n") + 7, (sauvegarde.IndexOf("</Case>\n") - (sauvegarde.IndexOf("<Case>\n") + 7))).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)[0].Split(';');
            //string[] pictureProfile = sauvegarde.Substring(sauvegarde.IndexOf("<PictureProfile>\n") + 16, (sauvegarde.IndexOf("</PictureProfile>\n") - (sauvegarde.IndexOf("<PictureProfile>\n") + 16))).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            

            textBoxUSERNAMEFRIENDS.Text = ecase[0];
            textBoxUSERNAME.Text = ecase[1];
            textBoxPASSWORD.Text = ecase[2];
            textBoxops.Text = ecase[3];
            textBoxArticle.Text = ecase[5];
            //labelID.Text = ecase[5];

            try
            {
                labelID.Text = ecase[6].Trim();
                if (labelID.Text != "")
                {
                    int pos = (panel23.Width - labelID.Size.Width) / 2;
                    labelID.Visible = true;
                    labelID.Location = new Point(pos, labelID.Location.Y);
                    labelID.Refresh();

                }
                else
                    labelID.Visible = false;
            }
            catch
            {
                labelID.Visible = false;

            }



            if (Directory.Exists(pathToSave + "\\Profile"))
            {
                if(Directory.GetFiles(pathToSave + "\\Profile","*.jpg").Count() > 0)
                
                try
                {
                    FileInfo fichierr = new FileInfo(Directory.GetFiles(pathToSave + "\\Profile", "*.jpg")[0]);
                    pictureBoxtango.Image = Image.FromFile(fichierr.FullName);
                    pictureBoxtango.BringToFront();
                }
                catch (Exception ex)
                {

                }

            }
            else
            {



            }


                      



           



        }


        private void FillMarketArticles()
        {
            if (Directory.Exists(pathToSave + "\\PICTURES") && File.Exists(pathToSave + "\\TXT\\articles.txt"))
            {
                dataGridViewArticles.Rows.Clear();
                foreach (string li in File.ReadAllLines(pathToSave + "\\TXT\\articles.txt"))
                {
                    string[] champ = li.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    if (champ[2].Contains("_"))
                        continue;

                    //foreach (string lii in champ)
                    //{
                    //string[] champp = lii.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        if (File.Exists(pathToSave + "\\THUMBPICTURES\\" + champ[2]))
                        {
                            FileInfo fichierr = new FileInfo(pathToSave + "\\THUMBPICTURES\\" + champ[2]);
                            Image img = ResizeImage(fichierr.FullName, 130, 130, false);
                            Button bouton = new Button();

                            dataGridViewArticles.Rows.Add(bouton,img, champ[0], champ[1], champ[2]);

                            if(Directory.Exists(pathToSave + "\\PICTURES\\ALBUMS\\ALBUM_" + champ[2].Split('.')[0]))
                            if (Directory.GetFiles(pathToSave + "\\PICTURES\\ALBUMS\\ALBUM_" + champ[2].Split('.')[0], champ[2].Split('.')[0] + "_*").Count() == 0)
                            {

                                DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                                dataGridViewArticles.Rows[dataGridViewArticles.Rows.Count - 1].Cells[0] = txtcell;

                            }
                        }
                        else
                        {
                            Image img = ViewerMarket.Properties.Resources.Business_Shop_icon;
                            Button bouton = new Button();
                            dataGridViewArticles.Rows.Add(bouton,img, champ[0], champ[1], champ[2]);

                            if (Directory.Exists(pathToSave + "\\PICTURES\\ALBUMS\\ALBUM_" + champ[2].Split('.')[0]))
                                if (Directory.GetFiles(pathToSave + "\\PICTURES\\ALBUMS\\ALBUM_" + champ[2].Split('.')[0], champ[2].Split('.')[0] + "_*").Count() == 0)
                                {

                                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                                    dataGridViewArticles.Rows[dataGridViewArticles.Rows.Count - 1].Cells[0] = txtcell;

                                }
                            continue;
                        }
                    }
                    catch
                    {
                        Image img = ViewerMarket.Properties.Resources.Business_Shop_icon;
                        Button bouton = new Button();
                        dataGridViewArticles.Rows.Add(bouton,img, champ[0], champ[1], champ[2]);

                        if (Directory.Exists(pathToSave + "\\PICTURES\\ALBUMS\\ALBUM_" + champ[2].Split('.')[0]))
                            if (Directory.GetFiles(pathToSave + "\\PICTURES\\ALBUMS\\ALBUM_" + champ[2].Split('.')[0], champ[2].Split('.')[0] + "_*").Count() == 0)
                            {

                                DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                                dataGridViewArticles.Rows[dataGridViewArticles.Rows.Count - 1].Cells[0] = txtcell;

                            }
                        //continue;
                    }

                    //}

                }
            }
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

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

        private void dataGridViewArticles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == 0)
                //.Substring(0, screenshot.IndexOf("\\"))
                {
                    if(Directory.Exists(pathToSave + "\\PICTURES\\ALBUMS\\ALBUM_" + dataGridViewArticles.Rows[e.RowIndex].Cells[4].Value.ToString().Split('.')[0]))
                    {
                        //FileInfo fichier = new FileInfo(pathToSave + "\\PICTURES\\" + dataGridViewArticles.Rows[e.RowIndex].Cells[4].Value);

                        Process.Start(pathToSave + "\\PICTURES\\ALBUMS\\ALBUM_" + dataGridViewArticles.Rows[e.RowIndex].Cells[4].Value.ToString().Split('.')[0]);
                        return;
                    }
                    
                }


                Process.Start(dataGridViewArticles.Rows[e.RowIndex].Cells[3].Value.ToString());
            }
            catch
            {
                return;
            }
            
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }
    }



}
