using BBS.Models.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBS.Models.Models
{
    public partial class Employment : ISoftDeletable
    {
        public Employment()
        {
            LoanRequests = new HashSet<LoanRequest>();
        }

        public int Id { get; set; }
        public int EmploymentTypeId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Employment")]
        [DateLessThanNow()]
        public DateTime DateOfEmployment { get; set; }

        [Display(Name = "Phone number in office")]
        public string OfficePhoneNumber { get; set; }

        public virtual EmploymentType EmploymentType { get; set; }
        public virtual ICollection<LoanRequest> LoanRequests { get; set; }
        public bool IsDeleted { get; set; }
    }
}
