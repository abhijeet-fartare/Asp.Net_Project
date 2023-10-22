using ServiceContracts.Dto;
using Entities;
using ServiceContracts;
using Services.helpers;
using ServiceContracts.Enums;
using RepositoryContracts;
using System.Collections.Generic;

namespace Services
{
    public class ContactService : IContactService
    {
        //private field
        private readonly IContactsRepository _contactsRepository;
        
        //constructor
        public ContactService(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }
        public ContactResponse AddContact(ContactAddRequest? contactAddRequest)
        {
            //check if contactAddreqeuest is not null
            if (contactAddRequest == null)
            {
                throw new ArgumentNullException();
            }

            //check Contactname is not blank
            ValidationHelper.ModelValidation(contactAddRequest);

            //convert ContactAddRequest into Contact type
            Contact contact = contactAddRequest.ToContact();

            //generate ContactId
            contact.ContactId = Guid.NewGuid();

            //Add Contact in DbSet not in Database
            _contactsRepository.AddContact(contact);

            //conert Contact into Contact response
            ContactResponse contactResponse = contact.ToContactResponse();

            return contactResponse;
        }

        public List<ContactResponse> GetAllContact()
        {
            var contact = _contactsRepository.GetAllContacts();
            //Convert All Contacts from "Contact" type to "ContactResonse" type
            //Return All Contact 
            //result get in Enumeration so convert it into list
            return contact.Select(temp=>temp.ToContactResponse()).ToList();
        }

        public ContactResponse? GetContactById(Guid? contactId)
        {
            //check if ContactId is not null
            if (contactId == null)
            {
                return null;
            }

            Contact contact = _contactsRepository.GetContactByContactId(contactId);
            //conert Contact into Contact response
            ContactResponse contactResponse = contact.ToContactResponse();

            return contactResponse;
        }

        public List<ContactResponse> GetFilteredContact(string property, string? keyword)
        {
            List<ContactResponse> allcontacts = GetAllContact();
            List<ContactResponse> matchingContacts = allcontacts;

            if(property == null || keyword == null)
            {
                return matchingContacts;
            }

            switch (property)
            {
                case nameof(ContactResponse.Name):
                    matchingContacts = allcontacts.Where(c => 
                    c.Name.Contains(keyword,StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(ContactResponse.Email):
                    matchingContacts = allcontacts.Where(c =>
                    c.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(ContactResponse.Phone):
                    matchingContacts = allcontacts.Where(c =>
                    c.Phone.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(ContactResponse.Description):
                    matchingContacts = allcontacts.Where(c =>
                    c.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(ContactResponse.Gender):
                    matchingContacts = allcontacts.Where(c =>
                    c.Gender.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
            }
            return matchingContacts;
        }

        public List<ContactResponse> GetSortedContact(List<ContactResponse> AllContact, string property, SortOrderOptions sortOrder)
        {
            if(property == null)
            {
                return AllContact;
            }

            List<ContactResponse> sortedContact = (property, sortOrder)
                switch
            {
                (nameof(ContactResponse.Name),SortOrderOptions.ASC)
               =>AllContact.OrderBy(c=>c.Name,StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(ContactResponse.Name), SortOrderOptions.DESC)
                => AllContact.OrderByDescending(c => c.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(ContactResponse.Email), SortOrderOptions.ASC)
               => AllContact.OrderBy(c => c.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(ContactResponse.Email), SortOrderOptions.DESC)
               => AllContact.OrderByDescending(c => c.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(ContactResponse.Phone), SortOrderOptions.ASC)
                => AllContact.OrderBy(c => c.Phone, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(ContactResponse.Phone), SortOrderOptions.DESC)
               => AllContact.OrderByDescending(c => c.Phone, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(ContactResponse.Description), SortOrderOptions.ASC)
              => AllContact.OrderBy(c => c.Description, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(ContactResponse.Description), SortOrderOptions.DESC)
               => AllContact.OrderByDescending(c => c.Description, StringComparer.OrdinalIgnoreCase).ToList(),
                
                (nameof(ContactResponse.Gender), SortOrderOptions.ASC)
               => AllContact.OrderBy(c => c.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(ContactResponse.Gender), SortOrderOptions.DESC)
               => AllContact.OrderByDescending(c => c.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

            };
            return sortedContact;
        }

        public ContactResponse? UpdateContact(ContactUpdateRequest? contactUpdateRequest)
        {
            if(contactUpdateRequest == null)
            {
                throw new ArgumentNullException();
            }

            //validation
            ValidationHelper.ModelValidation(contactUpdateRequest);

            //get matching Contact object ot update
            Contact? matchingContact = _contactsRepository.GetContactByContactId(contactUpdateRequest.ContactId);

            if (matchingContact == null)
            {
                throw new ArgumentException();
            }

            //update all details
            matchingContact.Name = contactUpdateRequest.Name;
            matchingContact.Email = contactUpdateRequest.Email;   
            matchingContact.Phone = contactUpdateRequest.Phone;
            matchingContact.Description = contactUpdateRequest.Description;
            matchingContact.Gender = contactUpdateRequest.Gender.ToString();

            _contactsRepository.UpdateContact(matchingContact);

            return matchingContact.ToContactResponse();
        }

        public bool DeleteContact(Guid? contactId)
        {
            if(contactId == null)
            {
                throw new ArgumentNullException();
            }

            Contact? contact = _contactsRepository.GetContactByContactId(contactId);

            if(contact == null)
            {
                return false;
            }

            _contactsRepository.DeleteContactByContactId(contactId);

            return true;
        }
    }
}
