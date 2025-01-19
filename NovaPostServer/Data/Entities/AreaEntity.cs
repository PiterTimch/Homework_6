using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaPostServer.Data.Entities
{
    [Table("tbl_areas")]
    public class AreaEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ref { get; set; } = String.Empty;

        [Required]
        [StringLength(50)]
        public string AreasCenter { get; set; } = String.Empty;

        [Required]
        [StringLength(255)]
        public string Description { get; set; } = String.Empty;

        public ICollection<CityEntity> Cities { get; set; } = new List<CityEntity>();
    }
}
