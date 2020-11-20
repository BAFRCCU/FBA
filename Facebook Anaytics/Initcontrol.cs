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
    public partial class Initcontrol : Form
    {
        string pathConfig = AppDomain.CurrentDomain.BaseDirectory;
        public Initcontrol()
        {
            InitializeComponent();

            //if (!File.Exists(pathConfig + "\\TXT\\ForAnalytics.txt"))
            //{
            //    Form2 formallcases = new Form2(this);
            //    formallcases.Show();

            //}
            //else
            //{
            //    Form1 formallcases = new Form1(this);
            //    formallcases.Show();
            //}
            


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

        
    }

    
}
