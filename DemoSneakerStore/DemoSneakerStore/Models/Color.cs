using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class Color
    {
        public Color()
        {
            ShoesColor = new HashSet<ShoesColor>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [InverseProperty("Color")]
        public ICollection<ShoesColor> ShoesColor { get; set; }
    }
}
