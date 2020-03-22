using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Models.Models
{
    public class DepositTerm : ISoftDeletable, IComparable
    {
        public DepositTerm()
        {
            DepositTypes = new HashSet<DepositType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get ; set; }
        public virtual ICollection<DepositType> DepositTypes { get; set; }
        public virtual ICollection<Deposit> Deposit { get; set; }

        public override string ToString()
        {
            return Name;
        }
        public int CompareTo(object obj)
        {
            return (obj as DepositTerm).Name.CompareTo(Name);
        }
    }
}
