using System.Collections.Generic;

namespace BBS.Models.Models
{
    public class InterestPaymentType : ISoftDeletable
    {
        public InterestPaymentType()
        {
            DepositTypes = new HashSet<DepositType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<DepositType> DepositTypes { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
