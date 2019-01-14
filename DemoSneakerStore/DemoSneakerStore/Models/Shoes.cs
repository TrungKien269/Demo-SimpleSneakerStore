using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class Shoes
    {
        public Shoes()
        {
            ExportBill = new List<ExportBill>();
            Image = new List<Image>();
            ImportBill = new List<ImportBill>();
            Price = new List<Price>();
            ShoesColor = new List<ShoesColor>();
            ShoesSize = new List<ShoesSize>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("MakerID")]
        public int MakerId { get; set; }
        [Column("CateID")]
        public int CateId { get; set; }
        [Column("CountryID")]
        public int CountryId { get; set; }
        [StringLength(100)]
        public string Designer { get; set; }
        [Column(TypeName = "date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }
        [DefaultValue(1)]
        public int Status { get; set; }

        [ForeignKey("CateId")]
        [InverseProperty("Shoes")]
        public Category Cate { get; set; }
        [ForeignKey("CountryId")]
        [InverseProperty("Shoes")]
        public Origin Country { get; set; }
        [ForeignKey("MakerId")]
        [InverseProperty("Shoes")]
        public Maker Maker { get; set; }
        [InverseProperty("Shoes")]
        public List<ExportBill> ExportBill { get; set; }
        [InverseProperty("Shoes")]
        public List<Image> Image { get; set; }
        [InverseProperty("Shoes")]
        public List<ImportBill> ImportBill { get; set; }
        [InverseProperty("Shoes")]
        public List<Price> Price { get; set; }
        [InverseProperty("Shoes")]
        public List<ShoesColor> ShoesColor { get; set; }
        [InverseProperty("Shoes")]
        public List<ShoesSize> ShoesSize { get; set; }
    }
}
