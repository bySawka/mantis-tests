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
            List<ProjectData> oldList = ProjectData.GetALL();
            ProjectData project = app.Projects.GetValidProject(oldList);

            // action
            app.Projects.Create(project);

            // считаем кол-во
            Assert.AreEqual(oldList.Count + 1, app.Projects.GeProjectsCount());

            List<ProjectData> newList = ProjectData.GetALL();

            oldList.Add(project);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
               

    }
}
