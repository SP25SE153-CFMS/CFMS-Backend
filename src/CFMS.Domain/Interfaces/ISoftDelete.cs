using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Domain.Interfaces
{
    public interface ISoftDelete
    {
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public DateTimeOffset? DeletedWhen { get; set; }
    }
}
