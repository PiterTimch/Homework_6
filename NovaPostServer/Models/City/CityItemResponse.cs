﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaPostServer.Models.City
{
    public class CityItemResponse
    {
        public string Ref { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SettlementTypeDescription { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
    }
}
