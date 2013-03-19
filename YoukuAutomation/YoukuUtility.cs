using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using AutoItX3Lib;

namespace YoukuAutomation
{
    [TestFixture]
    public class YoukuUtility
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        private static bool fileupflag;
        private static bool loginflag;

        /// <summary>
        /// 单例
        /// </summary>
        /// <value>The instance.</value>
        public static YoukuUtility Instance { get { return Singleton<YoukuUtility>.GetInstance(); } }

        [SetUp]
        public void SetupFF()
        {
            driver = new FirefoxDriver();
            baseURL = "http://www.youku.com/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void DoUpload(string Filepath)
        {
            driver.Navigate().GoToUrl(baseURL + "/v/upload");
            try
            {
                Thread.Sleep(1000);
                if (driver.FindElement(By.ClassName("uploadHot")) != null)
                {
                    loginflag = true;
                }
            }
            catch
            {
 
            }
            while (!loginflag)
            {
                try
                {
                    Thread.Sleep(1000);
                    driver.FindElement(By.Id("user_name_login")).Clear();
                    driver.FindElement(By.Id("user_name_login")).SendKeys("jnrain_game@163.com");
                    driver.FindElement(By.Id("passwd_login")).Clear();
                    driver.FindElement(By.Id("passwd_login")).SendKeys("613866");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("当前捕捉失败，继续扫描,错误消息为" + ex.Message);
                }
            }

            if (!loginflag)
            {
                Console.WriteLine("登陆完成后请敲回车：enter");
                Console.ReadLine();
                driver.Navigate().GoToUrl(baseURL + "/v/upload");
            }

            

            IWebElement approve = driver.FindElement(By.ClassName("uploadHot"));

            new Thread(() =>
            {

                //while (true)
                //{
                    try
                    {
                        //if (fileupflag) break;
                        Thread.Sleep(1000);
                        var Autoit = new AutoItX3();
                        const string widowTitle = "[Class:#32770]";
                        Autoit.WinWait(widowTitle, "", 1);
                        Autoit.WinActivate(widowTitle);
                        Autoit.ControlFocus("[Class:#32770]", "", "[CLASS:Edit; INSTANCE:1]");
                        Autoit.Send(Filepath);
                        Autoit.Sleep(100);
                        Autoit.ControlClick("[Class:#32770]", "Open(O)", "Cancel");
                        Autoit.Sleep(100);
                        Autoit.Send("!O");
                        Autoit.Send("{ENTER}", 0);
                        //Thread.CurrentThread.Abort();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("当前捕捉失败，继续扫描,错误消息为" + ex.Message);
                    }
                //}
                //System.Diagnostics.ProcessStartInfo ImgstartInfo = new System.Diagnostics.ProcessStartInfo("fileup.exe");
                //ImgstartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //System.Diagnostics.Process.Start(ImgstartInfo);
            }


                ).Start();

            approve.Click();
            fileupflag = true;

            Thread.Sleep(2000);

            driver.FindElement(By.Name("memo")).Clear();
            driver.FindElement(By.Name("memo")).SendKeys("qqq");
            driver.FindElement(By.CssSelector("div.meta-cate.upOriginal  > span")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("其他")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("input.input-mini.meta-tags")).Clear();
            driver.FindElement(By.CssSelector("input.input-mini.meta-tags")).SendKeys("qq");
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("span.form_btn_text")).Click();
            Thread.Sleep(2000);
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alert.Text;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
