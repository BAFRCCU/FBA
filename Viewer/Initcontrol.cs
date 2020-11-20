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

namespace Viewer
{
    public partial class Initcontrol : Form
    {
        string pathConfig = AppDomain.CurrentDomain.BaseDirectory;
        public Initcontrol()
        {
            InitializeComponent();

            var sorted = Directory.GetFiles(pathConfig, "*.fbv").Select(fn => new FileInfo(fn)).OrderBy(f => f.CreationTime);
            FileInfo[] fichiers = sorted.ToArray();
            FileInfo ff = fichiers[0];

            string sauvegarde = File.ReadAllText(ff.FullName);

            string[] ecase = sauvegarde.Substring(sauvegarde.IndexOf("<Case>\n") + 7, (sauvegarde.IndexOf("</Case>\n") - (sauvegarde.IndexOf("<Case>\n") + 7))).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)[0].Split(';');
            //string[] datagridviewmessenger = sauvegarde.Substring(sauvegarde.IndexOf("<DataGridViewMessenger>\n") + 24).Split(new string[] { "</DataGridViewMessenger>\n" }, StringSplitOptions.RemoveEmptyEntries);
           
            


            if (GetNumberInstagram() != "0" && !ecase[0].Contains("facebook"))
                pictureBoxLogo.Image = global::Viewer.Properties.Resources.viewer_logo2;

            progressBar1.Visible = true;
            progressBar1.Value = 1;
            progressBar1.Maximum = 6;
        }

        public ProgressBar GetProgressBar()
        {
            progressBar1.Refresh();
            //progressBar1.Update();
            Thread.Sleep(200);
            return progressBar1;
        }

        private void Initcontrol_Load(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                

                this.Invoke(new EventHandler(Initcontrol_Load), new object[] { sender, e });
                return;
            }

            
            
        }

        private void Initcontrol_Shown(object sender, EventArgs e)
        {
            // FillJournalView();
            //Thread.Sleep(5000);
            this.Refresh();
            this.SuspendLayout();
            Thread.Sleep(500);
        }

        private string GetNumberInstagram()
        {
            if (System.IO.Directory.Exists(pathConfig + "INSTAGRAM"))
            {

                int nbreDir = Directory.GetDirectories(pathConfig + "INSTAGRAM").Count() - 1;

                if (Directory.Exists(pathConfig + "INSTAGRAM\\ALBUM"))
                    nbreDir = nbreDir - 1;


                return nbreDir.ToString();
            }

            return "0";
        }
    }

    
}
