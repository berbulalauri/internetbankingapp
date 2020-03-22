
using System;

namespace BBS.Models.Models
{
    public class CardRequestHistory : ISoftDeletable
    {
        public int Id { get; set; }
        public int? CardRequestId { get; set; }
        public DateTime CreatedAt { get; set; }
        public CardRequestStatus RequestStatus { get; set; }
        public string Comment { get; set; }
        public bool IsDeleted { get; set; }
        public int? CardId { get; set; }
        public virtual CardRequest CardRequest { get; set; }
        public virtual Card Card { get; set; }
    }
}
