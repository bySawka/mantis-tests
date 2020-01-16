using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    class ApiHelper : HelperBase
    {
        public ApiHelper(ApplicationManager manager) : base(manager) {}

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(account.Name, account.Password, issue);
        }

        public void CreateNewProject(AccountData account, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData projectMantis = new Mantis.ProjectData();
            projectMantis.name = project.Name;
            projectMantis.description = project.Description;
            client.mc_project_add(account.Name, account.Password, projectMantis);
        }

        public void RemoveProject(AccountData account, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData projectMantis = new Mantis.ProjectData();
            projectMantis.id = project.Id;
            client.mc_project_delete(account.Name, account.Password, project.Id);
        }
    }
}
