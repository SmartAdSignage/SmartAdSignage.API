﻿using AutoMapper;
using SmartAdSignage.Core.DTOs.Panel.Responses;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Mappings
{
    public class PanelMappingProfile : Profile
    {
        public PanelMappingProfile()
        {
            CreateMap<Panel, PanelResponse>();
            /*CreateMap<CreatePanelRequest, Panel>();
            CreateMap<UpdatePanelRequest, Panel>();*/
        }
    }
}
