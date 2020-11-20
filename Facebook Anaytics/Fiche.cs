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

    
    
    public partial class Fiche : UserControl
    {
        public List<Casedata> _liste = new List<Casedata>();
        int lastY = 15;
        public Label click = new Label();

        public Fiche(Label lbl)
        {
            InitializeComponent();
            click = lbl;
            //_liste = liste;

            //AddTableLayout(_liste);
        }
        public void SetProfilePicture(Image im)
        {
            pictureBoxTarget.Image = im;
        }
        public void SetProfile(string username, string id, string url) 
        {
            labelUsername.Text = username;
            labelID.Text = id;
            labelURL.Text = url;
        }
        public TableLayoutPanel GetTablePanel()
        {
            return tableLayoutPanel1;
        }
        private void dataGridViewIdentifiers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelTarget_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void EndOf()
        {
            // 
            // panel1
            // 
            Panel horizontal = new Panel();
            horizontal.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);

            horizontal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(242)))));
            horizontal.Location = new System.Drawing.Point(104, lastY + 200);
            horizontal.Name = "panel" + DateTime.Now.Ticks.ToString();
            horizontal.Size = new System.Drawing.Size(1200, 200);
            horizontal.TabIndex = 8;

            this.Controls.Add(horizontal);
        }
        public void AddTableLayout(List<Casedata> liste, List<Image> images, string clee)
        {
            FlowLayoutPanel floww = new FlowLayoutPanel();
           
            

            tableLayoutPanel1.SuspendLayout();
            floww.SuspendLayout();



            //les elements qui composent la tablayout

            System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();


            // 
            // dataGridViewTextBoxColumn6
            // 

            dataGridViewTextBoxColumn6.HeaderText = "PathToFile";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn" + DateTime.Now.Ticks.ToString();
            dataGridViewTextBoxColumn6.ReadOnly = true;
            dataGridViewTextBoxColumn6.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Category";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn" + DateTime.Now.Ticks.ToString();
            dataGridViewTextBoxColumn5.ReadOnly = true;
            dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "From Case";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn" + DateTime.Now.Ticks.ToString();
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.Visible = false;

            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "From Case";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn" + DateTime.Now.Ticks.ToString();
            dataGridViewTextBoxColumn4.ReadOnly = true;
            dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "URL";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn" + DateTime.Now.Ticks.ToString();
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Username";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn" + DateTime.Now.Ticks.ToString();
            dataGridViewTextBoxColumn1.ReadOnly = true;


            // 
            // dataGridViewIdentifiers
            // 
            DataGridView dataGridVieww = new DataGridView();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();

            dataGridVieww.AllowUserToAddRows = false;
            dataGridVieww.AllowUserToDeleteRows = false;
            dataGridVieww.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridVieww.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridVieww.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))));
            dataGridVieww.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridVieww.BackgroundColor = System.Drawing.Color.White;
            dataGridVieww.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridVieww.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            dataGridViewTextBoxColumn1,
            dataGridViewTextBoxColumn2,
            dataGridViewTextBoxColumn3,
            dataGridViewTextBoxColumn4,
            dataGridViewTextBoxColumn5,
            dataGridViewTextBoxColumn6});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridVieww.DefaultCellStyle = dataGridViewCellStyle4;
            dataGridVieww.GridColor = System.Drawing.Color.White;
            dataGridVieww.Location = new System.Drawing.Point(279, 3);
            dataGridVieww.Name = "dataGridView" + DateTime.Now.Ticks.ToString();
            dataGridVieww.ReadOnly = true;
            dataGridVieww.RowHeadersVisible = false;
            dataGridVieww.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridVieww.Size = new System.Drawing.Size(1034, 300);
            dataGridVieww.TabIndex = 2;
            dataGridVieww.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewIdentifiers_CellContentClick);
            dataGridVieww.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewIdentifiers_CellDoubleClick);

            // 
            // flowLayoutPanel1
            // 
            floww = new FlowLayoutPanel();
            floww.AutoSize = false;
            floww.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            //floww.BackColor = Color.Red;
            //floww.Controls.Add(imm);
            ////floww.Controls.Add(im);            
            floww.Dock = System.Windows.Forms.DockStyle.Fill;
            floww.Location = new System.Drawing.Point(3, 3);
            floww.Name = "flowLayoutPanel" + DateTime.Now.Ticks.ToString();
            floww.Size = new System.Drawing.Size(152, 152);
            floww.TabIndex = 3;
            //floww.Paint += new System.Windows.Forms.PaintEventHandler(floww_Paint);


            // 
            // flowLayoutPanel2
            // 
            FlowLayoutPanel floww2 = new FlowLayoutPanel();
            floww2.AutoSize = false;
            floww2.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            floww2.Dock = System.Windows.Forms.DockStyle.Fill;
            floww2.Location = new System.Drawing.Point(79, 3);
            floww2.Name = "flowLayoutPanel" + DateTime.Now.Ticks.ToString();
            floww2.Size = new System.Drawing.Size(400, 250);
            //floww2.BackColor = Color.Red;

            floww2.TabIndex = 9;

            // 
            // tableLayoutPanel1
            // 
            TableLayoutPanel tableLayout = new TableLayoutPanel();

            

            tableLayout.AutoSize = false;
            tableLayout.BackColor = System.Drawing.Color.White;
            tableLayout.ColumnCount = 3;
            tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 740F));
            //tableLayout.Controls.Add(floww2, 1, 0);
            //tableLayout.Controls.Add(floww, 0, 0);
            //tableLayout.Controls.Add(dataGridVieww, 2, 0);

            tableLayout.Location = new System.Drawing.Point(104, 100 + lastY);
            tableLayout.Name = "tableLayoutPanel" + DateTime.Now.Ticks.ToString();
            tableLayout.RowCount = 1;
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));

            tableLayout.TabIndex = 7;


            int j = 0;
            string[] profils = clee.Split(';');
            foreach (Image im in images)
            {
                PictureBox imm = new PictureBox();
                imm.Image = im;
                imm.Location = new System.Drawing.Point(3, 3);
                imm.Name = "Picture" + DateTime.Now.Ticks.ToString();
                imm.Size = new System.Drawing.Size(152, 152);
                imm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                imm.TabIndex = 3;
                imm.TabStop = false;

                floww.Controls.Add(imm);

                Panel pan = new Panel();
                pan.Size = new Size(245, 152);
                pan.BackColor = Color.White;
                Label l = new Label();
                l.AutoSize = true;
                l.Text = profils[j].Replace(" ID : ","\n\nID :").Replace(" -","");
                l.Location = new Point(0, 3);

                pan.Controls.Add(l);

                floww2.Controls.Add(pan);

                j++;

            }

           
                
            

            
            foreach (Casedata casee in liste)
            {

                dataGridVieww.Rows.Add(casee.Username, casee.Url, casee.PathToFolder);               

                // 
                // Picture
                // 
                
                //PictureBox imm = new PictureBox();
                //imm.Image = casee.ImageProfile;
                //imm.Location = new System.Drawing.Point(3, 3);
                //imm.Name = "Picture" + DateTime.Now.Ticks.ToString();
                //imm.Size = new System.Drawing.Size(120, 120);
                //imm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                //imm.TabIndex = 3;
                //imm.TabStop = false;

                //Panel pan = new Panel();
                //pan.Size = new Size(400, 120);
                //pan.BackColor = Color.Silver;



            }

            floww.Size = new System.Drawing.Size(70, (152 * images.Count) + 150);
            floww2.Size = new System.Drawing.Size(70, (152 * images.Count) + 150);

            //floww.Height = (72 * images.Count) + 150;
            tableLayout.Size = new System.Drawing.Size(930, ( ((120 * images.Count) < 300) ? dataGridVieww.Height : 120 * images.Count));


            tableLayout.Controls.Add(floww2, 1, 0);
            tableLayout.Controls.Add(floww, 0, 0);
            tableLayout.Controls.Add(dataGridVieww, 2, 0);

            //fin de tableau rajout de ligne se separation

            // 
            // panel2
            // 
            Panel horizontal1 = new Panel();
            horizontal1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);

            horizontal1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(242)))));
            horizontal1.Location = new System.Drawing.Point(104, tableLayout.Location.Y + 20 + tableLayout.Size.Height - 20);
            horizontal1.Name = "panel" + DateTime.Now.Ticks.ToString();
            horizontal1.Size = new System.Drawing.Size(930, 10);
            horizontal1.TabIndex = 8;



            Panel vertical = new Panel();
            vertical.Anchor = System.Windows.Forms.AnchorStyles.Top;
            vertical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(242)))));
            vertical.Location = new System.Drawing.Point(276, tableLayout.Location.Y + 7 + tableLayout.Size.Height - 20);
            vertical.Name = "panel" + DateTime.Now.Ticks.ToString();
            vertical.Size = new System.Drawing.Size(5, 40);

            // 
            // panel1
            // 
            Panel horizontal = new Panel();
            horizontal.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);

            horizontal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(242)))));
            horizontal.Location = new System.Drawing.Point(104, vertical.Location.Y + vertical.Height);
            horizontal.Name = "panel" + DateTime.Now.Ticks.ToString();
            horizontal.Size = new System.Drawing.Size(930, 5);
            horizontal.TabIndex = 8;

            lastY = (vertical.Location.Y + vertical.Height) - 100;

            this.Controls.Add(tableLayout);
            this.Controls.Add(vertical);
            this.Controls.Add(horizontal);
            this.Controls.Add(horizontal1);

            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            floww.ResumeLayout();
            this.ResumeLayout(false);
            this.PerformLayout();


        }

        private void dataGridViewIdentifiers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            click.Text = ((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString() + ";" + ((DataGridView)sender).Rows[e.RowIndex].Cells[1].Value.ToString() + ";" + ((DataGridView)sender).Rows[e.RowIndex].Cells[2].Value.ToString();
            click.Refresh();
            
        }

        private void Fiche_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
