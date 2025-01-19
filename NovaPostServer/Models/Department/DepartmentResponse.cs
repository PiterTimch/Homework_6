using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaPostServer.Models.Department
{
    public class DepartmentResponse : StandartResponse<DepartmentItemResponse>
    {
        public object? Info { get; set; }
    }
}
