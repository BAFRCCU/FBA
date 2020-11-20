using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Viewer
{
    public partial class Comments : Form
    {
        
        public Comments()
        {
            InitializeComponent();
        }

        public void SetDatagridview(string fichier)
        {
            if (fichier.Contains("HomepageComments_With_Screenshots.txt"))
            {
                SetDataGridViewCommentsHomepage(fichier);
                return;
            }
                
            
            string[] lines = File.ReadAllLines(fichier);

            foreach (string ligne in lines)
            {
                string[] champ = ligne.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                //foreach (string lii in champ)
                //{
                //string[] champp = lii.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    dataGridView1.Rows.Add(champ[0], champ[1], champ[3], champ[4]);

                }
                catch(Exception ex)
                {
                    //return;
                }
            }

            //dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);
        }

        private void SetDataGridViewCommentsHomepage(string fichier)
        {
            string[] lines = File.ReadAllLines(fichier);

            foreach (string ligne in lines)
            {
                string[] champ = ligne.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                //foreach (string lii in champ)
                //{
                //string[] champp = lii.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    dataGridView1.Rows.Add(champ[1], champ[0], champ[2], champ[3].Substring(champ[3].IndexOf("\\HOMEPAGE")));

                }
                catch (Exception ex)
                {
                    //return;
                }
            }

            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                flowLayoutPanel2.Controls.Clear();

                foreach (DataGridViewRow ro in dataGridView1.Rows)
                {

                    ro.DefaultCellStyle.BackColor = Color.White;

                }

                foreach (DataGridViewRow ro in dataGridView1.Rows)
                {
                    if (dataGridView1.Rows[ro.Index].Cells[1].Value.ToString().ToLower().Contains(textBox1.Text.ToLower()))
                    {
                        ro.DefaultCellStyle.BackColor = Color.Red;

                        LinkLabel link = new LinkLabel();
                        //link.Text = numeroLigne;
                        link.Text = ro.Index.ToString();
                        link.AutoSize = true;
                        link.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        //link.Name = "linkLabel" + indexx;
                        link.Name = "linkLabel" + ro.Index;
                        link.Size = new System.Drawing.Size(18, 20);
                        link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
                        flowLayoutPanel2.Controls.Add(link);


                    }
                }

            }
            catch
            {
                return;
            }
            
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView1.FirstDisplayedScrollingRowIndex = Int32.Parse(((LinkLabel)sender).Text);            
            dataGridView1.Focus();
            dataGridView1.Rows[Int32.Parse(((LinkLabel)sender).Text)].DefaultCellStyle.BackColor = Color.Yellow;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Process.Start(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
        }
    }
}
