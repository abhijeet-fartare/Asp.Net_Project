using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System.Diagnostics.Contracts;

namespace Repository
{
    public class ContactsRepository : IContactsRepository
    {
        //private field
        private readonly ContactsDbContext _db;

        //constructor
        public ContactsRepository(ContactsDbContext contactsDbContext)
        {
            _db = contactsDbContext;
        }
        public Contact AddContact(Contact contact)
        {
            _db.ContactTable.Add(contact);
            _db.SaveChanges();

            return contact;
        }

        public bool DeleteContactByContactId(Guid? contactId)
        {
             Contact? contact = _db.ContactTable.FirstOrDefault(temp=>temp.ContactId == contactId);
            _db.ContactTable.Remove(contact);
            _db.SaveChanges();

            return true;
        }

        public List<Contact> GetAllContacts()
        {
            return _db.ContactTable.ToList();
		}

        public Contact GetContactByContactId(Guid? contactId)
        {
            return _db.ContactTable.
                FirstOrDefault(temp=>temp.ContactId == contactId);
        }

        public Contact UpdateContact(Contact? contact)
        {
            //get matching contact object ot update
            Contact? matchingContact = _db.ContactTable.FirstOrDefault(c => c.ContactId == contact.ContactId);

            if (matchingContact == null)
            {
                throw new ArgumentException();
            }

            //update all details
            matchingContact.Name = contact.Name;
            matchingContact.Email = contact.Email;
            matchingContact.Phone = contact.Phone;
            matchingContact.Description = contact.Description;
            matchingContact.Gender = contact.Gender.ToString();

            _db.SaveChanges();
            return matchingContact;
        }
    }
}