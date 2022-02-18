using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssessmentProject.Core.DTOs.User.Request
{
    public class UserRequest
    {
        public int AccountId { get; set; }

        [Required, StringLength(128)]
        public string FirstName { get; set; }

        [StringLength(128)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
    }
}
