using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Models.Models
{
    public class AccountProperty : ISoftDeletable
    {
        public int Id { get; set; }
        public string MajorIndustryIdentifier { get; set; }
        public string BankIdentificationNumber { get; set; }
        public bool IsDeleted { get; set; }
    }
}
