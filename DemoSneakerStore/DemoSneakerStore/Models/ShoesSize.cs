using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class ShoesSize
    {
        [Column("ShoesID")]
        public int ShoesId { get; set; }
        [Column("VNSize")]
        public float Vnsize { get; set; }

        [ForeignKey("ShoesId")]
        [InverseProperty("ShoesSize")]
        public Shoes Shoes { get; set; }
        [ForeignKey("Vnsize")]
        [InverseProperty("ShoesSize")]
        public Size VnsizeNavigation { get; set; }
    }
}
