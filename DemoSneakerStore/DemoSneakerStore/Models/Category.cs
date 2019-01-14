using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class Category
    {
        public Category()
        {
            Shoes = new HashSet<Shoes>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string Decription { get; set; }

        [InverseProperty("Cate")]
        public ICollection<Shoes> Shoes { get; set; }
    }
}
