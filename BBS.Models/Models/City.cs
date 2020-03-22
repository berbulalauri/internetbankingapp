using System;
using System.Collections.Generic;

namespace BBS.Models.Models
{
    public partial class City : ISoftDeletable, IComparable
    {
        public City()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public bool IsDeleted { get; set; }
        public int CompareTo(object obj)
        {
            return (obj as City).Name.CompareTo(Name);
        }
    }
}
