using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaPostServer.Models.City
{
    public class CityResponse : StandartResponse<CityItemResponse>
    {
        public Info? Info { get; set; }
    }
    public class Info
    {
        public int TotalCount { get; set; }
    }
}
