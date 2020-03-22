using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.ConsumerServicesAPI.Repositories
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
