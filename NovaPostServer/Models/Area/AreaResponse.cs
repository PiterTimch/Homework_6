using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaPostServer.Models.Area
{
    public class AreaResponse : StandartResponse<AreaItemResponse>
    {
        public List<object>? Info { get; set; }
    }
}
