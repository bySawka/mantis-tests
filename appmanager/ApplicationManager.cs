using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ApplicationManager
    {

        protected IWebDriver driver;
        protected string baseURL;

        public RegistrationHelper Registration { get;  set; }
        public FtpHelper Ftp { get;  set; }
        public JamesHelper James { get;  set; }

        public MailHelper Mail { get; set; }
        public ManagementMenuHelper ManagementMenu { get;  set; }
        public ProjectManagementHelper Projects { get;  set; }
        public LoginHelper Auth { get;  set; }

        // специальный объект, который устанавливает соответствие между текущим потоком
        // и объектом типа ApplicationManager
        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            // объект нужен для того что 
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            baseURL = "http://192.168.238.13/mantisbt-2.23.0";
            Registration = new RegistrationHelper(this, baseURL);
 
            ManagementMenu = new ManagementMenuHelper(this, baseURL);
            Projects = new ProjectManagementHelper(this);
            Auth = new LoginHelper(this, baseURL);


            //Ftp = new FtpHelper(this);
            // James = new JamesHelper(this);
            //  Mail = new MailHelper(this);
        }

        // деструктор
        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            // если для текущего потока, внутри хранилища, ничего не созданно 
            if (!app.IsValueCreated)
            {
                // Создаем новый экзмепляр                
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = "http://192.168.238.13/mantisbt-2.23.0/login_page.php";
                app.Value = newInstance;
            }
            // для каждого потока будет разным
            return app.Value;
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

 
    }
}
