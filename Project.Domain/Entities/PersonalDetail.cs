using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.ComponentModel.DataAnnotations;


namespace Project.Domain.Entities
{
   

public class PersonalDetail
    {
        [Required]
        public Guid Code { get; set; }  // Primary key is Code, not Id
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [StringLength(13)]
        public string IDNumber { get; set; } = string.Empty;
        [Required]
        public string AccountNumber { get; set; } = string.Empty;



        // Navigation properties
        //public virtual ICollection<Account> Accounts { get; set; }
        //public virtual ICollection<Transactions> Transactions { get; set; }


    }
}
