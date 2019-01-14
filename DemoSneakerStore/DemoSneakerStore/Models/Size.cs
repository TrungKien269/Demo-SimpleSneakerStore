using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class Size
    {
        public Size()
        {
            ShoesSize = new HashSet<ShoesSize>();
        }

        [Key]
        [Column("VNSize")]
        public float Vnsize { get; set; }
        [Column("USSize")]
        public float Ussize { get; set; }
        [Column("UKSize")]
        public float Uksize { get; set; }

        [InverseProperty("VnsizeNavigation")]
        public ICollection<ShoesSize> ShoesSize { get; set; }
    }
}
