using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BBS.Models.Models
{
    public partial class Currency : ISoftDeletable, IComparable
    {
        public Currency()
        {
            Accounts = new HashSet<Account>();
            Deposits = new HashSet<Deposit>();
            DepositTypes = new HashSet<DepositType>();
            LoanTypes = new HashSet<LoanType>();
        }

        public int Id { get; set; }

        [DisplayName("Currency")]
        public string Name { get; set; }
        public decimal Rate { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Deposit> Deposits { get; set; }
        public virtual ICollection<DepositType> DepositTypes { get; set; }
        public virtual ICollection<LoanType> LoanTypes { get; set; }
        public bool IsDeleted { get; set; }
        public override string ToString()
        {
            return $"{Name}";

        }
        public int CompareTo(object obj)
        {
            return (obj as Currency).Name.CompareTo(Name);
        }
    }
}
