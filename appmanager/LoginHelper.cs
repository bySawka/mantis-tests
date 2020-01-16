using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        private string baseUrl;
        public LoginHelper(ApplicationManager manager, string baseUrl) : base(manager)
        {
            this.baseUrl = baseUrl;
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }
                Logout();
            }

            Type(By.Name("username"), account.Name);
            driver.FindElement(By.XPath(@"//input[@type='submit']")).Click();
            Type(By.Name("password"), account.Password);
            driver.FindElement(By.XPath(@"//input[@type='submit']")).Click();  
        }


        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.ClassName("user-info")).Click();
                driver.FindElement(By.XPath(@"//a[@href='/mantisbt-2.23.0/logout_page.php']")).Click();
                //Ждем загрузку страницы
                driver.FindElement(By.Name("username")).Click();
            }
        }

        public bool IsLoggedIn()
        {
            //done
            return IsElementPresent(By.ClassName("user-info"));
        }   

        public bool IsLoggedIn(AccountData account)
        {
            // done
            return IsLoggedIn()
                && GetLoggetUserName() == account.Name;
        }

        public string GetLoggetUserName()
        {
            //done
            return driver.FindElement(By.ClassName("user-info")).Text;
        }
    }
}
