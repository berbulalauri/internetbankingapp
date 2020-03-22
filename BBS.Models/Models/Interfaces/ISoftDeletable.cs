using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Models.Models
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
