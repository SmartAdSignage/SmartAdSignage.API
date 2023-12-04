using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Models
{
    public class Queue : BaseEntity
    {
        public int? PanelId { get; set; }
        public virtual Panel? Panel { get; set; }
        public int? AdvertisementId { get; set; }
        public virtual Advertisement? Advertisement { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
