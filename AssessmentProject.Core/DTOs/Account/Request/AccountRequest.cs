using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssessmentProject.Core.DTOs.Account.Request
{
    public class AccountRequest
    {
        [Required, StringLength(128)]
        public string CompanyName { get; set; }

        public string Website { get; set; } 

    }
}
