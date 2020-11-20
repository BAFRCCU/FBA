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

namespace FacebookAnalyzer
{
    public partial class Market : Form
    {

        public string pathToSave = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string pathToSaveBAK = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MARKET\";
        public Bitmap MyImage;
        public bool profilSet = false;

        public ChromeDriver driver;



        public Market()
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




            driver.Navigate().GoToUrl(urlFriend);
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

            if(!profilSet)
            try
            {

                if (isElementPresent(driver, "//div[@class='j83agx80 cbu4d94t lzcic4wl ni8dbmo4 stjgntxs oqq733wu l9j0dhe7 du4w35lb cwj9ozl2 ue3kfks5 pw54ja7n uo3d90p7 l82x9zwi nwpbqux9 gc7gaz0o k4urcfbm']"))
                {
                    IWebElement ecran = driver.FindElementByXPath("//div[@class='j83agx80 cbu4d94t lzcic4wl ni8dbmo4 stjgntxs oqq733wu l9j0dhe7 du4w35lb cwj9ozl2 ue3kfks5 pw54ja7n uo3d90p7 l82x9zwi nwpbqux9 gc7gaz0o k4urcfbm']");




                    //if (isElementPresent(driver, "//div[@class='q9uorilb l9j0dhe7 pzggbiyp du4w35lb']"))
                    //{
                    IWebElement profileImage = ecran.FindElement(By.XPath(".//div[@class='q9uorilb l9j0dhe7 pzggbiyp du4w35lb']"));
                    if (profileImage != null)
                    {
                        string linkToProfile = profileImage.FindElement(By.TagName("image")).GetAttribute("xlink:href");



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
                                    IWebElement nomProlile = ecran.FindElement(By.XPath(".//span[@class='d2edcug0 hpfvmrgz qv66sw1b c1et5uql rrkovp55 a8c37x1j keod5gw0 nxhoafnm aigsh9s9 l1jc4y16 fe6kdd0r mau55g9w c8b282yb rwim8176 mhxlubs3 p5u9llcw hnhda86s oo9gr5id']"));
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
            }
            catch
            {

            }

            if (!Directory.Exists(pathToSave + @"\PICTURES_SCREENSHOTS"))
                Directory.CreateDirectory(pathToSave + @"\PICTURES_SCREENSHOTS");

            if (!Directory.Exists(pathToSave + @"\PICTURES"))
                Directory.CreateDirectory(pathToSave + @"\PICTURES");

            if (!Directory.Exists(pathToSave + @"\THUMBPICTURES"))
                Directory.CreateDirectory(pathToSave + @"\THUMBPICTURES");

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


                if (isElementPresent(driver, "//div[@class='oajrlxb2 tdjehn4e gcieejh5 bn081pho humdl8nn izx4hr6d rq0escxv nhd2j8a9 j83agx80 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys d1544ag0 qt6c0cv9 tw6a2znq i1ao9s8h esuyzwwr f1sip0of lzcic4wl l9j0dhe7 abiwlrkh p8dawk7l beltcj47 p86d2i9g aot14ch1 kzx2olss cbu4d94t taijpn5t ni8dbmo4 stjgntxs k4urcfbm qypqp5cg']"))
                try
                {
                        Object lastHeight = ((IJavaScriptExecutor)driver).ExecuteScript("return document.body.scrollHeight");
                        int ii = 1;

                        while (scrollHeight >= innerHeightt && isElementPresent(driver, "//div[@class='oajrlxb2 tdjehn4e gcieejh5 bn081pho humdl8nn izx4hr6d rq0escxv nhd2j8a9 j83agx80 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys d1544ag0 qt6c0cv9 tw6a2znq i1ao9s8h esuyzwwr f1sip0of lzcic4wl l9j0dhe7 abiwlrkh p8dawk7l beltcj47 p86d2i9g aot14ch1 kzx2olss cbu4d94t taijpn5t ni8dbmo4 stjgntxs k4urcfbm qypqp5cg']"))
                        {
                            //((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                            //Thread.Sleep(1000);

                            Screenshot imageScreenshott = ((ITakesScreenshot)driver).GetScreenshot();
                            imageScreenshott = ((ITakesScreenshot)driver).GetScreenshot();

                            //Save the screenshot
                            imageScreenshott.SaveAsFile(pathToSave + @"\PICTURES_SCREENSHOTS\" + "Screenshot_" + ii + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                            Thread.Sleep(100);

                            if (isElementPresent(driver,"//div[@class='oajrlxb2 tdjehn4e gcieejh5 bn081pho humdl8nn izx4hr6d rq0escxv nhd2j8a9 j83agx80 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys d1544ag0 qt6c0cv9 tw6a2znq i1ao9s8h esuyzwwr f1sip0of lzcic4wl l9j0dhe7 abiwlrkh p8dawk7l beltcj47 p86d2i9g aot14ch1 kzx2olss cbu4d94t taijpn5t ni8dbmo4 stjgntxs k4urcfbm qypqp5cg']"))
                            {
                                var bouttonencore = driver.FindElementByXPath("//div[@class='oajrlxb2 tdjehn4e gcieejh5 bn081pho humdl8nn izx4hr6d rq0escxv nhd2j8a9 j83agx80 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys d1544ag0 qt6c0cv9 tw6a2znq i1ao9s8h esuyzwwr f1sip0of lzcic4wl l9j0dhe7 abiwlrkh p8dawk7l beltcj47 p86d2i9g aot14ch1 kzx2olss cbu4d94t taijpn5t ni8dbmo4 stjgntxs k4urcfbm qypqp5cg']");
                                bouttonencore.Click();
                                Thread.Sleep(2500);
                            }
                                      



                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0, " + hauteur + ");");
                            if ((scrollHeight - innerHeightt) < 200)
                            {
                                Thread.Sleep(5000);
                            }
                            else
                                Thread.Sleep(2500);


                            scrollHeight = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.scrollHeight;");
                            Thread.Sleep(2000);


                            if (scrollHeight <= innerHeightt)
                            {
                                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0, " + hauteur + ");");
                                Thread.Sleep(2000);
                                scrollHeight = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.scrollHeight;");

                            }

                            scrollHeight = scrollHeight + scroll;
                            innerHeightt = innerHeightt + hauteur;
                            ii++;
                        }
                }
                    catch
                {
                        //e.printStackTrace();
                }
            else
            {
                try
                {
                    Object lastHeight = ((IJavaScriptExecutor)driver).ExecuteScript("return document.body.scrollHeight");
                    int ii = 1;

                    while (scrollHeight >= innerHeightt)
                    {
                        //((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                        //Thread.Sleep(1000);

                        Screenshot imageScreenshott = ((ITakesScreenshot)driver).GetScreenshot();
                        imageScreenshott = ((ITakesScreenshot)driver).GetScreenshot();

                        //Save the screenshot
                        imageScreenshott.SaveAsFile(pathToSave + @"\PICTURES_SCREENSHOTS\" + "Screenshot_" + ii + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                        Thread.Sleep(100);

                        if (isElementPresent(driver, "//div[@class='oajrlxb2 tdjehn4e gcieejh5 bn081pho humdl8nn izx4hr6d rq0escxv nhd2j8a9 j83agx80 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys d1544ag0 qt6c0cv9 tw6a2znq i1ao9s8h esuyzwwr f1sip0of lzcic4wl l9j0dhe7 abiwlrkh p8dawk7l beltcj47 p86d2i9g aot14ch1 kzx2olss cbu4d94t taijpn5t ni8dbmo4 stjgntxs k4urcfbm qypqp5cg']"))
                        {
                            var bouttonencore = driver.FindElementByXPath("//div[@class='oajrlxb2 tdjehn4e gcieejh5 bn081pho humdl8nn izx4hr6d rq0escxv nhd2j8a9 j83agx80 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys d1544ag0 qt6c0cv9 tw6a2znq i1ao9s8h esuyzwwr f1sip0of lzcic4wl l9j0dhe7 abiwlrkh p8dawk7l beltcj47 p86d2i9g aot14ch1 kzx2olss cbu4d94t taijpn5t ni8dbmo4 stjgntxs k4urcfbm qypqp5cg']");
                            bouttonencore.Click();
                            Thread.Sleep(2500);
                        }




                        ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0, " + hauteur + ");");
                        if ((scrollHeight - innerHeightt) < 200)
                        {
                            Thread.Sleep(5000);
                        }
                        else
                            Thread.Sleep(2500);


                        scrollHeight = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.scrollHeight;");
                        Thread.Sleep(2000);


                        if (scrollHeight <= innerHeightt)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0, " + hauteur + ");");
                            Thread.Sleep(2000);
                            scrollHeight = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.scrollHeight;");

                        }

                        scrollHeight = scrollHeight + scroll;
                        innerHeightt = innerHeightt + hauteur;
                        ii++;
                    }
                }
                catch
                {
                    //e.printStackTrace();
                }
            }

            try
            {

                if (isElementPresent(driver, "//div[@class='j83agx80 cbu4d94t lzcic4wl ni8dbmo4 stjgntxs oqq733wu l9j0dhe7 du4w35lb cwj9ozl2 ue3kfks5 pw54ja7n uo3d90p7 l82x9zwi nwpbqux9 gc7gaz0o k4urcfbm']"))
                {
                    IWebElement ecran = driver.FindElementByXPath("//div[@class='j83agx80 cbu4d94t lzcic4wl ni8dbmo4 stjgntxs oqq733wu l9j0dhe7 du4w35lb cwj9ozl2 ue3kfks5 pw54ja7n uo3d90p7 l82x9zwi nwpbqux9 gc7gaz0o k4urcfbm']");

                    IList<IWebElement> nbreImg = ecran.FindElements(By.XPath(".//div[@class='b3onmgus ph5uu5jm g5gj957u buofh1pr cbu4d94t rj1gh0hx j83agx80 rq0escxv fnqts5cd fo9g3nie n1dktuyu e5nlhep0 ecm0bbzt']"));

                    string urlMarket = "";
                    string urlImageMArket = "";
                    string intitule = "";
                    string prix = "";
                    string ligne = "";
                    int numeroLigne = 1;

                    IList<string> liensFullPicture = new List<string>();
                    foreach (IWebElement el in nbreImg)
                    {
                        if (el.Text == "")
                            continue;
                        
                        urlMarket = el.FindElement(By.TagName("a")).GetAttribute("href");
                        urlImageMArket = el.FindElement(By.TagName("img")).GetAttribute("src");
                        intitule = el.FindElement(By.TagName("img")).GetAttribute("alt");

                        if(intitule == "")
                        {
                            try
                            {
                                intitule = el.FindElement(By.XPath(".//span[@class='a8c37x1j ni8dbmo4 stjgntxs l9j0dhe7']")).Text;
                            }
                            catch
                            {

                            }
                        }
                        prix = el.FindElement(By.TagName("span")).Text;
                        liensFullPicture.Add(urlMarket);

                        if (urlImageMArket.Contains("https://scontent"))
                        {
                            using (var client = new WebClient())
                            {


                                chemin = "Picture" + i + ".jpg";
                                //nomDossierCommentScreenshot = "Picture" + i;
                                //string sc = "screenshot_" + i + ".jpg";

                                client.DownloadFile(urlImageMArket, pathToSave + @"\THUMBPICTURES\" + chemin);
                                Thread.Sleep(500);
                            }

                            ligne += prix.Replace("\r", "").Replace("\n", "") + " " + intitule.Replace(";", "").Replace("\r", "").Replace("\n", "") + ";" + urlMarket + ";" + chemin + ";" + urlImageMArket + "\n";
                        }
                        else
                        {
                            chemin = "aucune";
                            ligne += prix.Replace("\r","").Replace("\n","") + " " + intitule.Replace(";", "").Replace("\r", "").Replace("\n", "") + ";" + urlMarket + ";" + chemin + ";" + "aucune" + "\n";
                        }
                            

                        i++;
                    }

                    i = 1;
                    backgroundWorker1.ReportProgress(-1,liensFullPicture.Count);
                    Thread.Sleep(100);

                   
                    foreach (string urll in liensFullPicture)
                    {

                        string urlForD = urll;
                        int nbsc = 1;

                        if(urll.Contains("https://www.facebook.com/marketplace/item/"))
                        try
                        {
                            //Thread.Sleep(rnd.Next(7500, 9500));
                            driver.Navigate().GoToUrl(urlForD);
                            Thread.Sleep(rnd.Next(2500, 4500));

                                Screenshot imageScreenshott = ((ITakesScreenshot)driver).GetScreenshot();
                                imageScreenshott = ((ITakesScreenshot)driver).GetScreenshot();

                                //Save the screenshot
                                imageScreenshott.SaveAsFile(pathToSave + @"\PICTURES_SCREENSHOTS\" + "PictureScreenshot_" + i + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                                Thread.Sleep(100);

                                var imageFinale = driver.FindElementByXPath("//div[@class='du4w35lb k4urcfbm stjgntxs ni8dbmo4 taijpn5t buofh1pr j83agx80 bp9cbjyn']");

                            string link = imageFinale.FindElement(By.TagName("img")).GetAttribute("src");



                            string nomImage = "";
                            string nomAlbum = "";
                            bool morePeople = false;
                            string tags = "";
                            string morepeople = "";

                            int numTag = 1;

                            using (var client = new WebClient())
                            {
                                //chemin = targetName + "\\" + titrePage + "\\" + titrePage + i + ".jpg";
                                //string sc = targetName + "\\" + titrePage + "\\screenshot_" + titrePage + i + ".jpg";

                                chemin = "Picture" + i + ".jpg";
                                nomDossierCommentScreenshot = "Picture" + i;
                                string sc = "screenshot_" + i + ".jpg";

                                client.DownloadFile(link, pathToSave + "\\PICTURES\\" + chemin);
                                Thread.Sleep(500);

                                //nomImage = pathToSave + "\\PICTURES\\" + chemin;
                                nomImage = "\\PICTURES\\" + chemin;
                            }

                                int suite = 1;
                                if(isElementPresent(driver, "//div[@class='nhd2j8a9 tv7at329 thwo4zme ggwglk7f oecfc0l4']"))
                                {
                                    int nbreImage = driver.FindElementsByXPath("//div[@class='nhd2j8a9 tv7at329 thwo4zme ggwglk7f oecfc0l4']").Count();

                                    if (!Directory.Exists(pathToSave + @"\PICTURES\ALBUMS"))
                                        Directory.CreateDirectory(pathToSave + @"\PICTURES\ALBUMS");

                                    while (suite <= nbreImage)
                                    {

                                        driver.FindElementsByXPath("//div[@class='hlyrhctz']")[1].Click();
                                        Thread.Sleep(2500);

                                        imageScreenshott = ((ITakesScreenshot)driver).GetScreenshot();


                                        //Save the screenshot
                                        imageScreenshott.SaveAsFile(pathToSave + @"\PICTURES_SCREENSHOTS\" + "PictureScreenshot_" + i + "_" + suite + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                                        Thread.Sleep(100);

                                        imageFinale = driver.FindElementByXPath("//div[@class='du4w35lb k4urcfbm stjgntxs ni8dbmo4 taijpn5t buofh1pr j83agx80 bp9cbjyn']");

                                        link = imageFinale.FindElement(By.TagName("img")).GetAttribute("src");



                                        nomImage = "";
                                        nomAlbum = "";
                                        morePeople = false;
                                        tags = "";
                                        morepeople = "";

                                        numTag = 1;

                                        using (var client = new WebClient())
                                        {
                                            //chemin = targetName + "\\" + titrePage + "\\" + titrePage + i + ".jpg";
                                            //string sc = targetName + "\\" + titrePage + "\\screenshot_" + titrePage + i + ".jpg";

                                            if (!Directory.Exists(pathToSave + @"\PICTURES\ALBUMS\ALBUM_" + "Picture" + i))
                                                Directory.CreateDirectory(pathToSave + @"\PICTURES\ALBUMS\ALBUM_" + "Picture" + i);

                                            chemin = "Picture" + i + "_" + suite + ".jpg";
                                            nomDossierCommentScreenshot = "Picture" + i + "_" + suite + ".jpg";
                                            string sc = "screenshot_" + i + "_" + suite + ".jpg";

                                            client.DownloadFile(link, pathToSave + "\\PICTURES\\ALBUMS\\ALBUM_" +"Picture" + i + "\\" + chemin);
                                            Thread.Sleep(500);

                                            //nomImage = pathToSave + "\\PICTURES\\" + chemin;
                                            nomImage = "\\PICTURES\\ALBUMS\\ALBUM_" + "Picture" + i + "\\" + chemin;
                                        }

                                        ligne += prix.Replace("\r", "").Replace("\n", "") + " " + intitule.Replace(";", "").Replace("\r", "").Replace("\n", "") + ";" + urll + ";" + chemin + ";" + "aucune" + "\n";
                                        suite++;
                                    }
                                }
                                
                            }
                        catch (Exception ex)
                        {

                        }
                        else
                        {

                        }

                        backgroundWorker1.ReportProgress(i);
                        i++;
                       

                    }






                    if (ligne != "")
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathToSave + @"\TXT\articles.txt", true))
                        {
                            //if (File.Exists(saveFileDialog1.FileName))
                            //    File.Delete(saveFileDialog1.FileName);

                            file.Write(ligne);

                        }
                    }

                    ligne = "";

                }

                
            }
            catch
            {
                ;
            }

            backgroundWorker1.ReportProgress(-102);
            Thread.Sleep(2000);

            //backgroundWorker1.ReportProgress(-3);
            //Thread.Sleep(2000);
            backgroundWorker1.CancelAsync();
            return;


            if (titrePage.Contains(") "))
                        titrePage = titrePage.Substring(titrePage.IndexOf(") ") + 2);

                    Thread.Sleep(rnd.Next(5500, 7500));




                try
                {
                    //var imageDown = driver.FindElementByXPath("tagWrapper']");//uiMediaThumb _6i9 uiMediaThumbMedium

                    if (isElementPresent(driver, "//div[@class='i09qtzwb rq0escxv n7fi1qx3 pmk7jnqg j9ispegn kr520xx4']"))
                    {
                        var tmp = driver.FindElementByXPath("//div[@class='i09qtzwb rq0escxv n7fi1qx3 pmk7jnqg j9ispegn kr520xx4']");
                        string tmpCode = ((OpenQA.Selenium.Remote.RemoteWebDriver)((OpenQA.Selenium.Remote.RemoteWebElement)tmp).WrappedDriver).PageSource;

                        string prenom = "";
                        //if (tmpCode.Contains("Vos photos"))
                        //    prenom = tmpCode.Substring(tmpCode.IndexOf("Vos photos") + 10).Split('<')[0];

                        if (tmpCode.Contains("Photos de ") && (tmpCode.Contains("Vos photos") || tmpCode.Contains("Photos prises par")))
                            tout = true;
                        else
                            tout = false;

                    }
                    var imageDown = driver.FindElementByXPath("//div[@class='j83agx80 btwxx1t3 lhclo0ds']");
                    string codePage = ((OpenQA.Selenium.Remote.RemoteWebDriver)((OpenQA.Selenium.Remote.RemoteWebElement)imageDown).WrappedDriver).PageSource;



                    //string [] codeImages = codePage.Split(new string[] { "<div class=\"tagWrapper" }, StringSplitOptions.RemoveEmptyEntries);
                    //string[] codeImages = codePage.Split(new string[] { "<a class=\"uiMediaThumb _6i9 uiMediaThumbMedium" }, StringSplitOptions.RemoveEmptyEntries);
                    IWebElement container = driver.FindElementByXPath("//div[@class='j83agx80 btwxx1t3 lhclo0ds']");
                    IList<IWebElement> imagees = container.FindElements(By.TagName("a"));

                    //IList <IWebElement> imagees = driver.FindElementsByXPath("//a[@class='oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz rq0escxv nhd2j8a9 a8c37x1j p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of lzcic4wl gmql0nx0 gpro0wi8 datstx6m l9j0dhe7 k4urcfbm']");

                    if (!Directory.Exists(pathToSave + "\\PICTURES\\"))
                        Directory.CreateDirectory(pathToSave + "\\PICTURES");

                    if (!Directory.Exists(pathToSave + "\\SCREENSHOTSPICTURESCOMMENTS\\"))
                        Directory.CreateDirectory(pathToSave + "\\SCREENSHOTSPICTURESCOMMENTS\\");

                    if (!Directory.Exists(pathToSave + "\\PICTURES_TAGGED\\"))
                        Directory.CreateDirectory(pathToSave + "\\PICTURES_TAGGED\\");

                    if (!Directory.Exists(pathToSave + "\\TXT\\"))
                        Directory.CreateDirectory(pathToSave + "\\TXT\\");

                    dossierLocal = pathToSave + @"\PICTURES";

                    //backgroundWorker1.ReportProgress(-1, (codeImages.Length - 1) + imgprec);

                    int counter = 0;
                    object[] codeImagess = imagees.ToArray();

                    object[] linktoImages = imagees.ToArray();

                    IList<string> liens = new List<string>();
                    foreach (IWebElement urll in codeImagess)
                    {
                        liens.Add(urll.GetAttribute("href"));

                    }



                    foreach (string urll in liens)
                    {

                        string urlForD = urll;
                        int nbsc = 1;



                        try
                        {
                            //Thread.Sleep(rnd.Next(7500, 9500));
                            driver.Navigate().GoToUrl(urlForD);
                            Thread.Sleep(rnd.Next(2500, 4500));

                            var imageFinale = driver.FindElementByXPath("//img[@class='gitj76qy r9f5tntg d2edcug0']");

                            string link = imageFinale.GetAttribute("src");



                            string nomImage = "";
                            string nomAlbum = "";
                            bool morePeople = false;
                            string tags = "";
                            string morepeople = "";

                            int numTag = 1;

                            using (var client = new WebClient())
                            {
                                //chemin = targetName + "\\" + titrePage + "\\" + titrePage + i + ".jpg";
                                //string sc = targetName + "\\" + titrePage + "\\screenshot_" + titrePage + i + ".jpg";

                                chemin = "Picture" + i + ".jpg";
                                nomDossierCommentScreenshot = "Picture" + i;
                                string sc = "screenshot_" + i + ".jpg";

                                client.DownloadFile(link, pathToSave + "\\PICTURES\\" + chemin);
                                Thread.Sleep(500);

                                //nomImage = pathToSave + "\\PICTURES\\" + chemin;
                                nomImage = "\\PICTURES\\" + chemin;

                                IList<IWebElement> list = new List<IWebElement>();
                                var tagImages = new ReadOnlyCollection<IWebElement>(list);
                                ////on essaie de récupérer des photos tagguées
                                try
                                {
                                    


                                    IList<IWebElement> els = new List<IWebElement>();
                                    IList<IWebElement> idss = new List<IWebElement>();
                                    //si autres personnes on clic et on prend les noms
                                    try
                                    {
                                        driver.FindElementByXPath("//div[@class='oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz rq0escxv nhd2j8a9 nc684nl6 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of lzcic4wl oo9gr5id gpro0wi8 lrazzd5p']").Click();
                                        Thread.Sleep(500);

                                        //o8kakjsu rpm2j7zs k7i0oixp gvuykj2m j83agx80 cbu4d94t ni8dbmo4 eg9m0zos l9j0dhe7 du4w35lb c3g1iek1 otl40fxz cxgpxx05 rz4wbd8a sj5x9vvc a8nywdso
                                        Object lastHeight = null;
                                        lastHeight = ((IJavaScriptExecutor)driver).ExecuteScript("return document.getElementsByClassName('j83agx80 cbu4d94t lzcic4wl ni8dbmo4 stjgntxs oqq733wu l9j0dhe7 du4w35lb cwj9ozl2 ue3kfks5 pw54ja7n uo3d90p7 l82x9zwi nwpbqux9 gc7gaz0o k4urcfbm')[0].scrollHeight");


                                        //Object lastHeight = ((IJavaScriptExecutor)driver).ExecuteScript("return document.getElementsByClassName('q5bimw55 rpm2j7zs k7i0oixp gvuykj2m j83agx80 cbu4d94t ni8dbmo4 eg9m0zos l9j0dhe7 du4w35lb ofs802cu pohlnb88 dkue75c7 mb9wzai9 d8ncny3e buofh1pr g5gj957u tgvbjcpo l56l04vs r57mb794 kh7kg01d c3g1iek1 k4xni2cv')[1].scrollHeight");

                                        while (true)
                                        {
                                            //driver.execute_script('document.getElementById("viewport").scrollTop += 100')

                                            ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('j83agx80 cbu4d94t lzcic4wl ni8dbmo4 stjgntxs oqq733wu l9j0dhe7 du4w35lb cwj9ozl2 ue3kfks5 pw54ja7n uo3d90p7 l82x9zwi nwpbqux9 gc7gaz0o k4urcfbm')[0].scrollTo(0, document.getElementsByClassName('q5bimw55 rpm2j7zs k7i0oixp gvuykj2m j83agx80 cbu4d94t ni8dbmo4 eg9m0zos l9j0dhe7 du4w35lb ofs802cu pohlnb88 dkue75c7 mb9wzai9 l56l04vs r57mb794 kh7kg01d c3g1iek1 otl40fxz cxgpxx05 rz4wbd8a sj5x9vvc a8nywdso')[0].scrollHeight)");
                                            Thread.Sleep(2000);

                                            Object newHeight = ((IJavaScriptExecutor)driver).ExecuteScript("return document.getElementsByClassName('j83agx80 cbu4d94t lzcic4wl ni8dbmo4 stjgntxs oqq733wu l9j0dhe7 du4w35lb cwj9ozl2 ue3kfks5 pw54ja7n uo3d90p7 l82x9zwi nwpbqux9 gc7gaz0o k4urcfbm')[0].scrollHeight");//ll8tlv6m j83agx80 taijpn5t hzruof5a
                                            if (newHeight.Equals(lastHeight))
                                            {
                                                break;
                                            }
                                            lastHeight = newHeight;
                                        }


                                        var el = driver.FindElementByXPath("//div[@class='ll8tlv6m j83agx80 taijpn5t hzruof5a']");
                                        //ll8tlv6m j83agx80 taijpn5t hzruof5a

                                        //IList<IWebElement> els = el.FindElements(By.TagName("a"));
                                        els = el.FindElements(By.XPath(".//a[@class='oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz rq0escxv nhd2j8a9 nc684nl6 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of lzcic4wl oo9gr5id gpro0wi8 lrazzd5p']"));
                                        idss = el.FindElements(By.XPath(".//div[@class='q9uorilb l9j0dhe7 pzggbiyp du4w35lb']"));

                                        //q9uorilb l9j0dhe7 pzggbiyp du4w35lb

                                        morePeople = true;
                                        int idCounterr = 0;
                                        string iddd = "999999";
                                        if (els.Count > 0)
                                        {
                                            foreach (IWebElement autre in els)
                                            {
                                                try
                                                {
                                                    iddd = idss[idCounterr].FindElement(By.TagName("image")).GetAttribute("xlink:href");
                                                    iddd = iddd.Substring(iddd.IndexOf("_") + 1).Split('_')[0];
                                                }
                                                catch
                                                {

                                                }



                                                //morepeople += autre.Text + ";" + iddd + ";" + autre.Text + "_" + "999999" + "_" + numTag + i + ".png" + ";" + autre.GetAttribute("href") + "\n";
                                                morepeople += autre.Text + ";" + iddd + ";" + "@@@@@@" + ";" + autre.GetAttribute("href") + "\n";

                                                //tags += autre.Text + ";" + iddd + ";" + sc + ";" + autre.GetAttribute("href") + "\n";

                                                idCounterr++;
                                            }
                                        }




                                    }//oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz rq0escxv nhd2j8a9 nc684nl6 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of lzcic4wl oo9gr5id gpro0wi8 lrazzd5p
                                    catch
                                    {
                                        morePeople = false;
                                       
                                    }

                                    if (morePeople)
                                    {
                                        try
                                        {
                                            //driver.FindElementByXPath("//div[@class='cypi58rs pmk7jnqg fcg2cn6m tkr6xdv7']").Click();
                                            //Thread.Sleep(500);
                                            driver.Navigate().GoToUrl(urlForD);
                                            Thread.Sleep(2500);
                                            morePeople = false;
                                        }
                                        catch
                                        {
                                            //driver.Navigate().GoToUrl(urlForD);
                                            //Thread.Sleep(2500);
                                            morePeople = false;
                                        }



                                    }

                                    //d2edcug0 hpfvmrgz qv66sw1b c1et5uql rrkovp55 a8c37x1j keod5gw0 nxhoafnm aigsh9s9 d3f4x2em fe6kdd0r mau55g9w c8b282yb mdeji52x a5q79mjw g1cxx5fr knj5qynh oo9gr5id
                                    try
                                    {
                                        //d2edcug0 hpfvmrgz qv66sw1b c1et5uql rrkovp55 a8c37x1j keod5gw0 nxhoafnm aigsh9s9 d3f4x2em fe6kdd0r mau55g9w c8b282yb mdeji52x a5q79mjw g1cxx5fr knj5qynh oo9gr5id
                                        tagImages = driver.FindElementsByXPath("//span[@class='oi732d6d ik7dh3pa d2edcug0 hpfvmrgz qv66sw1b c1et5uql a8c37x1j s89635nw ew0dbk1b a5q79mjw g1cxx5fr knj5qynh oo9gr5id']");
                                        if (tagImages.Count == 0)
                                        {
                                            try
                                            {
                                                tagImages = driver.FindElementsByXPath("//span[@class='d2edcug0 hpfvmrgz qv66sw1b c1et5uql rrkovp55 a8c37x1j keod5gw0 nxhoafnm aigsh9s9 d3f4x2em fe6kdd0r mau55g9w c8b282yb mdeji52x a5q79mjw g1cxx5fr knj5qynh oo9gr5id']");
                                            }
                                            catch
                                            {

                                            }
                                        }
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            tagImages = driver.FindElementsByXPath("//span[@class='d2edcug0 hpfvmrgz qv66sw1b c1et5uql rrkovp55 a8c37x1j keod5gw0 nxhoafnm aigsh9s9 d3f4x2em fe6kdd0r mau55g9w c8b282yb mdeji52x a5q79mjw g1cxx5fr knj5qynh oo9gr5id']");
                                        }
                                        catch
                                        {

                                        }
                                    }

                                    object[] tmp = tagImages.ToArray();

                                    if (tmp.Length > 0)
                                    {
                                        //int numTag = 1;
                                        //string tags = "";
                                        Dictionary<string, string> dicotags = new Dictionary<string, string>();



                                        foreach (IWebElement elli in tagImages)
                                        {
                                            var inputTags = elli.FindElements(By.TagName("a"));

                                            foreach (IWebElement ii in inputTags)
                                            {
                                                //string idTag = ii.GetAttribute("data-tag");
                                                string userNameTag = ii.Text;



                                                //if (idTag == "")
                                                string idTag = "999999";

                                                Screenshot imageScreenshot = ((ITakesScreenshot)driver).GetScreenshot();
                                                ////Save the screenshot
                                                //imageScreenshot.SaveAsFile(pathToSave + "\\PICTURES_TAGGED\\" + userNameTag + "_" + idTag + "_" + numTag + i + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                                                imageScreenshot.SaveAsFile(pathToSave + "\\PICTURES_TAGGED\\" + userNameTag + "_" + idTag + "_" + numTag + i + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);

                                                Thread.Sleep(500);

                                                morepeople = morepeople.Replace("@@@@@@", userNameTag + "_" + idTag + "_" + numTag + i + ".png");
                                                tags += morepeople;
                                                morepeople = "";

                                                tags += userNameTag + ";" + idTag + ";" + userNameTag + "_" + idTag + "_" + numTag + i + ".png\n";




                                                userNameTag = "";
                                                idTag = "";
                                                numTag++;


                                                if (!dicotags.ContainsKey(tags))
                                                {
                                                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathToSave + "\\TXT\\tags.txt", true))
                                                    {
                                                        //if (File.Exists(saveFileDialog1.FileName))
                                                        //    File.Delete(saveFileDialog1.FileName);

                                                        file.Write(tags);

                                                    }

                                  
                                                    dicotags.Add(tags, tags);
                                                    tags = "";
                                                }

                                            }

                                            tags = "";

                                        }



                                        tags = "";
                                        dicotags = new Dictionary<string, string>();
                                    }


                                }
                                catch (Exception ex)
                                {

                                }
                                backgroundWorker1.ReportProgress(i);

                                //Screenshot imageScreenshot = ((ITakesScreenshot)driver).GetScreenshot();
                                ////Save the screenshot
                                //imageScreenshot.SaveAsFile(pathToSave + @"\Facebook_Friends\" + targetName + "\\PICTURES\\" + sc, OpenQA.Selenium.ScreenshotImageFormat.Jpeg);

                                i++;

                            }

                            //on essaie de récupérer les identifiants des amis qui postent des commentaires

                            //Thread.Sleep(2500);

                            if (!Directory.Exists(pathToSave + "\\SCREENSHOTSPICTURESCOMMENTS\\" + nomDossierCommentScreenshot))
                                Directory.CreateDirectory(pathToSave + "\\SCREENSHOTSPICTURESCOMMENTS\\" + nomDossierCommentScreenshot);


                            Screenshot imageScreenshott = ((ITakesScreenshot)driver).GetScreenshot();
                            ////Save the screenshot
                            //imageScreenshot.SaveAsFile(pathToSave + "\\PICTURES_TAGGED\\" + userNameTag + "_" + idTag + "_" + numTag + i + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                            imageScreenshott.SaveAsFile(pathToSave + "\\SCREENSHOTSPICTURESCOMMENTS\\" + nomDossierCommentScreenshot + "\\image" + nbsc++ + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                            nomAlbum = nomDossierCommentScreenshot + "\\image1" + ".png";
                            Thread.Sleep(500);


                            try
                            {
                                try
                                {
                                    var lienComments = driver.FindElementsByXPath("//div[@class='oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz rq0escxv nhd2j8a9 nc684nl6 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of lzcic4wl l9j0dhe7 abiwlrkh gpro0wi8 dwo3fsh8 ow4ym5g4 auili1gw du4w35lb gmql0nx0']");

                                    foreach (IWebElement ll in lienComments)
                                    {

                                        //ll.Click();
                                        //Thread.Sleep(250);


                                        try
                                        {
                                            var secondLien = driver.FindElementByXPath("//div[@class='oajrlxb2 bp9cbjyn g5ia77u1 mtkw9kbi tlpljxtp qensuy8j ppp5ayq2 goun2846 ccm00jje s44p3ltw mk2mc5f4 rt8b4zig n8ej3o3l agehan2d sk4xxmp2 rq0escxv nhd2j8a9 pq6dq46d mg4g778l btwxx1t3 g5gj957u p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x tgvbjcpo hpfvmrgz jb3vyjys p8fzw8mz qt6c0cv9 a8nywdso l9j0dhe7 i1ao9s8h esuyzwwr f1sip0of du4w35lb lzcic4wl abiwlrkh gpro0wi8 m9osqain buofh1pr']");
                                            secondLien.Click();
                                            Thread.Sleep(250);

                                            imageScreenshott = ((ITakesScreenshot)driver).GetScreenshot();
                                            ////Save the screenshot
                                            //imageScreenshot.SaveAsFile(pathToSave + "\\PICTURES_TAGGED\\" + userNameTag + "_" + idTag + "_" + numTag + i + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                                            imageScreenshott.SaveAsFile(pathToSave + "\\SCREENSHOTSPICTURESCOMMENTS\\" + nomDossierCommentScreenshot + "\\image" + nbsc++ + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                                            nomAlbum = nomDossierCommentScreenshot + "\\image1" + ".png";
                                            Thread.Sleep(500);


                                        }
                                        catch
                                        {

                                        }

                                        break;

                                    }

                                    Thread.Sleep(500);
                                    string photosCommen = "";
                                    string nomCommen = "";
                                    var ligneCommens = driver.FindElementsByXPath("//a[@class='oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz rq0escxv nhd2j8a9 nc684nl6 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of lzcic4wl gmql0nx0 gpro0wi8']");
                                    var containerr = driver.FindElementByXPath("//div[@class='cwj9ozl2']");
                                    IList<IWebElement> bulles = containerr.FindElements(By.XPath(".//a[@class='oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz rq0escxv nhd2j8a9 nc684nl6 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of lzcic4wl gmql0nx0 gpro0wi8']"));

                                    IList<IWebElement> idcom = containerr.FindElements(By.XPath(".//div[@class='q9uorilb l9j0dhe7 pzggbiyp du4w35lb']"));



                                    int counterIDCom = 0;

                                    foreach (IWebElement lig in ligneCommens)
                                    {
                                        //on essaie de scroller afin de prendre des screenshots

                                        OpenQA.Selenium.Remote.RemoteWebElement o = (OpenQA.Selenium.Remote.RemoteWebElement)lig;

                                        if (o.LocationOnScreenOnceScrolledIntoView.Y > (height - 150))
                                        {
                                            // o.Click();
                                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", ligneCommens[counterIDCom - 1]);
                                            Thread.Sleep(2500);


                                            imageScreenshott = ((ITakesScreenshot)driver).GetScreenshot();
                                            ////Save the screenshot
                                            //imageScreenshot.SaveAsFile(pathToSave + "\\PICTURES_TAGGED\\" + userNameTag + "_" + idTag + "_" + numTag + i + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                                            imageScreenshott.SaveAsFile(pathToSave + "\\SCREENSHOTSPICTURESCOMMENTS\\" + nomDossierCommentScreenshot + "\\image" + nbsc + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                                            nomAlbum = nomDossierCommentScreenshot + "\\image" + nbsc++ + ".png";
                                            Thread.Sleep(500);


                                        }


                                        //string IDComment = lig.GetAttribute("data-hovercard").Substring(lig.GetAttribute("data-hovercard").IndexOf("php?id=") + 7).Split('&')[0];
                                        string urlCommen = lig.GetAttribute("href");
                                        string IDComment = "999999";

                                        nomCommen = lig.Text;

                                        if (counterIDCom + 1 < idcom.Count)
                                            try
                                            {
                                                IDComment = idcom[counterIDCom].FindElement(By.TagName("image")).GetAttribute("xlink:href");
                                                IDComment = IDComment.Substring(IDComment.IndexOf("_") + 1).Split('_')[0];

                                            }
                                            catch
                                            {

                                            }

                                        if (urlCommen.Contains("/profile.php?id="))
                                        {
                                            photosCommen = "&sk=photos";
                                            urlCommen = urlCommen.Split('&')[0];

                                            //if (urlCommen.Contains(">"))
                                            //{
                                            //urlCommen = urlCommen.Substring(urlCommen.IndexOf(">") + 1);
                                            urlCommen = urlCommen + photosCommen;

                                            //}
                                        }

                                        else
                                        {
                                            photosCommen = "/photos";
                                            urlCommen = urlCommen.Split('?')[0];
                                            urlCommen = urlCommen + photosCommen;
                                            urlCommen = urlCommen.Replace("/photos", "");
                                        }

                                        if (!dicocomments.ContainsKey(urlCommen))
                                        {
                                            dicocomments.Add(urlCommen, urlCommen + ";" + nomCommen + ";" + IDComment + ";" + nomAlbum);
                                        }

                                        counterIDCom++;
                                    }

                                }
                                catch
                                {

                                }






                            }
                            catch
                            {

                            }

                            //on récupère les commentaires en texte

                            try
                            {
                                var ligncommentaires = driver.FindElementsByXPath("//div[@class='q9uorilb bvz0fpym c1et5uql sf5mxxl7']");

                                foreach (IWebElement lig in ligncommentaires)
                                {


                                    //string IDComment = lig.GetAttribute("data-hovercard").Substring(lig.GetAttribute("data-hovercard").IndexOf("php?id=") + 7).Split('&')[0];
                                    string commment = "";
                                    string auteur = "";




                                    try
                                    {
                                        commment = lig.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)[1];

                                    }
                                    catch
                                    {

                                    }

                                    try
                                    {

                                        auteur = lig.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)[0];
                                    }
                                    catch
                                    {

                                    }


                                    commentairesTexte += auteur + ";" + commment + ";" + nomImage + ";" + urlForD + ";" + nomAlbum + "\n";


                                }
                            }
                            catch
                            {

                            }


                            //on essaie de récupérer les identifiants des amis qui postent des likes

                            try
                            {

                                //IWebElement el = driver.FindElementByXPath("//div[@class='_6iid']");////*[@id="u_o_2"]/div[2]/div/div[1]/div/div[1]

                                try
                                {
                                    //IWebElement el = driver.FindElementByXPath("//div[@class='oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz rq0escxv nhd2j8a9 nc684nl6 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of lzcic4wl l9j0dhe7 abiwlrkh p8dawk7l']");
                                    IWebElement el = driver.FindElementByXPath("//div[@class='oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz rq0escxv nhd2j8a9 a8c37x1j p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of lzcic4wl l9j0dhe7 abiwlrkh p8dawk7l gmql0nx0 ce9h75a5 ni8dbmo4 stjgntxs']");

                                    Thread.Sleep(250);
                                    el.Click();
                                    Thread.Sleep(500);

                                    Object lastHeight = null;
                                    lastHeight = ((IJavaScriptExecutor)driver).ExecuteScript("return document.getElementsByClassName('q5bimw55 rpm2j7zs k7i0oixp gvuykj2m j83agx80 cbu4d94t ni8dbmo4 eg9m0zos l9j0dhe7 du4w35lb ofs802cu pohlnb88 dkue75c7 mb9wzai9 l56l04vs r57mb794 kh7kg01d c3g1iek1 otl40fxz cxgpxx05 rz4wbd8a sj5x9vvc a8nywdso')[0].scrollHeight");


                                    //Object lastHeight = ((IJavaScriptExecutor)driver).ExecuteScript("return document.getElementsByClassName('q5bimw55 rpm2j7zs k7i0oixp gvuykj2m j83agx80 cbu4d94t ni8dbmo4 eg9m0zos l9j0dhe7 du4w35lb ofs802cu pohlnb88 dkue75c7 mb9wzai9 d8ncny3e buofh1pr g5gj957u tgvbjcpo l56l04vs r57mb794 kh7kg01d c3g1iek1 k4xni2cv')[1].scrollHeight");

                                    while (true)
                                    {
                                        //driver.execute_script('document.getElementById("viewport").scrollTop += 100')

                                        ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('q5bimw55 rpm2j7zs k7i0oixp gvuykj2m j83agx80 cbu4d94t ni8dbmo4 eg9m0zos l9j0dhe7 du4w35lb ofs802cu pohlnb88 dkue75c7 mb9wzai9 l56l04vs r57mb794 kh7kg01d c3g1iek1 otl40fxz cxgpxx05 rz4wbd8a sj5x9vvc a8nywdso')[0].scrollTo(0, document.getElementsByClassName('q5bimw55 rpm2j7zs k7i0oixp gvuykj2m j83agx80 cbu4d94t ni8dbmo4 eg9m0zos l9j0dhe7 du4w35lb ofs802cu pohlnb88 dkue75c7 mb9wzai9 l56l04vs r57mb794 kh7kg01d c3g1iek1 otl40fxz cxgpxx05 rz4wbd8a sj5x9vvc a8nywdso')[0].scrollHeight)");
                                        Thread.Sleep(2000);

                                        Object newHeight = ((IJavaScriptExecutor)driver).ExecuteScript("return document.getElementsByClassName('q5bimw55 rpm2j7zs k7i0oixp gvuykj2m j83agx80 cbu4d94t ni8dbmo4 eg9m0zos l9j0dhe7 du4w35lb ofs802cu pohlnb88 dkue75c7 mb9wzai9 l56l04vs r57mb794 kh7kg01d c3g1iek1 otl40fxz cxgpxx05 rz4wbd8a sj5x9vvc a8nywdso')[0].scrollHeight");//ll8tlv6m j83agx80 taijpn5t hzruof5a
                                        if (newHeight.Equals(lastHeight))
                                        {
                                            break;
                                        }
                                        lastHeight = newHeight;
                                    }


                                    el = driver.FindElementByXPath("//div[@class='ll8tlv6m j83agx80 taijpn5t hzruof5a']");
                                    //ll8tlv6m j83agx80 taijpn5t hzruof5a

                                    //IList<IWebElement> els = el.FindElements(By.TagName("a"));
                                    IList<IWebElement> els = el.FindElements(By.XPath(".//a[@class='oajrlxb2 g5ia77u1 qu0x051f esr5mh6w e9989ue4 r7d6kgcz rq0escxv nhd2j8a9 nc684nl6 p7hjln8o kvgmc6g5 cxmmr5t8 oygrvhab hcukyx3x jb3vyjys rz4wbd8a qt6c0cv9 a8nywdso i1ao9s8h esuyzwwr f1sip0of lzcic4wl oo9gr5id gpro0wi8 lrazzd5p']"));
                                    IList<IWebElement> ids = el.FindElements(By.XPath(".//div[@class='q9uorilb l9j0dhe7 pzggbiyp du4w35lb']"));

                                    //q9uorilb l9j0dhe7 pzggbiyp du4w35lb

                                    int idCounter = 0;

                                    string urllll = "";
                                    string photosComment = "";
                                    string id = "999999";
                                    foreach (IWebElement aelement in els)
                                    {
                                        if (aelement.Text == "")
                                            continue;

                                        urllll = aelement.GetAttribute("href").Split(new String[] { "__tn" }, StringSplitOptions.RemoveEmptyEntries)[0];

                                        try
                                        {
                                            id = ids[idCounter].FindElement(By.TagName("image")).GetAttribute("xlink:href");
                                            id = id.Substring(id.IndexOf("_") + 1).Split('_')[0];
                                        }
                                        catch
                                        {

                                        }

                                        if (urllll.Contains("profile.php?"))
                                            urllll = urllll.Replace("&", "");
                                        else
                                            urllll = urllll.Replace("?", "");

                                        if (!dicocomments.ContainsKey(urllll))
                                        {
                                            dicocomments.Add(urllll, urllll + ";" + aelement.Text + ";" + id);
                                        }
                                        //break;

                                        idCounter++;
                                    }

                                }
                                catch
                                {

                                }



                                Thread.Sleep(2500);
                                driver.Navigate().GoToUrl(urlForD);
                                Thread.Sleep(2500);






                            }
                            catch
                            {
                                //pictureBoxpictures.Image = global::FacebookAnalyzer.Properties.Resources.ko;
                            }




                        }
                        catch (Exception ex)
                        {
                            //pictureBoxpictures.Image = global::FacebookAnalyzer.Properties.Resources.ko;
                        }
                        //i++;
                        nbreImages++;
                        //backgroundWorker1.ReportProgress(nbreImages);

                    }
                    //imgprec = codeImages.Length;





                }
                catch (Exception ex)
                {


                    //pictureBoxpictures.Image = global::FacebookAnalyzer.Properties.Resources.ko;
                }

            

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
            driver.Manage().Window.Maximize();
        }

        private void button12_Click(object sender, EventArgs e)
        {

            if (textBoxops.Text == "" || textBoxArticle.Text == "")
            {
                textBoxops.BackColor = Color.Red;
                MessageBox.Show("Veuillez remplir le champ OPS ou ARTICLE");
                return;
            }
            else
                textBoxops.BackColor = Color.White;


            if (!pathToSave.Contains(textBoxops.Text))
                pathToSave = pathToSaveBAK + textBoxops.Text + "_" + textBoxArticle.Text;

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
                            dataGridViewArticles.Rows.Add(bouton, img, champ[0], champ[1], champ[2]);

                            if (Directory.Exists(pathToSave + "\\PICTURES\\ALBUMS\\ALBUM_" + champ[2].Split('.')[0]))
                                if (Directory.GetFiles(pathToSave + "\\PICTURES\\ALBUMS\\ALBUM_" + champ[2].Split('.')[0], champ[2].Split('.')[0] + "_*").Count() == 0)
                                {

                                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                                    dataGridViewArticles.Rows[dataGridViewArticles.Rows.Count - 1].Cells[0] = txtcell;

                                }
                        }
                        else
                        {
                            Image img = FacebookAnalyzer.Properties.Resources.Business_Shop_icon;
                            Button bouton = new Button();
                            dataGridViewArticles.Rows.Add(bouton, img, champ[0], champ[1], champ[2]);

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
                        Image img = FacebookAnalyzer.Properties.Resources.Business_Shop_icon;
                        Button bouton = new Button();
                        dataGridViewArticles.Rows.Add(bouton, img, champ[0], champ[1], champ[2]);

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
                    if (Directory.Exists(pathToSave + "\\PICTURES\\ALBUMS\\ALBUM_" + dataGridViewArticles.Rows[e.RowIndex].Cells[4].Value.ToString().Split('.')[0]))
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


                CopyData(pathToSave, folderBrowserDialog1.SelectedPath, textBoxops.Text);

                string ligne = "<Case>\n";

                
                    //Données dans les champs

                    string username = textBoxUSERNAME.Text;
                    string password = textBoxPASSWORD.Text;
                    string article = textBoxArticle.Text;

                //Données dans les champs
                if (!checkBoxSavepassword.Checked)
                    {
                        username = "";
                        password = "";
                    }


                    ligne += textBoxUSERNAMEFRIENDS.Text + ";" + username + ";" + password + ";" + textBoxops.Text.ToUpper() + ";" + "..\\" + textBoxops.Text + ";" + article + ";" + labelID.Text +"\n";
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

            if (!Directory.Exists(pathToSaveBAK + ops  + "_" + textBoxArticle.Text))
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

            foreach (string dir in Directory.GetDirectories(pathToSaveBAK + ops + "_" + textBoxArticle.Text))
            {
                DirectoryInfo dirr = new DirectoryInfo(dir);

                if (!Directory.Exists(dest + "\\" + ops + "\\" + dirr.Name))
                    Directory.CreateDirectory(dest + "\\" + ops + "\\" + dirr.Name);

                DirectoryCopy(dir, dest + "\\" + ops + "\\" + dirr.Name, true);


            }

            foreach (string fichierinRoot in Directory.GetFiles(pathToSaveBAK + ops + "_" + textBoxArticle.Text))
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

            
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Viewers\\ViewerMarket.exe"))
                {
                    File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Viewers\\ViewerMarket.exe", dest + "\\" + ops + "\\ViewerMarket.exe", true);
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
