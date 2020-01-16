using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ProjectManagementHelper CreateProjectIfNotExists()
        {
            manager.ManagementMenu.GoToManageProject();
            if (GeProjectsCount() == 0)
            {
                Create(new ProjectData()
                {
                    Name = TestBase.GenerateRandomString(20),
                    Description = TestBase.GenerateRandomString(200)
                });
            }
            return this;
        }

        public ProjectManagementHelper Create(ProjectData project)
        {
            manager.ManagementMenu.GoToManageProject();
            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
            manager.ManagementMenu.GoToManageProject();
            return this;
        }

        public ProjectManagementHelper Remove(ProjectData project)
        {
            manager.ManagementMenu.GoToManageProject();
            selectRemovalProject(project.Id);
            removeProject();
            ConfirmRemove();
             return this;
        }

        public ProjectManagementHelper selectRemovalProject(int id)
        {
            manager.Driver.FindElement(By.XPath("//a[@href='manage_proj_edit_page.php?project_id=" + id+"']")).Click();
            return this;
        }

        public void ConfirmRemove()
        {
            manager.Driver.FindElement(By.CssSelector(@"input[class='btn btn-primary btn-white btn-round']")).Click();
        }

        public void removeProject()
        {
            manager.Driver.FindElement(By.CssSelector(@"input[class='btn btn-primary btn-sm btn-white btn-round']")).Click();
        }

        public int GeProjectsCount()
        {
  
            return driver.FindElements(By.CssSelector("div.table-responsive"))[0].
                          FindElement(By.TagName("tbody")).
                          FindElements(By.TagName("tr")).Count;
        }

        public ProjectData GetValidProject(List<ProjectData> oldList)
        {
            // для того чтобы добавить проект - имя должно быть уникальным
            ProjectData project = new ProjectData()
            {
                Description = TestBase.GenerateRandomString(200)
            };

            do
            {
                project.Name = TestBase.GenerateRandomString(20);

            } while (oldList.Exists(x => x.Name == project.Name));

            return project;

        }

        private void SubmitProjectCreation()
        {
            manager.Driver.FindElement(By.CssSelector(@"input[class='btn btn-primary btn-white btn-round']")).Click();

        }

        public ProjectManagementHelper FillProjectForm(ProjectData projectData)
        {
            Type(By.Id("project-name"), projectData.Name);
            Type(By.Id("project-description"), projectData.Description);
            return this;
        }

        public void InitProjectCreation()
        {
            manager.Driver.FindElement(By.CssSelector(@"button[class='btn btn-primary btn-white btn-round']")).Click();
        }


    }
}
