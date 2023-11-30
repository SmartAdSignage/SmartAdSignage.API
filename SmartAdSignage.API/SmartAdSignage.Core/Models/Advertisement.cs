﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Models
{
    public class Advertisement : BaseEntity
    {
        public string? Title { get; set; }
        public string? Type { get; set; }
        public byte[]? File { get; set; }

        public string? UserId { get; set; }

        public User? User { get; set; }

        public ICollection<CampaignAdvertisement>? CampaignAdvertisements { get; set; }

        public ICollection<Queue>? Queues { get; set; }
    }
}
