using Entities;

namespace RepositoryContracts
{
    public interface IContactsRepository
    {
        public Contact AddContact(Contact contact);
        public Contact GetContactByContactId(Guid? contactId);
        public List<Contact> GetAllContacts();
        public Contact UpdateContact(Contact? contact);
        public bool DeleteContactByContactId(Guid? contactId);
    }
}