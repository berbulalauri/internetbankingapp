using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Web.Models
{
    public class CardRequestViewModel
    {
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string CompanyAddress { get; set; }
        [Required]
        [Display(Name = "Phone")]
        public string CompanyPhone { get; set; }
        [Display(Name = "City")]
        public City City { get; set; }

        public static implicit operator CardRequest(CardRequestViewModel cardRequestViewModel)
        {
            return new CardRequest
            {
                CompanyPhone = cardRequestViewModel.CompanyPhone,
                CompanyName = cardRequestViewModel.CompanyName,
                CompanyAddress = cardRequestViewModel.CompanyAddress,
                City = cardRequestViewModel.City,
                CardId = cardRequestViewModel.City.Id,
                CardRequestStatus = CardRequestStatus.New,
                CreatedAt = DateTime.Now,
            };
        }

    }
}
