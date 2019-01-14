using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class Image
    {
        [Column("ImageID")]
        public float ImageId { get; set; }
        [Column("ShoesID")]
        public int ShoesId { get; set; }
        [Required]
        public string Link { get; set; }

        [ForeignKey("ShoesId")]
        [InverseProperty("Image")]
        public Shoes Shoes { get; set; }
    }
}
