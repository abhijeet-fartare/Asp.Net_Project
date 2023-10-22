using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Dto
{
    public class ContactUpdateRequest
    {
        public Guid ContactId { get; set; }

        [Required(ErrorMessage = "Pesron Name can not be blank.")]
        public string? Name { get; set; }
        [EmailAddress(ErrorMessage = "Enter valid email Id.")]
        public string? Email { get; set; }
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public GenderOptions? Gender { get; set; }

        //Convert Current Object of AddContact in Object of Contact
        public Contact ToContact()
        {
            return new Contact()
            {
                ContactId = ContactId,
                Name = Name,
                Email = Email,
                Phone = Phone,
                Description = Description,
                Gender = Gender.ToString(),
            };
        }
    }
}
