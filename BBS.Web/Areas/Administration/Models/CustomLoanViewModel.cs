using System.ComponentModel.DataAnnotations;

namespace BBS.Web.Areas.Administration.Models
{
    public class CustomLoanViewModel
    {
        [Required]
        public string Comment { get; set; }
    }
}
