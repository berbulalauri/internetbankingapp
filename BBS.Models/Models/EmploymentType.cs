using System.Collections.Generic;

namespace BBS.Models.Models
{
    public partial class EmploymentType : ISoftDeletable
    {
        public EmploymentType()
        {
            Employments = new HashSet<Employment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employment> Employments { get; set; }
        public bool IsDeleted { get; set; }
    }
}
