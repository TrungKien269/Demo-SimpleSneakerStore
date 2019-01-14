using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DemoSneakerStore.Models
{
    public partial class ExportBill
    {
        public int ExSerial { get; set; }
        [Column("ShoesID")]
        public int ShoesId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public float Total
        {
            get { return Price * Quantity; }
        }

        [ForeignKey("ExSerial")]
        [InverseProperty("ExportBill")]
        public Sell ExSerialNavigation { get; set; }
        [ForeignKey("ShoesId")]
        [InverseProperty("ExportBill")]
        [JsonIgnore]
        public Shoes Shoes { get; set; }
    }
}
