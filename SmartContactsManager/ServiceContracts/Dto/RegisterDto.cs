using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Dto
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="Name can't be blank")]
        public string Name { get; set; }

        [Required(ErrorMessage = "EmailAddress can't be blank")]
        [EmailAddress(ErrorMessage ="Enter proper email address")]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "IsEmailDupicate", controller: "Account", ErrorMessage = "Already Registerd")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber can't be blank")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Mobile Number must have 10 digits.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        [DataType(DataType.Password)]
         public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password not matching")]
        public string ConfirmPassword { get; set; }

        public UserTypeOptions UserType { get; set; } = UserTypeOptions.USER;

    }
}
