﻿using System;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager) { }

        internal void Register(AccountData account)
        {
            OpenMainPage();
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();
            string url = GetConfirmationUrl(account);
            FillPasswordForm(url, account);
            SubmitPasswordForm();
        }

        private void SubmitPasswordForm()
        {
            //!
            manager.Driver.FindElement(By.CssSelector("input.button")).Click();
        }

        private void FillPasswordForm(string url, AccountData account)
        {
            driver.Url = url;
            manager.Driver.FindElement(By.Name("password")).SendKeys(account.Password);
            manager.Driver.FindElement(By.Name("password_confirm")).SendKeys(account.Password);
        }

        private string GetConfirmationUrl(AccountData account)
        {
            string message = manager.Mail.GetLastMail(account);
            Match match = Regex.Match(message, @"http://\S*");
            return match.Value;
        }

        private void OpenRegistrationForm()
        {
            manager.Driver.FindElement(By.CssSelector(@"a[class='back-to-login-link pull-left']")).Click();
        }

        private void SubmitRegistration()
        {
            manager.Driver.FindElement(By.CssSelector(@"input[class='width-40 pull-right btn btn-success btn-inverse bigger-110']")).Click();
        }

        private void FillRegistrationForm(AccountData account)
        {
            manager.Driver.FindElement(By.Name("username")).SendKeys(account.Name);
            manager.Driver.FindElement(By.Name("email")).SendKeys(account.Email);
            
        }

        private void OpenMainPage()
        {
            manager.Driver.Url = "http://192.168.238.13/mantisbt-2.23.0/login_page.php";
        }
    }
}