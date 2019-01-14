using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class Account
    {
        public Account()
        {
            Customer = new List<Customer>();
            Sell = new HashSet<Sell>();
        }

        [Key]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        public int Type { get; set; }
        [DefaultValue(1)]
        public int Status { get; set; }

        [ForeignKey("Type")]
        [InverseProperty("Account")]
        public AccountType TypeNavigation { get; set; }
        [InverseProperty("UsernameNavigation")]
        public List<Customer> Customer { get; set; }
        [InverseProperty("UserNameNavigation")]
        public ICollection<Sell> Sell { get; set; }
    }
}
