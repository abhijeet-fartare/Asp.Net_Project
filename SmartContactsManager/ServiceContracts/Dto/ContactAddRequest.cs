using System;
using ServiceContracts.Enums;
using ServiceContracts.Dto;
using Entities;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.Dto
{
    //Dto for insert new Contact
    public class ContactAddRequest
    {
        [Required(ErrorMessage ="Pesron Name can not be blank.")]
        public string? Name { get; set; }
        [EmailAddress(ErrorMessage ="Enter valid email Id.")]
        [DataType(DataType.EmailAddress)]//so <input asp-for=""> tag take only email not text
        public string? Email { get; set; }
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Mobile Number must have 10 digits.")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public GenderOptions? Gender { get; set; }

        //Convert Current Object of AddContact in Object of Contact
        public Contact ToContact()
        {
            return new Contact()
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
                Description = Description,
                Gender = Gender.ToString(),
            };
        }
    }
}
