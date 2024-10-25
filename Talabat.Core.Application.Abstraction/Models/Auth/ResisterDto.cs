using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Application.Abstraction.Models.Auth
{
    public class RegisterDto
    {
        public required string DisplayName { get; set; }
        public required string UserName { get; set; }

        public required string Email { get; set; }

        //[RegularExpression("^(?=^.{6,10}$)(?=.\\d)(?=.[a-z])(?=.[A-Z])(?=.[!@#$%^&()_+{}\\[\\]:;<>,.?~\\-\\/]).$",
        // ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non-alphanumeric and at least 6 characters")]
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }

    }
}
