using AssessmentProject.Core.DTOs.User.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentProject.Core.DTOs.Account.Response
{
    class AllAccountResponse
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }

        public string CompanyWebsite { get; set; }
    }

    public class AccountResponse
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }

        public string Website { get; set; }

        public List<UserResponse> Users { get; set; }
    }
}
