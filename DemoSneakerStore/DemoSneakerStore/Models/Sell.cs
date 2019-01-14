using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DemoSneakerStore.Models
{
    public partial class Sell
    {
        public Sell()
        {
            ExportBill = new List<ExportBill>();
        }

        [Key]
        public int Serial { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }

        [ForeignKey("UserName")]
        [InverseProperty("Sell")]
        public Account UserNameNavigation { get; set; }
        [InverseProperty("ExSerialNavigation")]
        public List<ExportBill> ExportBill { get; set; }
    }
}
