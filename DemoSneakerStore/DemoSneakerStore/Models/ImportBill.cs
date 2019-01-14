using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class ImportBill
    {
        public int ImSerial { get; set; }
        [Column("ShoesID")]
        public int ShoesId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        [ForeignKey("ImSerial")]
        [InverseProperty("ImportBill")]
        public WareHouse ImSerialNavigation { get; set; }
        [ForeignKey("ShoesId")]
        [InverseProperty("ImportBill")]
        public Shoes Shoes { get; set; }
    }
}
