using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    class ProjectRemovalTests : AuthTestBase
    {
        [Test]
         public void ProjectRemovalTest()
        {
            // prepare
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            ProjectData project = new ProjectData()
            {
                Name = TestBase.GenerateRandomString(20),
                Description = TestBase.GenerateRandomString(200)
            };

            List<ProjectData> oldList = ProjectData.GetALLFromWebServer(account);

            if (oldList ==null)
            {
                app.API.CreateNewProject(account, project);
            }

            // action
            app.API.RemoveProject(account, oldList[0]);

            // считаем кол-во
            Assert.AreEqual(oldList.Count - 1, app.Projects.GeProjectsCount());

            List<ProjectData> newList = ProjectData.GetALLFromWebServer(account);

            oldList.Remove(oldList[0]);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
               

    }
}
