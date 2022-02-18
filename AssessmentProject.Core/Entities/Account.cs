using System;
using System.ComponentModel.DataAnnotations;

namespace AssessmentProject.Core.Entities
{
    public class Account
    {
        public int Id { get; set; }

        [Required,StringLength(128)]
        public string CompanyName { get; set; }
        public string Website { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
