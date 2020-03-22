using System.Collections.Generic;

namespace BBS.Models.Models
{
    public partial class ServiceCategory : ISoftDeletable
    {
        public ServiceCategory()
        {
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Service> Services { get; set; }
        public bool IsDeleted { get; set; }
    }
}
