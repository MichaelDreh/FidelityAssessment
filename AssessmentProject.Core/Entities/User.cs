using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssessmentProject.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int AccountId { get; set; }

        [Required, StringLength(128)]
        public string FirstName { get; set; }

        [StringLength(128)]
        public string LastName { get; set; }

        [Required, EmailAddress]      
        public string Email { get; set; }

        public DateTime DateCreated { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
    }
}
