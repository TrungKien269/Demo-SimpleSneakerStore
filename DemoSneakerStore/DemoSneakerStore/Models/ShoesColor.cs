using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class ShoesColor
    {
        [Column("ShoesID")]
        public int ShoesId { get; set; }
        [Column("ColorID")]
        public int ColorId { get; set; }

        [ForeignKey("ColorId")]
        [InverseProperty("ShoesColor")]
        public Color Color { get; set; }
        [ForeignKey("ShoesId")]
        [InverseProperty("ShoesColor")]
        public Shoes Shoes { get; set; }
    }
}
