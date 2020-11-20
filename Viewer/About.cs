using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Viewer
{
    public partial class About : Form
    {
        string pathConfig = AppDomain.CurrentDomain.BaseDirectory;
        FileInfo[] fichiersJournal;
        public About()
        {
            InitializeComponent();
            FillDataGridView();
        }

        private void FillDataGridView()
        {
            if (!Directory.Exists(pathConfig + "\\ABOUT"))
                return;

            var sorted = Directory.GetFiles(pathConfig + "\\ABOUT", "*.png").Select(fn => new FileInfo(fn)).OrderBy(f => f.LastWriteTime);
            fichiersJournal = sorted.ToArray();           



            dataGridViewAbout.Rows.Clear();

            Rectangle rect = GetResolutionScreen();         
            int hauteurForm = 706;
                                 

            foreach (FileInfo fichier in fichiersJournal)
            {
                               
                Image tmp = Image.FromFile(fichier.FullName);
                int differentiel = tmp.Width - tmp.Height;
                Image imgg = (Image)(new Bitmap(Image.FromFile(fichier.FullName), new Size(hauteurForm - (120 - differentiel), hauteurForm - 120)));
                //Image img = CreateThumbnail(fichier.FullName, hauteurForm - 79, hauteurForm - 120);

                dataGridViewAbout.Rows.Add(imgg);
                
            }

           
        }

        private Rectangle GetResolutionScreen()
        {

            Rectangle resolution = Screen.PrimaryScreen.WorkingArea;

            //pour connaitre la taille de l'écran actuel
            Screen scrn = Screen.FromControl(this);

            return scrn.WorkingArea;


           
        }
    }
}
