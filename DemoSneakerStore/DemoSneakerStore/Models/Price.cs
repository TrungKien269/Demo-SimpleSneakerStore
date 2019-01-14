using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class Price
    {
        [Column("ShoesID")]
        public int ShoesId { get; set; }
        [Column("Price")]
        public float ShoesPrice { get; set; }

        [ForeignKey("ShoesId")]
        [InverseProperty("Price")]
        public Shoes Shoes { get; set; }
    }
}
