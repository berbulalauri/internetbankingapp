using System;
using System.Collections.Generic;

namespace BBS.Models.Models
{
    public partial class Question : ISoftDeletable, IComparable
    {
        public Question()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Content { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public bool IsDeleted { get; set; }
        public override string ToString()
        {
            return $"{Content}";
        }
        public int CompareTo(object obj)
        {
            return (obj as Question).Content.CompareTo(Content);
        }
    }
}
