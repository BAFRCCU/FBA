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

namespace FacebookAnalyzer
{
    public partial class Batch : Form
    {
        DateTimePicker dtp = new DateTimePicker();
        Rectangle _Rectangle;
        DataGridView dtt;
        DataGridView res;
        Label batching;
        public Batch(DataGridView dt, Label batch, DataGridView r)
        {
            InitializeComponent();
            dataGridView1.Controls.Add(dtp);
            dtp.Visible = false;
            dtp.CustomFormat = "yyyy"; 
            dtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtp.ShowUpDown = true;
            dtp.TextChanged += new EventHandler(dtp_TextChange);
            textBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +@"\FacebookAnalyzer\";
            textBoxops.Select();

            dtt = dt;
            res = r;
            batching = batch;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow ro = dataGridView1.Rows[dataGridView1.Rows.Count - 1];

              if(ro.Cells[1].Value == null && ro.Cells[2].Value == null && ro.Cells[3].Value == null && ro.Cells[4].Value == null && ro.Cells[5].Value == null && ro.Cells[6].Value == null && ro.Cells[7].Value == null && ro.Cells[8].Value == null && ro.Cells[9].Value == null)
              {
                    MessageBox.Show("Veuillez effectuer une sélection");
                    return;
              }

              if (!ro.Cells[0].Value.ToString().Contains("https://www.facebook.com/"))
              {
                    MessageBox.Show("Veuillez introduire une url de type https://www.facebook.com/");
                    return;

              }

             if (ro.Cells[0].Value.ToString().EndsWith("/") && ro.Cells[0].Value.ToString().Contains("https://www.facebook.com/"))
              {
                    ro.Cells[0].Value = ro.Cells[0].Value.ToString().Substring(0, ro.Cells[0].Value.ToString().LastIndexOf("/"));
              }
            }
            
            dataGridView1.Rows.Add("https://www.facebook.com/");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow ro in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(ro);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (dataGridView1.Columns[e.ColumnIndex].Name)
            {
                case "Column6":

                    _Rectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true); //  
                    dtp.Size = new Size(_Rectangle.Width, _Rectangle.Height); //  
                    dtp.Location = new Point(_Rectangle.X, _Rectangle.Y); //  
                    dtp.Visible = true;

                    break;

            }
        }

        private void dtp_TextChange(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = dtp.Text.ToString();
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            dtp.Visible = false;

        }


        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            dtp.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBoxops.Text == "")
            {
                textBoxops.BackColor = Color.Red;
                MessageBox.Show("Veuillez remplir le champ OPS");
                return;
            }
            else
                textBoxops.BackColor = Color.White;

            if (MessageBox.Show("Etes-vous certain ? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            dtt.Rows.Clear();

            bool error = false;
            dataGridView1.ClearSelection();
            foreach (DataGridViewRow ro in dataGridView1.Rows)
            {
                if ( ro.Cells[1].Value == null && ro.Cells[2].Value == null && ro.Cells[3].Value == null && ro.Cells[4].Value == null && ro.Cells[5].Value == null && ro.Cells[6].Value == null && ro.Cells[7].Value == null && ro.Cells[8].Value == null && ro.Cells[9].Value == null)
                {
                    ro.DefaultCellStyle.BackColor = Color.Red;
                    error = true;
                }

                if (!ro.Cells[0].Value.ToString().Contains("https://www.facebook.com/"))
                {
                    ro.DefaultCellStyle.BackColor = Color.Red;
                    error = true;

                }

                if (ro.Cells[0].Value.ToString().EndsWith("/") && ro.Cells[0].Value.ToString().Contains("https://www.facebook.com/"))
                {
                    ro.Cells[0].Value = ro.Cells[0].Value.ToString().Substring(0, ro.Cells[0].Value.ToString().LastIndexOf("/"));
                }

            }

            if (error)
            {
                MessageBox.Show("Veuillez corriger les lignes en rouge");
                return;
            }
            else
            {
                foreach (DataGridViewRow ro in dataGridView1.Rows)
                {
                    ro.DefaultCellStyle.BackColor = Color.White;
                }
            }

            foreach (DataGridViewRow ro in dataGridView1.Rows)
            {
                dtt.Rows.Add(ro.Cells[0].Value.ToString(), ro.Cells[1].Value, ro.Cells[2].Value, ro.Cells[3].Value, ro.Cells[4].Value, ro.Cells[5].Value, ro.Cells[6].Value, ro.Cells[7].Value, ro.Cells[8].Value, ro.Cells[9].Value, ro.Cells[10].Value) ;
                dataGridView2.Rows.Add(ro.Cells[0].Value.ToString());
                res.Rows.Add(ro.Cells[0].Value.ToString());

                if (ro.Cells[1].Value != null)
                    res.Rows[res.Rows.Count - 1].Cells[1].Style.BackColor = Color.Crimson;

                if (ro.Cells[2].Value != null)
                    res.Rows[res.Rows.Count - 1].Cells[2].Style.BackColor = Color.Crimson;

                if (ro.Cells[3].Value != null)
                    res.Rows[res.Rows.Count - 1].Cells[3].Style.BackColor = Color.Crimson;

                if (ro.Cells[4].Value != null)
                    res.Rows[res.Rows.Count - 1].Cells[4].Style.BackColor = Color.Crimson;

                if (ro.Cells[6].Value != null)
                    res.Rows[res.Rows.Count - 1].Cells[5].Style.BackColor = Color.Crimson;

                if (ro.Cells[7].Value != null)
                    res.Rows[res.Rows.Count - 1].Cells[6].Style.BackColor = Color.Crimson;

                if (ro.Cells[8].Value != null)
                    res.Rows[res.Rows.Count - 1].Cells[7].Style.BackColor = Color.Crimson;

                if (ro.Cells[9].Value != null)
                    res.Rows[res.Rows.Count - 1].Cells[8].Style.BackColor = Color.Crimson;

                if (ro.Cells[10].Value != null)
                    res.Rows[res.Rows.Count - 1].Cells[9].Style.BackColor = Color.Crimson;



            }

            batching.Text = "Discreet="+checkBoxDiscreetMode.Checked.ToString()+";"+"Ecase="+textBoxops.Text+";" + "Directory=" + textBox1.Text + ";";
        }

        private void checkBoxDiscreetMode_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDiscreetMode.Checked)
            {
                panel2.BackColor = Color.Black;
                panel1.BackColor = Color.Black;
                panelops.BackColor = Color.Black;

            }
            else
            {
                panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(242)))));
                panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(242)))));
                panelops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(242)))));
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {

                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        public void SetResultInDataGridResult(string result, int index)
        {
            string[] paras = result.Split(';');
            res.ClearSelection();

            dataGridView2.Rows[index].Cells[1].Value = paras[0];
            dataGridView2.Rows[index].Cells[2].Value = paras[1];
            dataGridView2.Rows[index].Cells[3].Value = paras[2];
            dataGridView2.Rows[index].Cells[4].Value = paras[3];
            dataGridView2.Rows[index].Cells[5].Value = paras[4];
            dataGridView2.Rows[index].Cells[6].Value = paras[5];
            dataGridView2.Rows[index].Cells[7].Value = paras[6];
            dataGridView2.Rows[index].Cells[8].Value = paras[7];
            dataGridView2.Rows[index].Cells[9].Value = paras[8];

            res.Rows[index].Cells[1].Value = paras[0];
            res.Rows[index].Cells[2].Value = paras[1];
            res.Rows[index].Cells[3].Value = paras[2];
            res.Rows[index].Cells[4].Value = paras[3];
            res.Rows[index].Cells[5].Value = paras[4];
            res.Rows[index].Cells[6].Value = paras[5];
            res.Rows[index].Cells[7].Value = paras[6];
            res.Rows[index].Cells[8].Value = paras[7];
            res.Rows[index].Cells[9].Value = paras[8];

            foreach(DataGridViewCell cel in res.Rows[index].Cells)
            {
                cel.Style.BackColor = Color.LightGreen;
            }
            res.Rows[index].DefaultCellStyle.BackColor = Color.LightGreen;
            res.FirstDisplayedScrollingRowIndex = index;


        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
        }
    }
}
