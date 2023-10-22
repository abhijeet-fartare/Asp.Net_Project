using System;
using System.Reflection.Metadata.Ecma335;
using ServiceContracts.Dto;
using ServiceContracts.Enums;

namespace ServiceContracts
{
    public interface IContactService
    {
        //add new Contact into list of Contact
        ContactResponse AddContact(ContactAddRequest? contactAddRequest);

        //return list of Contacts
        List<ContactResponse> GetAllContact();

        //return Contact matching by Id
        ContactResponse? GetContactById(Guid? contactId);

        //return Contact by search keyword in name/address property
        List<ContactResponse> GetFilteredContact(string property , string? keyword);

        //return sorted Contact 
        List<ContactResponse> GetSortedContact(List<ContactResponse> AllContact, string property, SortOrderOptions sortOrderOptions);

        //return updated Contact based on Contact Id
        ContactResponse? UpdateContact(ContactUpdateRequest? contactUpdateRequest);

        //delete Contact based on Contact Id
        bool DeleteContact(Guid? ContactId);
    }
}
