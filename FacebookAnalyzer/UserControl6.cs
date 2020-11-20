using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Reflection;

namespace FacebookAnalyzer
{
    public partial class UserControl6 : UserControl
    {
        public Configuration config;
        public UserControl6()
        {
            InitializeComponent();
            string pathConfig = AppDomain.CurrentDomain.BaseDirectory; 
            string path = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().ManifestModule.FullyQualifiedName);
                FillUser(config.AppSettings.Settings["OSINTusername"].Value, config.AppSettings.Settings["OSINTpassword"].Value);
            }
            catch
            {

            }
        }

        public TextBox GetBoxName
        {
            get { return textBoxName; }
        }

      

        public TextBox GetBoxLogin
        {
            get { return textBoxLogin; }
        }

       

        public Button GetButtonSave
        {
            get { return buttonSave; }
        }

        public PictureBox GetBoxSaveOk
        {
            get { return pictureBox1; }
        }

        public void FillUser(string username, string password)
        {
            textBoxName.Text = username;
            textBoxLogin.Text = password;
            
        }

        public void ClearAll()
        {
            textBoxName.Text = "";
            textBoxLogin.Text = "";
            
        }



        public void HideArowUp(bool hide)
        {
            panel34.Visible = hide;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ClearAll();
            Hide();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().ManifestModule.FullyQualifiedName);


                config.AppSettings.Settings["OSINTusername"].Value = textBoxName.Text;
                config.AppSettings.Settings["OSINTpassword"].Value = textBoxLogin.Text;
                config.Save();
                MessageBox.Show("Paramètres du profile OSINT sauvegardés");
            }
            catch (Exception ex)
            {
                MessageBox.Show("error " + ex.Message);
            }
        }
    }
}
