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
            app.Projects.CreateProjectIfNotExists();
            List<ProjectData> oldList = ProjectData.GetALL();
            
            // action
            app.Projects.Remove(oldList[0]);

            // считаем кол-во
            Assert.AreEqual(oldList.Count - 1, app.Projects.GeProjectsCount());

            List<ProjectData> newList = ProjectData.GetALL();

            oldList.Remove(oldList[0]);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
               

    }
}
