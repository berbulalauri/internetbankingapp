using System.Collections.Generic;
using System;
using System.Collections;

namespace BBS.Models.Models
{
    public partial class Service : ISoftDeletable, IComparable
    {
        public Service()
        {
            Transactions = new HashSet<Transactions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal FixedFee { get; set; }
        public decimal PercentFee { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int? AccountId { get; set; }

        public virtual ServiceCategory Category { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
        public bool IsDeleted { get; set; }


        public int CompareTo(object obj)
        {
            return (obj as Service).Name.CompareTo(Name);
        }
    }
}
