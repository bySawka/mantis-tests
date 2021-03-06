﻿using System.Collections.Generic;
using System.IO;
using NUnit.Framework;


namespace mantis_tests
{
    
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [OneTimeSetUp]
        public void setUpConfig()
        {
            app.Ftp.BackUpFile("/config_inc.php");
            using (Stream localfile = File.OpenRead(Path.Combine(TestContext.CurrentContext.TestDirectory, "config_inc.php")))
            {
                app.Ftp.Upload("/config_inc.php", localfile);
            }
        }

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = "testUser13",
                Password = "password",
                Email = "testuser13@localhost"
            };

           List<AccountData> accounts = app.Admin.GetAllAccounts();
           AccountData existingAccount = accounts.Find(x => x.Name == account.Name);
            if (existingAccount != null)
            {
                app.Admin.DeleteAccount(existingAccount);
            }
            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);

        }

        [OneTimeTearDown]
        public void restoreConfig()
        {
            app.Ftp.RestoreBackUpFile("/config_inc.php");
        }

    }
  }
