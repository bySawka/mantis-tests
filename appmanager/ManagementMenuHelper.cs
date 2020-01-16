using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ManagementMenuHelper : HelperBase
    {
        private string baseUrl;
        public ManagementMenuHelper(ApplicationManager manager, string baseUrl) : base(manager)
        {
            this.baseUrl = baseUrl;
        }

        public void GoToManageProject()
        {
           if (driver.Url != baseUrl + "/manage_proj_page.php")
            {
                driver.Navigate().GoToUrl(baseUrl + "/manage_proj_page.php");
            }
        }

    }
}
