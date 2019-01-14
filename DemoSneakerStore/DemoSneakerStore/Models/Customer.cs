using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class Customer
    {
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        [StringLength(50)]
        public string MobileNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [ForeignKey("Username")]
        [InverseProperty("Customer")]
        public Account UsernameNavigation { get; set; }
    }
}
