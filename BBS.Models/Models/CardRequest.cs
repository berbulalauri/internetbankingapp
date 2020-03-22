using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Models.Models
{
    public class CardRequest : ISoftDeletable
    {
        public CardRequest()
        {
            CardRequestHistories = new HashSet<CardRequestHistory>();
        }

        public int Id { get; set; }
        public CardRequestStatus CardRequestStatus { get; set; }
        public int UserId { get; set; }
        public int? CardId { get; set; }
        public string CompanyName { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsHidden { get; set; }
        public bool? IsActive { get; set; }
        public virtual ICollection<CardRequestHistory> CardRequestHistories { get; set; }
        public virtual User User { get; set; }
        public virtual Card Card { get; set; }
    }
}
