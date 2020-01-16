using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    class AddingProjectTests : AuthTestBase
    {
        [Test]
        public void AddingProjectTest()
        {

            // prepare
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            List<ProjectData> oldList = ProjectData.GetALLFromWebServer(account);
            ProjectData project = app.Projects.GetValidProject(oldList);

            // action
            app.Projects.Create(project);

            // считаем кол-во
            Assert.AreEqual(oldList.Count + 1, app.Projects.GeProjectsCount());

            List<ProjectData> newList = ProjectData.GetALLFromDB();

            oldList.Add(project);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
               

    }
}
