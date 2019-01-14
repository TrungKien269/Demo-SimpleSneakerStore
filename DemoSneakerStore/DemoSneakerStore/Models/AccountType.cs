using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoSneakerStore.Models
{
    public partial class AccountType
    {
        public AccountType()
        {
            Account = new HashSet<Account>();
        }

        [Key]
        [Column("TypeID")]
        public int TypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }

        [InverseProperty("TypeNavigation")]
        public ICollection<Account> Account { get; set; }
    }
}
