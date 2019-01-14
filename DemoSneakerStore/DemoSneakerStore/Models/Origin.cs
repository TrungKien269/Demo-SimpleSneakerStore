using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class Origin
    {
        public Origin()
        {
            Shoes = new HashSet<Shoes>();
        }

        [Key]
        [Column("CountryID")]
        public int CountryId { get; set; }
        [Required]
        [StringLength(100)]
        public string CountryName { get; set; }

        [InverseProperty("Country")]
        public ICollection<Shoes> Shoes { get; set; }
    }
}
