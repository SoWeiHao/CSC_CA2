using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Configuration;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{

    public class TestingController : Controller
    {
        private static IWebDriver driver;

        TestObject[] myObjArray = new TestObject[] { new TestObject("Windows 10", "Firefox", "84.0"), new TestObject("Windows 10", "Chrome", "87.0") };

        public string Index()
        {
            for (int x = 0; x < myObjArray.Length; x++)
            {
                DesiredCapabilities caps1 = new DesiredCapabilities();
                caps1.SetCapability("platform", myObjArray[x].platform);
                caps1.SetCapability("browserName", myObjArray[x].browserName); // name of your browser
                caps1.SetCapability("version", myObjArray[x].version); // version of your selected browser
                caps1.SetCapability("name", "CSharpTestSample");
                caps1.SetCapability("build", "LambdaTestSampleApp");
                caps1.SetCapability("user", "");
                caps1.SetCapability("accessKey", "");
                caps1.SetCapability("network", true); // To enable network logs
                caps1.SetCapability("visual", true); // To enable step by step screenshot
                caps1.SetCapability("video", true); // To enable video recording
                caps1.SetCapability("console", true); // To capture console logs
                caps1.SetCapability("tunnel", true);
                Debug.WriteLine("https://hub.lambdatest.com/wd/hub");

                driver = new RemoteWebDriver(new Uri("https://hub.lambdatest.com/wd/hub"), caps1, TimeSpan.FromSeconds(600));
                driver.Manage().Window.Maximize();
                driver.Url = "https://localhost:44391/";

                Assert.AreEqual("Home Page - WebApplication2", driver.Title);
                String itemName = "";
                String password = "";
                // Click on First Check box
                IWebElement firstCheckBox = driver.FindElement(By.Name("Login"));
                firstCheckBox.Click();

                // Enter Item name 
                IWebElement textfield = driver.FindElement(By.Id("Input_UserName"));
                textfield.SendKeys(itemName);

                IWebElement textfield1 = driver.FindElement(By.Id("Input_Password"));
                textfield1.SendKeys(password);

                // Click on Add button
                IWebElement addButton = driver.FindElement(By.Name("login111"));
                addButton.Click();

                // Verified Added Item name
                IWebElement itemtext = driver.FindElement(By.Id("logindetails"));
                Debug.WriteLine(itemtext);
                String getText = itemtext.Text;
                Debug.WriteLine(getText);
                Assert.IsTrue(getText.Contains(itemName));

                driver.Quit(); //really important statement for preventing your test execution from a timeout.
            }
            return "DONE";

        }
    }
}
