using LinqToDB;

namespace mantis_tests
{
    public class MantisDB : LinqToDB.Data.DataConnection
    {
        public MantisDB() : base("Bugtracker") { }
        public ITable<ProjectData> Projects { get { return GetTable<ProjectData>(); } }
    }
}
