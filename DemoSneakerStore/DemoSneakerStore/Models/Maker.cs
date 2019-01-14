using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class Maker
    {
        public Maker()
        {
            Shoes = new HashSet<Shoes>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }

        [InverseProperty("Maker")]
        public ICollection<Shoes> Shoes { get; set; }
    }
}
