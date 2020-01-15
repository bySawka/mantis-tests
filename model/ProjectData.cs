using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB.Mapping;

namespace mantis_tests
{
    [Table(Name = "mantis_project_mantis")]
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        [Column(Name = "name"), NotNull]
        public string Name { get; set; }
        [Column(Name = "description"), NotNull]
        public string Description { get; set; }

        [Column(Name = "id"), NotNull, PrimaryKey, Identity]
        public int Id { get; set; }

        public int CompareTo(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            int compare = Name.CompareTo(other.Name);
            if (compare == 0)
            {
                compare = Description.CompareTo(other.Description);
            }

            return compare;
        }

        public bool Equals(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name
                && Description == other.Description;
        }

        public override int GetHashCode()
        {
            return (Name + Description).GetHashCode();
        }
        public static List<ProjectData> GetALL()
        {
            using (MantisDB db = new MantisDB())
            {
                return (from p in db.Projects select p).ToList();
            }
        }
    }
}
