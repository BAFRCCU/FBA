using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium.Support.UI;
using System.Data.OleDb;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium.Chrome;
using System.IO;
using OpenQA.Selenium;
using System.Net;
using System.Diagnostics;
using System.Security.Policy;
using Bizness;

namespace FacebookAnalyzer
{
    public partial class Group : Form
    {

        public string pathToSave = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string pathToSaveBAK = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\FacebookAnalyzer\";
        public Bitmap MyImage;
        public bool profilSet = false;
        Dictionary<string, Analytics> Analytics = new Dictionary<string, Analytics>();

        public ChromeDriver driver;



        public Group()
        {
            InitializeComponent();
            label2.Text = pathToSaveBAK;
            textBoxops.Focus();
            textBoxops.Select();
            if (checkBoxHidepassword.Checked)
            {
                textBoxPASSWORD.PasswordChar = '*';
            }
            else
                textBoxPASSWORD.PasswordChar = '\0';

        }

        private void FindAllPicturesFromFacebookForNewLook(string url)
        {

            //IsANewThread();
            backgroundWorker1.ReportProgress(-90);

           
            string targetName = textBoxops.Text.ToUpper();
            System.Random rnd = new System.Random();
            int nbreImages = 0;
            string titrePage = "";
            string urlFriend = textBoxUSERNAMEFRIENDS.Text;
            string dossierLocal = "";
            string ID = "";

            if (driver == null)
            {
                InitializeDriver();



                // 2. Go to the "Google" homepage
                driver.Navigate().GoToUrl("https://facebook.com/login");
                Thread.Sleep(500);

                if (isElementPresent(driver, "//button[@class='_42ft _4jy0 _9fws _4jy3 _4jy1 selected _51sy']"))
                {
                    driver.FindElementByXPath("//button[@class='_42ft _4jy0 _9fws _4jy3 _4jy1 selected _51sy']").Click();
                    Thread.Sleep(2000);
                }

                // 3. Find the username textbox (by ID) on the homepage
                var userNameBox = driver.FindElementById("email");

                // 4. Enter the text (to search for) in the textbox
                userNameBox.SendKeys(textBoxUSERNAME.Text);

                // 3. Find the username textbox (by ID) on the homepage
                var userpasswordBox = driver.FindElementById("pass");

                // 4. Enter the text (to search for) in the textbox
                userpasswordBox.SendKeys(textBoxPASSWORD.Text);
                Thread.Sleep(5000);

                // 5. Find the search button (by Name) on the homepage
                driver.FindElementById("loginbutton").Click();
                Thread.Sleep(2500);


            }




            driver.Navigate().GoToUrl(urlFriend + "/members");
            titrePage = driver.Title;
            Thread.Sleep(5000);

           
           
            if(isElementPresent(driver, "//div[@class='oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz rq0escxv nhd2j8a9 nc684nl6 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of lzcic4wl oo9gr5id gpro0wi8 lrazzd5p']"))
            {
                try
                {
                    var lien = driver.FindElementByXPath("//div[@class='oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz rq0escxv nhd2j8a9 nc684nl6 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of lzcic4wl oo9gr5id gpro0wi8 lrazzd5p']");
                    lien.Click();

                    Thread.Sleep(5000);
                }
                catch
                {

                }
                
               
            }

            if (!profilSet)
                try
                {
                    string linkToProfile = "";

                    if (isElementPresent(driver, "//div[@class='gs1a9yip ow4ym5g4 auili1gw j83agx80 cbu4d94t buofh1pr g5gj957u i1fnvgqd oygrvhab cxmmr5t8 hcukyx3x kvgmc6g5 tgvbjcpo hpfvmrgz qt6c0cv9 rz4wbd8a a8nywdso jb3vyjys du4w35lb i09qtzwb rq0escxv n7fi1qx3 pmk7jnqg j9ispegn kr520xx4']"))
                    {
                        IWebElement ecran = driver.FindElementByXPath("//div[@class='gs1a9yip ow4ym5g4 auili1gw j83agx80 cbu4d94t buofh1pr g5gj957u i1fnvgqd oygrvhab cxmmr5t8 hcukyx3x kvgmc6g5 tgvbjcpo hpfvmrgz qt6c0cv9 rz4wbd8a a8nywdso jb3vyjys du4w35lb i09qtzwb rq0escxv n7fi1qx3 pmk7jnqg j9ispegn kr520xx4']");




                        //if (isElementPresent(driver, "//div[@class='q9uorilb l9j0dhe7 pzggbiyp du4w35lb']"))
                        //{
                        if(isElementPresent(driver, "//img[@class='i09qtzwb rq0escxv n7fi1qx3 pmk7jnqg j9ispegn kr520xx4 datstx6m k4urcfbm']"))
                        {
                            try
                            {
                                IWebElement tmp = driver.FindElementsByXPath("//img[@class='i09qtzwb rq0escxv n7fi1qx3 pmk7jnqg j9ispegn kr520xx4 datstx6m k4urcfbm']")[1];
                                linkToProfile = tmp.GetAttribute("src");
                            }
                            catch
                            {

                            }
                            
                        }
                            //string linkToProfile = ecran.FindElement(By.TagName("img")).GetAttribute("src");



                            try
                            {
                                using (var client = new WebClient())
                                {
                                    if (!Directory.Exists(pathToSave + "\\Profile"))
                                        Directory.CreateDirectory(pathToSave + "\\Profile");

                                    if (!File.Exists(pathToSave + "\\Profile\\Profile.jpg"))
                                    {
                                        client.DownloadFile(linkToProfile, pathToSave + "\\Profile\\profile.jpg");
                                        Thread.Sleep(5000);


                                        backgroundWorker1.ReportProgress(-5, pathToSave + "\\Profile\\profile.jpg");
                                        //backgroundWorker1.ReportProgress(-2, ID);

                                        Thread.Sleep(2000);
                                        profilSet = true;

                                    }
                                    else
                                    {


                                        backgroundWorker1.ReportProgress(-5, pathToSave + "\\Profile\\profile.jpg");
                                        //backgroundWorker1.ReportProgress(-2, ID);

                                        profilSet = true;
                                        return;
                                    }


                                }

                                try
                                {
                                    IWebElement nomProlile = ecran.FindElement(By.XPath("//span[@class='d2edcug0 hpfvmrgz qv66sw1b c1et5uql rrkovp55 a8c37x1j keod5gw0 nxhoafnm aigsh9s9 embtmqzv fe6kdd0r mau55g9w c8b282yb hrzyx87i m6dqt4wy h7mekvxk hnhda86s oo9gr5id hzawbc8m']"));
                                    backgroundWorker1.ReportProgress(-2, nomProlile.Text);
                                }

                                catch
                                {

                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("PROBLEME AVEC LE TELECHARGEMENT POUR LA PHOTO DE PROFIL" + Environment.NewLine + ex.Message);
                                return;
                            }
                        
                    }
                }
                catch
                {

                }

            if (!Directory.Exists(pathToSave + @"\GROUPS_SCREENSHOTS"))
                Directory.CreateDirectory(pathToSave + @"\GROUPS_SCREENSHOTS");

            if (!Directory.Exists(pathToSave + @"\GROUPS"))
                Directory.CreateDirectory(pathToSave + @"\GROUPS");

            if (!Directory.Exists(pathToSave + @"\GROUPS\PicturesProfiles"))
                Directory.CreateDirectory(pathToSave + @"\GROUPS\PicturesProfiles");


            if (!Directory.Exists(pathToSave + @"\TXT"))
                Directory.CreateDirectory(pathToSave + @"\TXT");

            //maintenant les photos 
            int nbreRow = 0;
            int i = 1;


            //deux url sont possibles /photos_of et /photos_all
            string photos = "";
            string photosBis = "";
            string chemin = "";
            string nomDossierCommentScreenshot = "";
            string commentairesTexte = "";
            int height = driver.Manage().Window.Size.Height;



            bool tout = false;
            int imgprec = 0;
            string pathToProfile = "";
            Dictionary<string, string> dicocomments = new Dictionary<string, string>();
               
                    //if (url.Contains("id="))
                    //{
                    //    photos = "&sk=photos";
                    //}
                    //else
                    //    photos = "/photos";

                    //driver.Navigate().GoToUrl(url + photos);
                    //pathToProfile = url + photos;
                    //titrePage = driver.Title;

                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0);"); //Scroll To Top

                    Object innerHeight = ((IJavaScriptExecutor)driver).ExecuteScript("return window.innerHeight;");
                    long innerHeightt = (long)innerHeight;
                    long scroll = (long)innerHeight;
                    long scrollHeight = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.scrollHeight;");

                    scrollHeight = scrollHeight + scroll;
                    int hauteur = 450;


                if (isElementPresent(driver, "//div[@class='obtkqiv7']"))
                try
                {
                    Object lastHeight = ((IJavaScriptExecutor)driver).ExecuteScript("return document.body.scrollHeight");
                    int ii = 1;
                    Dictionary<string, string> dico = new Dictionary<string, string>();

                    while (true)
                    {
                        //driver.execute_script('document.getElementById("viewport").scrollTop += 100')

                        ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                        //((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('ltEKP')[0].scrollIntoView(true)");

                        Thread.Sleep(2000);

                        Object newHeight = ((IJavaScriptExecutor)driver).ExecuteScript("return document.body.scrollHeight");


                        //((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0, " + hauteur + ");");
                        //((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                        //if ((scrollHeight - innerHeightt) < 200)
                        //{
                           ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                            Thread.Sleep(5000);

                            
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                            Thread.Sleep(5000);

                            
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                            Thread.Sleep(5000);
                        //}
                        //else
                        //    Thread.Sleep(500);

                        if (scrollHeight <= innerHeightt)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                            Thread.Sleep(2000);
                            scrollHeight = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.scrollHeight;");

                        }


                        if (newHeight.Equals(lastHeight))
                        {
                            break;
                        }
                        lastHeight = newHeight;
                    }

                    //while (scrollHeight >= innerHeightt)
                    //{
                    //    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                    //    Thread.Sleep(1000);

                    //    //Screenshot imageScreenshott = ((ITakesScreenshot)driver).GetScreenshot();
                    //    //imageScreenshott = ((ITakesScreenshot)driver).GetScreenshot();

                    //    ////Save the screenshot
                    //    //imageScreenshott.SaveAsFile(pathToSave + @"\GROUPS_SCREENSHOTS\" + "\\Screenshot_" + ii + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                    //    //Thread.Sleep(100);



                    //    //((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0, " + hauteur + ");");
                    //    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");                       
                    //    if ((scrollHeight - innerHeightt) < 200)
                    //    {
                    //        Thread.Sleep(5000);
                    //    }
                    //    else
                    //        Thread.Sleep(500);

                    //    ////on récupère tous les urls présentes

                    //    //IList<IWebElement> urls = driver.FindElements(By.XPath("//div[@class='ue3kfks5 pw54ja7n uo3d90p7 l82x9zwi a8c37x1j']"));


                    //    //foreach (IWebElement el in urls)
                    //    //{
                    //    //    var urll = el.FindElement(By.TagName("a"));
                    //    //    string urlll = urll.GetAttribute("href");
                    //    //    string username = urll.GetAttribute("aria-label");
                    //    //    string urlImage = el.FindElement(By.TagName("image")).GetAttribute("xlink:href");

                    //    //    using (var client = new WebClient())
                    //    //    {



                    //    //        client.DownloadFile(urlImage, pathToSave + @"\GROUPS\PicturesProfiles\" + username + "_" + DateTime.Now.Ticks + ".jpg");
                    //    //        Thread.Sleep(500);



                    //    //    }

                    //    //    //if (urlll.StartsWith("/"))
                    //    //    //    urlll = urlll.Substring(urlll.IndexOf("/") + 1);

                    //    //    //if (urlll.StartsWith("https://www.instagram.com/"))
                    //    //    //    urlll = urlll.Replace("https://www.instagram.com/", "");

                    //    //    if (!dico.ContainsKey(urlll))
                    //    //        dico.Add(urlll, urlll);
                    //    //}

                    //    scrollHeight = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.scrollHeight;");
                    //    Thread.Sleep(2000);


                    //    //if (scrollHeight <= innerHeightt)
                    //    //{
                    //    //    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0, " + hauteur + ");");
                    //    //    Thread.Sleep(2000);
                    //    //    scrollHeight = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.scrollHeight;");

                    //    //}

                    //    scrollHeight = scrollHeight + scroll;
                    //    innerHeightt = innerHeightt + hauteur;
                    //    ii++;
                    //}


                    //on récupère tous les urls présentes
                    try
                    {
                        IList<IWebElement> urls = driver.FindElements(By.XPath("//div[@class='ue3kfks5 pw54ja7n uo3d90p7 l82x9zwi a8c37x1j']"));
                        Thread.Sleep(10000);
                        backgroundWorker1.ReportProgress(-1, urls.Count);
                        Thread.Sleep(100);

                        string ligne = "";
                        ii = 1;
                        foreach (IWebElement el in urls)
                        {
                            var urll = el.FindElement(By.TagName("a"));
                            string urlll = urll.GetAttribute("href");
                            string username = urll.GetAttribute("aria-label");
                            string usernameForDownload = "";
                            string urlImage = el.FindElement(By.TagName("image")).GetAttribute("xlink:href");
                            string detail = el.Text.Replace("\r\n", " ").Replace("Ajouter", "");

                            try
                            {
                                using (var client = new WebClient())
                                {


                                    usernameForDownload = username + "_" + DateTime.Now.Ticks;
                                    client.DownloadFile(urlImage, pathToSave + @"\GROUPS\PicturesProfiles\" + usernameForDownload + ".jpg");
                                    Thread.Sleep(500);



                                }
                            }
                            catch
                            {

                            }

                            ligne += usernameForDownload + ";" + username + ";" + detail + ";" + urlll + "\n";
                            //if (urlll.StartsWith("/"))
                            //    urlll = urlll.Substring(urlll.IndexOf("/") + 1);

                            //if (urlll.StartsWith("https://www.instagram.com/"))
                            //    urlll = urlll.Replace("https://www.instagram.com/", "");

                            //if (!dico.ContainsKey(ligne))
                            //    dico.Add(ligne, ligne);


                            try
                            {
                                Analytics analy = new Analytics();
                                analy.Category = "GROUPS";

                                //analy.PathToPicture = userNameTag + "_" + idTag + "_" + numTag + i + ".png";
                                try
                                {
                                    analy.Username = username;
                                    analy.Url = urlll;
                                    analy.PathToPicture = @"\GROUPS\PicturesProfiles\" + usernameForDownload + ".jpg";
                                    //analy.PathToFolder = forGrid.PathToFolder;
                                }
                                catch
                                {

                                }


                                if (!Analytics.ContainsKey(analy.Category + ";" + analy.Username + ";" + analy.Url))
                                    Analytics.Add(analy.Category + ";" + analy.Username + ";" + analy.Url, analy);

                            }
                            catch
                            {

                            }

                            backgroundWorker1.ReportProgress(ii++);
                        }

                        try
                        {
                            //ligne = "";
                            //string urll = "";
                            //string user = "";
                            //string iddd = "";
                            //foreach (string valeur in dico.Values)
                            //{
                            //    user = valeur.Split(';')[0];
                            //    urll = valeur.Split(';')[1];
                            //    //iddd = dicocomments[valeur].Split(';')[2];

                            //    ligne += urll + ";" + user + ";" + valeur.Replace("&amp", "") + ";" + "\r\n";
                            //}

                            if (File.Exists(pathToSave + "\\TXT" + "\\groups.txt"))
                                File.Delete(pathToSave + "\\TXT" + "\\groups.txt");

                            File.WriteAllText(pathToSave + "\\TXT" + @"\groups.txt", ligne);
                            dico = new Dictionary<string, string>();
                            ligne = "";
                        }
                        catch (Exception exx)
                        {

                        }
                    }
                    catch
                    {
                        string codePagee = driver.PageSource;
                    }


                    try
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathToSave + "\\TXT\\ForAnalytics.txt", true))
                        {
                            string textes = "";
                            foreach (Analytics a in Analytics.Values)
                            {
                                textes += a.Category.Trim() + ";Key=" + a.Username + "@" + a.Url + "@;" + a.Username + ";" + a.Url + ";" + a.PathToPicture.Replace("\r\n", " ").Replace("\n", " ") + ";" + a.PathToFolder + "\n";
                            }

                            file.Write(textes);

                        }
                        Analytics = new Dictionary<string, Analytics>();

                    }
                    catch
                    {

                    }



                }
                    catch
                {
                        //e.printStackTrace();
                }




            backgroundWorker1.ReportProgress(-102);
            Thread.Sleep(2000);

            //backgroundWorker1.ReportProgress(-3);
            //Thread.Sleep(2000);
            backgroundWorker1.CancelAsync();
            return;


           
            //on écrit les resultats des ID commentaires dans un fichier
            try
            {
                string ligne = "";
                string urll = "";
                string user = "";
                string iddd = "";
                foreach (string valeur in dicocomments.Keys)
                {
                    user = dicocomments[valeur].Split(';')[1];
                    urll = dicocomments[valeur].Split(';')[0];
                    iddd = dicocomments[valeur].Split(';')[2];

                    ligne += urll + ";" + user + ";" + iddd + "\r\n";
                }

                if (File.Exists(pathToSave + "\\TXT" + "\\friendsFromComments.txt"))
                    File.Delete(pathToSave + "\\TXT" + "\\friendsFromComments.txt");

                //File.WriteAllText(dossierLocal + @"\friendsFromComments.txt", ligne);
                File.WriteAllText(pathToSave + "\\TXT" + "\\friendsFromComments.txt", ligne);

                dicocomments = new Dictionary<string, string>();
            }
            catch (Exception ex)
            {

            }

            //on écrit les commentaires
            if (commentairesTexte != "")
                try
                {
                    string[] lignes = commentairesTexte.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    string urll = "";
                    string user = "";
                    string comm = "";
                    string ligne = "";
                    string urlImage = "";
                    string album = "";

                    foreach (string valeur in lignes)
                    {
                        user = valeur.Split(';')[0];
                        comm = valeur.Split(';')[1];
                        urll = valeur.Split(';')[2];
                        urlImage = valeur.Split(';')[3];
                        album = valeur.Split(';')[4];

                        ligne += user + ";" + comm + ";" + urll + ";" + urlImage + ";" + album + "\r\n";
                    }

                    if (File.Exists(pathToSave + "\\TXT" + "\\commentsFromPictures.txt"))
                        File.Delete(pathToSave + "\\TXT" + "\\commentsFromPictures.txt");

                    File.WriteAllText(pathToSave + "\\TXT" + @"\commentsFromPictures.txt", ligne);

                }
                catch (Exception ex)
                {

                }

            ////photos de profil
            driver.Navigate().GoToUrl(pathToProfile);
            //var image = driver.FindElementByXPath("//a[@class='_1nv3 _11kg _1nv5 profilePicThumb']");

            ////clic sur image dans href
            //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", image);

            Thread.Sleep(5000);

            bool error = false;

            try
            {

                //Thread.Sleep(5000);


            }
            catch (Exception ex)
            {

                error = true;
                try
                {
                    string ligne = "";
                    string urll = "";
                    string user = "";
                    string iddd = "";
                    foreach (string valeur in dicocomments.Keys)
                    {
                        user = dicocomments[valeur].Split(';')[0];
                        urll = dicocomments[valeur].Split(';')[1];
                        //iddd = dicocomments[valeur].Split(';')[2];

                        ligne += urll + ";" + user + ";" + valeur.Replace("&amp", "") + ";" + "\r\n";
                    }

                    if (File.Exists(pathToSave + "\\TXT" + "\\friendsFromComments.txt"))
                        File.Delete(pathToSave + "\\TXT" + "\\friendsFromComments.txt");

                    File.WriteAllText(pathToSave + "\\TXT" + @"\friendsFromComments.txt", ligne);
                    dicocomments = new Dictionary<string, string>();
                }
                catch (Exception exx)
                {

                }

            }
            if (error)
            {
                try
                {
                   

                    try
                    {
                        string ligne = "";
                        string urll = "";
                        string user = "";
                        string iddd = "";
                        foreach (string valeur in dicocomments.Keys)
                        {
                            user = dicocomments[valeur].Split(';')[0];
                            urll = dicocomments[valeur].Split(';')[1];
                            iddd = dicocomments[valeur].Split(';')[2];

                            ligne += urll + ";" + user + ";" + valeur.Replace("&amp", "") + ";" + "\r\n";
                        }

                        if (File.Exists(pathToSave + "\\TXT" + "\\friendsFromComments.txt"))
                            File.Delete(pathToSave + "\\TXT" + "\\friendsFromComments.txt");

                        File.WriteAllText(pathToSave + "\\TXT" + @"\friendsFromComments.txt", ligne);
                        dicocomments = new Dictionary<string, string>();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                catch (Exception ex)
                {
                    //pictureBoxpictures.Image = global::FacebookAnalyzer.Properties.Resources.ko;
                }
                finally
                {
                    i = 0;
                    nbreImages = 0;
                    photos = "";
                    photosBis = "";
                    backgroundWorker1.ReportProgress(++nbreRow);
                }
            }

            i = 0;
            nbreImages = 0;
            photos = "";
            photosBis = "";
            //backgroundWorker1.ReportProgress(++nbreRow);
            error = false;



           
            
        }

        public Boolean isElementPresent(ChromeDriver driver, string path)
        {
            try
            {
                driver.FindElementByXPath(path);
                return true;
            }
            catch (OpenQA.Selenium.NoSuchElementException e)
            {
                return false;
            }
        }


        private void InitializeDriver()
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            // progressBarfriends.Visible = true;

            //var driver = new ChromeDriver(driverService, new ChromeOptions());

            //System.Diagnostics.Process.Start(filepath);
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--disable-notifications");


            driver = new ChromeDriver(driverService, chromeOptions);
            //driver.Manage().Window.Maximize();
        }

        private void button12_Click(object sender, EventArgs e)
        {

            if (textBoxops.Text == "" )
            {
                textBoxops.BackColor = Color.Red;
                MessageBox.Show("Veuillez remplir le champ OPS ou ARTICLE");
                return;
            }
            else
                textBoxops.BackColor = Color.White;


            if (!pathToSave.Contains(textBoxops.Text))
                pathToSave = pathToSaveBAK + textBoxops.Text;

            if (Directory.Exists(pathToSave))
            {
                if (MessageBox.Show("Ce dossier existe déjà. Voulez-vous le supprimer ? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    EraseDirectory(pathToSave, true);
                }

            }

            pictureBoxwaiting.Visible = true;
            IsANewThread("Market");

            //FindAllPicturesFromFacebookForNewLook(textBoxUSERNAMEFRIENDS.Text);
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
            while (!backgroundWorker1.CancellationPending)
            {
                FindAllPicturesFromFacebookForNewLook(textBoxUSERNAMEFRIENDS.Text);
            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == -1)
            {
                progressBar1.Visible = true;
                progressBar1.Maximum = Convert.ToInt32(e.UserState);
            }
             
            else
                if (e.ProgressPercentage > 0)
                try
                {
                    progressBar1.Value = e.ProgressPercentage;
                }
                catch
                {

                }


            //Images
            if (e.ProgressPercentage == -102)
            {

                progressBar1.Value = 0;
                progressBar1.Maximum = 0;
                progressBar1.Visible = false;



                //if (Directory.Exists(pathToSave + @"\MARKET\PICTURES\"))
                //    labelIMAGES.Text = "IMAGES : " + Directory.GetFiles(pathToSave + @"\PICTURES\", "*.jpg").Count();

                //if (Directory.Exists(pathToSave + @"\PICTURES_TAGGED\"))
                //    labelTags.Text = "TAGS : " + Directory.GetFiles(pathToSave + @"\PICTURES_TAGGED\", "*.png").Count();

                FillMarketArticles();
                    

               

                pictureBoxwaiting.Visible = false;

            }

            if (e.ProgressPercentage == -2)
            {
                string id = ((string)e.UserState);
                if (id != "")
                {
                    labelID.Text = id;
                    //int pos = (panel23.Width - labelID.Size.Width) / 2;
                    labelID.Visible = true;
                    //labelID.Location = new Point(pos, labelID.Location.Y);
                    labelID.Refresh();
                }

            }



            if (e.ProgressPercentage == -5)
            {
                string path = ((string)e.UserState);


                // Sets up an image object to be displayed.
                if (MyImage != null)
                {
                    MyImage.Dispose();
                }

                MyImage = new Bitmap(path);
                //pictureBox1.ClientSize = new Size(xSize, ySize);
                pictureBoxtango.Image = (Image)MyImage;
                pictureBoxtango.BringToFront();
                pictureBoxtango.Refresh();

                //labelpathPictureProfile.Text = path;
                Thread.Sleep(2000);
            }
        }

        private void FillMarketArticles()
        {
            if (Directory.Exists(pathToSave + "\\GROUPS") && File.Exists(pathToSave + "\\TXT\\groups.txt"))
            {
                dataGridViewArticles.Rows.Clear();
                foreach (string li in File.ReadAllLines(pathToSave + "\\TXT\\groups.txt"))
                {
                    string[] champ = li.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    if (champ[2].Contains("_"))
                        continue;

                    //foreach (string lii in champ)
                    //{
                    //string[] champp = lii.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        if (File.Exists(pathToSave + "\\GROUPS\\PicturesProfiles\\" + champ[0] + ".jpg"))
                        {
                            FileInfo fichierr = new FileInfo(pathToSave + "\\GROUPS\\PicturesProfiles\\" + champ[0] + ".jpg");
                            Image img = ResizeImage(fichierr.FullName, 65, 65, false);
                            
                           
                            dataGridViewArticles.Rows.Add(img, champ[1], champ[2], champ[3]);

                            
                        }
                        else
                        {
                            Image img = FacebookAnalyzer.Properties.Resources.anonymous;
                            
                            dataGridViewArticles.Rows.Add(img, champ[1], champ[2], champ[3]);                           

                            continue;
                        }
                    }
                    catch
                    {
                        Image img = FacebookAnalyzer.Properties.Resources.anonymous;
                        dataGridViewArticles.Rows.Add(img, champ[1], champ[2], champ[3]);

                        
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
                //if (e.ColumnIndex == 0)
                ////.Substring(0, screenshot.IndexOf("\\"))
                //{
                //    if (Directory.Exists(pathToSave + "\\PICTURES\\ALBUMS\\ALBUM_" + dataGridViewArticles.Rows[e.RowIndex].Cells[4].Value.ToString().Split('.')[0]))
                //    {
                //        //FileInfo fichier = new FileInfo(pathToSave + "\\PICTURES\\" + dataGridViewArticles.Rows[e.RowIndex].Cells[4].Value);

                //        Process.Start(pathToSave + "\\PICTURES\\ALBUMS\\ALBUM_" + dataGridViewArticles.Rows[e.RowIndex].Cells[4].Value.ToString().Split('.')[0]);
                //        return;
                //    }

                //}

                Process.Start(dataGridViewArticles.Rows[e.RowIndex].Cells[3].Value.ToString().Replace(textBoxUSERNAMEFRIENDS.Text + "/user", "https://www.facebook.com/"));
                //Process.Start(dataGridViewArticles.Rows[e.RowIndex].Cells[3].Value.ToString());
            }
            catch
            {
                return;
            }
            
        }

        private void checkBoxHidepassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHidepassword.Checked)
            {
                textBoxPASSWORD.PasswordChar = '*';
            }
            else
                textBoxPASSWORD.PasswordChar = '\0';
        }

        private void button18_Click(object sender, EventArgs e)
        {
            SaveCase();
        }

        private void SaveCase()
        {


            //if (dataGridViewMessenger.Rows.Count > 0 || dataGridView2.Rows.Count > 0)
            //{


            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {

                if (!Directory.Exists(folderBrowserDialog1.SelectedPath + "\\" + textBoxops.Text.ToUpper()))
                    Directory.CreateDirectory(folderBrowserDialog1.SelectedPath + "\\" + textBoxops.Text.ToUpper());
                else
                {

                    if (MessageBox.Show("Ce dossier existe déjà. Voulez-vous le supprimer ? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        EraseDirectory(folderBrowserDialog1.SelectedPath + "\\" + textBoxops.Text.ToUpper(), true);
                    }


                }


                CopyData(pathToSaveBAK, folderBrowserDialog1.SelectedPath, textBoxops.Text);

                string ligne = "<Case>\n";

                
                    //Données dans les champs

                    string username = textBoxUSERNAME.Text;
                    string password = textBoxPASSWORD.Text;
                    

                //Données dans les champs
                if (!checkBoxSavepassword.Checked)
                    {
                        username = "";
                        password = "";
                    }


                    ligne += textBoxUSERNAMEFRIENDS.Text + ";" + username + ";" + password + ";" + textBoxops.Text.ToUpper() + ";" + "..\\" + textBoxops.Text + ";" + labelID.Text +"\n";
                    ligne += "</Case>\n";

                

                

                try
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(folderBrowserDialog1.SelectedPath + "\\" + textBoxops.Text.ToUpper() + "\\" + textBoxops.Text.ToUpper() + "_CASE.fbv", false))
                    {
                        file.Write(ligne);
                    }

                    MessageBox.Show("Votre ECASE a été sauvegardé.");
                }
                catch
                {

                }

            }
        }

        private void CopyData(string path, string dest, string ops)
        {

            if (!Directory.Exists(pathToSaveBAK + ops))
            {
                MessageBox.Show("Aucune donnée à copier");
                return;
            }





            //if(MessageBox.Show("Etes-vous certain d'effacer " + "repertoire dest : " + dest + "\\" + ops) == DialogResult.OK)
            //{
            if (!Directory.Exists(dest + "\\" + ops))
                Directory.CreateDirectory(dest + "\\" + ops);
            //else
            //{

            //}
            //    EraseDirectory(dest + "\\" + ops, true);

            foreach (string dir in Directory.GetDirectories(pathToSaveBAK + ops))
            {
                DirectoryInfo dirr = new DirectoryInfo(dir);

                if (!Directory.Exists(dest + "\\" + ops + "\\" + dirr.Name))
                    Directory.CreateDirectory(dest + "\\" + ops + "\\" + dirr.Name);

                DirectoryCopy(dir, dest + "\\" + ops + "\\" + dirr.Name, true);


            }

            foreach (string fichierinRoot in Directory.GetFiles(pathToSaveBAK + ops))
            {
                FileInfo fich = new FileInfo(fichierinRoot);
                try
                {
                    fich.CopyTo(dest + "\\" + ops + "\\" + fich.Name, true);
                }
                catch
                {
                    Process pro = new Process();
                    pro.StartInfo.UseShellExecute = false;
                    pro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pro.StartInfo.CreateNoWindow = true;
                    pro.StartInfo.RedirectStandardOutput = true;
                    pro.StartInfo.FileName = "cmd.exe";
                    pro.StartInfo.Arguments = "/C copy \"" + fich.FullName + "\" \"" + dest + "\\" + ops + "\\" + fich.Name + "\"";
                    pro.Start();
                    //Console.WriteLine(Process.StandardOutput.ReadToEnd());
                    pro.WaitForExit();
                    pro.Close();
                }

            }

            //on copie le reader

            //if (File.Exists(pathConfig.Replace("\\FacebookAnalyzer\\FacebookAnalyzer\\", "\\FacebookAnalyzer\\Viewer\\") + "Viewer.exe"))
            //{
            //    File.Copy(pathConfig.Replace("\\FacebookAnalyzer\\FacebookAnalyzer\\", "\\FacebookAnalyzer\\Viewer\\") + "Viewer.exe", dest + "\\" + ops + "\\Viewer.exe",true);
            //    Directory.CreateDirectory(dest + "\\" + ops + "\\Resources");
            //    File.Copy(pathConfig.Replace("\\FacebookAnalyzer\\FacebookAnalyzer\\bin\\Debug\\", "\\FacebookAnalyzer\\Viewer\\") + "Resources\\PV_Template.docx", dest + "\\" + ops + "\\Resources\\PV_Template.docx", true);
            //}

            
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Viewers\\ViewerGroups.exe"))
                {
                    File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Viewers\\ViewerGroups.exe", dest + "\\" + ops + "\\ViewerGroups.exe", true);
                    //Directory.CreateDirectory(dest + "\\" + ops + "\\Resources");
                    //File.Copy(AppDomain.CurrentDomain.BaseDirectory + "Resources\\PV_Template.docx", dest + "\\" + ops + "\\Resources\\PV_Template.docx", true);
                    //File.Copy(AppDomain.CurrentDomain.BaseDirectory + "Resources\\FACEBOOK_VIEWER_MANUEL_UTILISATION.pdf", dest + "\\" + ops + "\\FACEBOOK_VIEWER_MANUEL_UTILISATION.pdf", true);//FACEBOOK VIEWER MANUEL UTILISATION.pdf
                }
           


        }

        private static void DirectoryCopy(string sourceDirName, string destDirName,
                                       bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);

                try
                {
                    file.CopyTo(temppath, true);
                }
                catch
                {
                    Process pro = new Process();
                    pro.StartInfo.UseShellExecute = false;
                    pro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pro.StartInfo.CreateNoWindow = true;
                    pro.StartInfo.RedirectStandardOutput = true;
                    pro.StartInfo.FileName = "cmd.exe";
                    pro.StartInfo.Arguments = "/C copy \"" + file.FullName + "\" \"" + temppath + "\"";
                    pro.Start();
                    //Console.WriteLine(Process.StandardOutput.ReadToEnd());
                    pro.WaitForExit();
                    pro.Close();

                }
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }


    }



}
