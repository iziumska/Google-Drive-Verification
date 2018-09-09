// Google Drive Verification
// Sign into the newly created account
// Go to Google Drive
// Upload File
// Check that the file was uploaded
// if the file was uploaded, the script should delete the file
// if the file wasn’t uploaded, the script should display “The file wasn’t uploaded” error message
// Sign out 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Chrome;
using System.Threading;
using AutoItX3Lib;

namespace ClassLibrary1.TestCases
{

    class UploadFileTest
    {
        IWebDriver driver;
        string urlForTests = "http://google.com";
        string userName = "anna.dnepr2018@gmail.com";
        string userPassword = "turina02";
        string expectedFileName = "FileForTest";

        [SetUp]
        public void InitializeDriverLogIn() // initialize Chrome Driver and LogIn User
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(urlForTests);

            // Click on ButtonAccount
            driver.FindElement(By.Id("gb_7d")).Click();

            // Enter UserName in InputField and click Enter
            IWebElement UserNameField = driver.FindElement(By.Id("identifierId"));
            UserNameField.Clear();
            UserNameField.SendKeys(userName + Keys.Enter);

            // Enter Password in InputField and click Enter
            IWebElement UserPasswordField = driver.FindElement(By.Name("password"));
            UserPasswordField.Clear();
            UserPasswordField.SendKeys(userPassword + Keys.Enter);

            // Open Account Google Page
            driver.FindElement(By.ClassName("WpHeLc")).Click();

            // Open Google Applications and select Google Disk
            driver.FindElement(By.ClassName("gb_ge")).Click();
            driver.FindElement(By.ClassName("gb_W")).Click();
        }

        [Test]
        public void ExecuteTest() // Upload file and check download
        {
            // Click on UploadFileButton and select option "Douwload File"
            driver.FindElement(By.ClassName("uw8t2")).Click();
            driver.FindElement(By.ClassName("q-v-T")).Click();

            AutoItX3 autoIt = new AutoItX3();
            autoIt.WinActivate("Open");

            try
            {
                autoIt.Send(@"C:\Users\Anna\FileForTest.txt");
                Thread.Sleep(1000);
                autoIt.Send("{ENTER}");

                IWebElement actual = driver.FindElement(By.LinkText("FileForTest"));

                string actualFileName = actual.Text;

                Assert.AreEqual(expectedFileName, actualFileName);

                // Select and delete File
                actual.Click();
                driver.FindElement(By.ClassName("a-s-fa-Ha-pa")).Click();
            }  
            catch(Exception ex)
            {
                Console.WriteLine("The file wasn’t uploaded" + ex.Message);

            }
        }

        [TearDown]
        public void CleanUp() // SingOut User and close Chrome Driver
        {
            // Click on User Field
            driver.FindElement(By.ClassName("gb_9a gbii"));
            
            // Click on SingOut Button
            driver.FindElement(By.Id("gb_71")).Click();

            // Close Browser
            driver.Close();
        }
    }
}
