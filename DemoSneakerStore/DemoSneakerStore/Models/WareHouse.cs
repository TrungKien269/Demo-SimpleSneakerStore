using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class WareHouse
    {
        public WareHouse()
        {
            ImportBill = new List<ImportBill>();
        }

        [Key]
        public int? Serial { get; set; }
        [Column("SupplierID")]
        public int SupplierId { get; set; }
        [Column(TypeName = "datetime"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        [ForeignKey("SupplierId")]
        [InverseProperty("WareHouse")]
        public Supplier Supplier { get; set; }
        [InverseProperty("ImSerialNavigation")]
        public List<ImportBill> ImportBill { get; set; }
    }
}
