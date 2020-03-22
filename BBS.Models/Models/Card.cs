using System;
using System.Collections.Generic;

namespace BBS.Models.Models
{
    public partial class Card : ISoftDeletable, IComparable
    {
        public Card()
        {
            Transactions = new HashSet<Transactions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public DateTime RegDate { get; set; }
        public bool Satus { get; set; }
        public int Cvc2 { get; set; }
        public DateTime ExpDate { get; set; }
        public string Status { get; set; }
        public bool IsDefault { get; set; }
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
        public bool IsDeleted { get; set; }
        public int? CardRequestId { get; set; }
        public CardRequest CardRequest { get; set; }
        public int CompareTo(object obj)
        {
            return (obj as Card).Name.CompareTo(Name);
        }

    }
}
