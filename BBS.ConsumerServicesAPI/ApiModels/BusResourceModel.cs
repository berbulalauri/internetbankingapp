using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.ApiModels
{
    public class BusResourceModel
    {
        [Required]
        public int CountOfSeats { get; set; }
        [Required]
        public string Model { get; set; }
    }
}
