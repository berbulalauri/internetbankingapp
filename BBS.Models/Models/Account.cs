using System;
using System.Collections.Generic;

namespace BBS.Models.Models
{
    public partial class Account: ISoftDeletable, IComparable
    {
        public Account()
        {
            DepositAccountForInterests = new HashSet<Deposit>();
            DepositAccountToTransfers = new HashSet<Deposit>();
            SendTransactions = new HashSet<Transactions>();
            RecievedTransactions = new HashSet<Transactions>();
            AccruedLoanRequests = new HashSet<LoanRequest>();
            TransferedLoanRequests = new HashSet<LoanRequest>();
            Cards = new HashSet<Card>();
            Services = new HashSet<Service>();
            Loans = new HashSet<Loan>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int CurrencyId { get; set; }
        public int UserId { get; set; }
        public DateTime RegDate { get; set; }
        public string Status { get; set; }
        public decimal? Balance { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Deposit> DepositAccountForInterests { get; set; }
        public virtual ICollection<Deposit> DepositAccountToTransfers { get; set; }
        public virtual ICollection<Transactions> SendTransactions { get; set; }
        public virtual ICollection<Transactions> RecievedTransactions { get; set; }
        public virtual ICollection<LoanRequest> AccruedLoanRequests { get; set; }
        public virtual ICollection<LoanRequest> TransferedLoanRequests { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
        public bool IsDeleted { get; set; }

        public int CompareTo(object obj)
        {
            return (obj as Account).Name.CompareTo(Name);
        }
    }
}
