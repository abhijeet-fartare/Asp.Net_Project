using Entities;
using System;
using Entities;
using System.Data;
using ServiceContracts.Enums;

namespace ServiceContracts.Dto
{
    public class ContactResponse
    {
        public Guid ContactId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public string? Gender { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            ContactResponse contactResponse = (ContactResponse)obj;

            return this.ContactId == contactResponse.ContactId &&
                this.Name == contactResponse.Name &&
                this.Phone == contactResponse.Phone &&
                this.Description == contactResponse.Description &&
                this.Gender == contactResponse.Gender;
        }
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public ContactUpdateRequest ToContactUpdateRequest()
        {
            return new ContactUpdateRequest()
            {
                ContactId = ContactId, 
                Name = Name,
                Email = Email,
                Phone = Phone,
                Description = Description,
                Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender) 
                //string type into specific enum type
            };
        }
    }

    public static class ContactExtension
    {
        //extension method to convert Contact object to ContactResponse
        public static ContactResponse ToContactResponse(this Contact contact)
        {
            return new ContactResponse()
            {
                ContactId = contact.ContactId,
                Name = contact.Name,
                Email = contact.Email,
                Phone = contact.Phone,
                Description = contact.Description,
                Gender = contact.Gender,
            };
        }
    }

}
