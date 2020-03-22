using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BBS.Models.Models
{
    public partial class User : IdentityUser<int>, ISoftDeletable, IComparable
    {
        public User()
        {
            Accounts = new HashSet<Account>();
            Deposits = new HashSet<Deposit>();
            LoanRequests = new HashSet<LoanRequest>();
        }

        public string FirstName { get; set; }
        public string LatsName { get; set; }
        public string Phone { get; set; }
        public string PassportId { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public DateTime RegDate { get; set; }
        public string Status { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        public int CityId { get; set; }
        public int DefaultAccountId { get; set; }
        public bool AllowPhoneForLogin { get; set; }
        public virtual City City { get; set; }
        public virtual Account DefaultAccount { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Deposit> Deposits { get; set; }
        public virtual ICollection<LoanRequest> LoanRequests { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public bool IsDeleted { get; set; }
        public int CardRequestId { get; set; }
        public CardRequest CardRequest { get; set; }

        public override string ToString()
        {
            return $"{Email}";

        }
        public int CompareTo(object obj)
        {
            return (obj as User).FirstName.CompareTo(FirstName);
        }
    }
}
