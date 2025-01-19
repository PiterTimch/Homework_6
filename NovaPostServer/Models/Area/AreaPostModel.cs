﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaPostServer.Models.Area
{
    public class AreaPostModel
    {
        public string ApiKey { get; set; } = string.Empty;
        public string ModelName { get; set; } = "Address";
        public string CalledMethod { get; set; } = "getAreas";

        public MethodProperties? MethodProperties { get; set; }
    }

    public class MethodProperties
    {
        
    }
}
