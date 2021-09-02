using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json.Linq;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace PRAA.Steps
{
    [Binding]
    public class SignupSteps
    {
        readonly dynamic specflowJson = JObject.Parse(File.ReadAllText(@"..\\PRAA\specflow.json"));

        IWebDriver driver;


        readonly private static Random random = new Random();
        string user;


        [Given(@"You are a student")]
        public void GivenYouAreAStudent()
        {
            var url = Convert.ToString(specflowJson["URL"]);
            var student_btn = Convert.ToString(specflowJson["STUDENT_BTN"]);
            var month_input = Convert.ToString(specflowJson["MONTH_INPUT"]);
            var day_input = Convert.ToString(specflowJson["DAY_INPUT"]);
            var year_input = Convert.ToString(specflowJson["YEAR_INPUT"]);
            var submit_btn = Convert.ToString(specflowJson["SUBMIT_BTN"]);


            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");

            driver = new ChromeDriver(chromeOptions);

            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.XPath(student_btn)).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.FindElement(By.XPath(month_input)).Click();
            driver.FindElement(By.XPath(month_input)).SendKeys("Jun");

            driver.FindElement(By.XPath(day_input)).Click();
            driver.FindElement(By.XPath(day_input)).SendKeys("20");

            driver.FindElement(By.XPath(year_input)).Click();
            driver.FindElement(By.XPath(year_input)).SendKeys("2000");

            driver.FindElement(By.XPath(submit_btn)).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }


        [When(@"you successfully signup with your email address")]
        public void WhenYouSuccessfullySignupWithYourEmailAddress()
        {
            var username_input = Convert.ToString(specflowJson["USERNAME_INPUT"]);
            var email_input = Convert.ToString(specflowJson["EMAIL_INPUT"]);
            var password_input = Convert.ToString(specflowJson["PASSWORD_INPUT"]);
            var signup_btn = Convert.ToString(specflowJson["SIGNUP_BTN"]);
            var username = Convert.ToString(specflowJson["username"]);
            var error = Convert.ToString(specflowJson["ERROR_USERNAME"]);
            var suggestion = Convert.ToString(specflowJson["SUGGESTION"]);
            var domain = Convert.ToString(specflowJson["domain"]);
            var password = Convert.ToString(specflowJson["pwd"]);
            var submit_btn = Convert.ToString(specflowJson["SUBMIT_BTN"]);

            driver.FindElement(By.XPath(username_input)).Click();
            driver.FindElement(By.XPath(username_input)).SendKeys(username + random.Next(10000).ToString());
            //driver.FindElement(By.XPath(username_input)).SendKeys(username + "792");
            user = driver.FindElement(By.XPath(username_input)).GetAttribute("value");
            driver.FindElement(By.XPath(submit_btn)).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);



            if (IsElementPresent(By.XPath(error)))
            {
                driver.FindElement(By.XPath(suggestion)).Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                user = driver.FindElement(By.XPath(username_input)).GetAttribute("value");
                driver.FindElement(By.XPath(submit_btn)).Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


            }


            driver.FindElement(By.XPath(email_input)).Click();
            driver.FindElement(By.XPath(email_input)).SendKeys(username + random.Next(1000).ToString() + domain);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            driver.FindElement(By.XPath(password_input)).Click();
            driver.FindElement(By.XPath(password_input)).SendKeys(password);

            driver.FindElement(By.XPath(signup_btn)).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            


        }
        
        [Then(@"you login to your kahoot account")]
        public void ThenYouLoginToYourKahootAccount()
        {
            
            var profile_name = Convert.ToString(specflowJson["PROFILE_NAME"]);
            string profile_username = driver.FindElement(By.XPath(profile_name)).Text;
            Console.WriteLine("Profile Name: " + profile_username);
            Console.WriteLine("User Name: " + user);

            Assert.AreEqual(profile_username, user);
            driver.Quit();

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

    }
}
