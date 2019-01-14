using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            WareHouse = new HashSet<WareHouse>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(50)]
        public string MobileNumber { get; set; }
        [StringLength(100)]
        public string Address { get; set; }

        [InverseProperty("Supplier")]
        public ICollection<WareHouse> WareHouse { get; set; }
    }
}
