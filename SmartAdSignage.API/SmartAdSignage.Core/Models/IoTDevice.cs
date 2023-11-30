using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Models
{
    public class IoTDevice : BaseEntity
    {
        public string? Name { get; set; }

        public string? Status { get; set; }

        public int? PanelId { get; set; }

        public Panel? Panel { get; set; }
    }
}
