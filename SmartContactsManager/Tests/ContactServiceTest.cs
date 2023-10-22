using System;
using Xunit;
using ServiceContracts.Dto;
using Entities;
using ServiceContracts;
using Services;
using ServiceContracts.Enums;

namespace Tests
{
	public class ContactServiceTest
	{
		private readonly IContactService contactService;

		//
		public ContactServiceTest()
		{
			contactService = new ContactService();
		}

		#region AddContact

		//If ContactAddrequest is null
		[Fact]
		public void AddContact_null()
		{
			//Arrange
			ContactAddRequest contactAddRequest = null;

			//Assert
			Assert.Throws<ArgumentNullException>(() =>
			{
				//Acts
				contactService.AddContact(contactAddRequest);
			});
		}

		//if Contactname name is null
		[Fact]
		public void AddContactName_null()
		{
			//Arrange
			ContactAddRequest contactAddRequest = new ContactAddRequest() { Name = null };

			//Assert
			Assert.Throws<ArgumentException>(() =>
			{
				//Acts
				contactService.AddContact(contactAddRequest);
			});
		}

		//proper contact details so it has return object of response
		[Fact]
		public void AddContact()
		{
			//Arrange 
			ContactAddRequest contactAddRequest = new ContactAddRequest()
			{
				Name = "Abhijeet",
				Description = "Student",
				Email = "abhi@gmail.com",
				Phone = "7984461222",
				Gender = GenderOptions.MALE
			};

			//Assert
			ContactResponse contactResponse = contactService.AddContact(contactAddRequest);

			Assert.True(contactResponse.ContactId != null);
		}

		#endregion

		#region GetContact

		//If contact Id is null
		[Fact]
		public void GetContactById_Null()
		{
			//Arrange
			Guid? contactId = null;

			//Acts
			ContactResponse? contact = contactService.GetContactById(contactId);

			//Assert
			Assert.Null(contact);
		}

		//Get Contact by exist Id
		[Fact]
		public void GetContactById()
		{
			ContactAddRequest request = new ContactAddRequest() { Name = "Abhijeet" };

			ContactResponse contact_Added = contactService.AddContact(request);

			ContactResponse? contact_Geted = contactService.GetContactById(contact_Added.ContactId);

			Assert.Equal(contact_Added, contact_Geted);
		}

		#endregion

		#region GetAllContact

		//It will return empty list bydefault
		[Fact]
		public void GetAllContact_EmptyList()
		{
			List<ContactResponse> contacts = contactService.GetAllContact();

			Assert.Empty(contacts);
		}

		//Add some persons so get all of them
		[Fact]
		public void GetAllContact()
		{
			ContactAddRequest contactAddRequest1 = new ContactAddRequest()
			{
				Name = "Abhijeet",
				Description = "Student",
				Email = "abhi@gmail.com",
				Phone = "7984461222",
				Gender = GenderOptions.MALE
			};

			ContactAddRequest contactAddRequest2 = new ContactAddRequest()
			{
				Name = "Sonu",
				Description = "Employee",
				Email = "sonu@gmail.com",
				Phone = "9584461222",
				Gender = GenderOptions.FEMALE
			};

			//sample persons added in list
			List<ContactAddRequest> contactAddRequest = new List<ContactAddRequest>()
			{
				contactAddRequest1,
				contactAddRequest2
			};


			//list for geted persons from service
			List<ContactResponse> contactResponseFromService = new List<ContactResponse>();

			foreach (ContactAddRequest readContactAddRequest in contactAddRequest)
			{
				ContactResponse contactResponse = contactService.AddContact(readContactAddRequest);
				contactResponseFromService.Add(contactResponse);
			}

			//Acts
			List<ContactResponse> contacts = contactService.GetAllContact();

			//Assert
			//compare GetAllcontact list with contactResponse list
			foreach (ContactResponse contactResponse in contacts)
			{
				Assert.Contains(contactResponse, contactResponseFromService);
			}
		}

		#endregion

		#region GetFilterContact

		//It will return all contact for keyword is empty
		[Fact]
		public void GetfilteredContact_EmptyKeyword()
		{
			ContactAddRequest contactAddRequest1 = new ContactAddRequest()
			{
				Name = "Abhijeet",
				Description = "Student",
				Email = "abhi@gmail.com",
				Phone = "7984461222",
				Gender = GenderOptions.MALE
			};

			ContactAddRequest contactAddRequest2 = new ContactAddRequest()
			{
				Name = "Sonu",
				Description = "Employee",
				Email = "sonu@gmail.com",
				Phone = "9584461222",
				Gender = GenderOptions.FEMALE
			};

			//sample persons added in list
			List<ContactAddRequest> contactAddRequest = new List<ContactAddRequest>()
			{
				contactAddRequest1,
				contactAddRequest2
			};


			//list for geted contacts from service
			List<ContactResponse> contactResponseFromService = new List<ContactResponse>();

			foreach (ContactAddRequest readContactAddRequest in contactAddRequest)
			{
				ContactResponse contactResponse = contactService.AddContact(readContactAddRequest);
				contactResponseFromService.Add(contactResponse);
			}

			//Acts
			//Keyword is empty
			List<ContactResponse> contactsfromSearch = contactService.GetFilteredContact(nameof(ContactResponse.Name), "");

			//Assert
			//compare GetAllcontact list with contactResponse list
			foreach (ContactResponse contactResponse in contactsfromSearch)
			{
				Assert.Contains(contactResponse, contactResponseFromService);
			}
		}

		//It will return all contact for maching keyword 
		[Fact]
		public void GetfilteredContact()
		{
			ContactAddRequest contactAddRequest1 = new ContactAddRequest()
			{
				Name = "Abhijeet",
				Description = "Student",
				Email = "abhi@gmail.com",
				Phone = "7984461222",
				Gender = GenderOptions.MALE
			};

			ContactAddRequest contactAddRequest2 = new ContactAddRequest()
			{
				Name = "Ketan",
				Description = "Employee",
				Email = "sonu@gmail.com",
				Phone = "9584461222",
				Gender = GenderOptions.MALE
			};

			//sample persons added in list
			List<ContactAddRequest> contactAddRequest = new List<ContactAddRequest>()
			{
				contactAddRequest1,
				contactAddRequest2
			};


			//list for geted persons from service
			List<ContactResponse> contactResponseFromService = new List<ContactResponse>();

			foreach (ContactAddRequest readContactAddRequest in contactAddRequest)
			{
				ContactResponse contactResponse = contactService.AddContact(readContactAddRequest);
				contactResponseFromService.Add(contactResponse);
			}

			//Acts
			//Keyword is empty
			List<ContactResponse> contactsfromSearch = contactService.GetFilteredContact(nameof(ContactResponse.Name), "a");

			//Assert
			//compare GetAllContact list with ContactResponse list
			foreach (ContactResponse contactResponse in contactResponseFromService)
			{
				if (contactResponse.Name.Contains("a", StringComparison.OrdinalIgnoreCase))
				{
					Assert.Contains(contactResponse, contactsfromSearch);
				}
			}
		}

		#endregion

		#region GetSortedContact

		//when we sort it return personlist in desc
		[Fact]
		public void GetSortedContact()
		{
			ContactAddRequest contactAddRequest1 = new ContactAddRequest()
			{
				Name = "Ketan",
				Description = "Student",
				Email = "ketan@gmail.com",
				Phone = "7984461222",
				Gender = GenderOptions.MALE
			};

			ContactAddRequest contactAddRequest2 = new ContactAddRequest()
			{
				Name = "Abhijeet",
				Description = "Employee",
				Email = "abhi@gmail.com",
				Phone = "9584461222",
				Gender = GenderOptions.MALE
			};

			ContactAddRequest contactAddRequest3 = new ContactAddRequest()
			{
				Name = "Sagar",
				Description = "Employee",
				Email = "sagar@gmail.com",
				Phone = "9584461222",
				Gender = GenderOptions.MALE
			};

			//sample persons added in list
			List<ContactAddRequest> contactAddRequest = new List<ContactAddRequest>()
			{
				contactAddRequest1,
				contactAddRequest2
			};


			//list for geted persons from service
			List<ContactResponse> contactResponseFromAdd = new List<ContactResponse>();

			foreach (ContactAddRequest readContactAddRequest in contactAddRequest)
			{
				ContactResponse contactResponse = contactService.AddContact(readContactAddRequest);
				contactResponseFromAdd.Add(contactResponse);
			}

			//we sort our contactResponseFromAdd list which we expected
			contactResponseFromAdd = contactResponseFromAdd.OrderByDescending(x => x.Name).ToList();

			List<ContactResponse> contacts = contactService.GetAllContact();

			//Acts
			List<ContactResponse> contactsFromSort = contactService.GetSortedContact(contacts, nameof(ContactResponse.Name), SortOrderOptions.DESC);

			//Assert
			for (int i = 0; i < contactsFromSort.Count; i++)
			{
				Assert.Equal(contactResponseFromAdd[i], contactsFromSort[i]);
			}
		}
		#endregion

		#region UpdateContact

		[Fact]
		public void UpdateContact_null()
		{
			//Arrange
			ContactUpdateRequest contactUpdateRequest = null;

			//Assert
			Assert.Throws<ArgumentNullException>(() =>
			{
				//Acts
				contactService.UpdateContact(contactUpdateRequest);
			});
		}

		//invalid personId
		[Fact]
		public void UpdateContact()
		{
			//Arrange
			ContactAddRequest contactAddRequest = new ContactAddRequest()
			{
				Name = "Abhijeet",
				Description = "Student",
				Email = "abhi@gmail.com",
				Phone = "7984461222",
				Gender = GenderOptions.MALE
			};

			ContactResponse contactResponse = contactService.AddContact(contactAddRequest);

			ContactUpdateRequest contactUpdateRequest = contactResponse.ToContactUpdateRequest();

			contactUpdateRequest.Name = "Avinash";

			ContactResponse? contactResponsefromUpdate = contactService.UpdateContact(contactUpdateRequest);

			//retrive contact from list to check contact get updated or not 
			ContactResponse? contactResponsefromGet = contactService.GetContactById(contactResponsefromUpdate.ContactId);

			//Assert
			Assert.Equal(contactResponsefromUpdate, contactResponsefromGet);
		}

		#endregion

		#region DeleteContact
		[Fact]
		public void DeleteContact_InvalidId()
		{
			//Arrange
			ContactAddRequest contactAddRequest = new ContactAddRequest()
			{
				Name = "Abhijeet",
				Description = "Student",
				Email = "abhi@gmail.com",
				Phone = "7984461222",
				Gender = GenderOptions.MALE
			};

			ContactResponse contactResponse = contactService.AddContact(contactAddRequest);

			//Act
			bool isDeleted = contactService.DeleteContact(contactResponse.ContactId);

			//Assert
			Assert.True(isDeleted);
		}


		[Fact]
		public void DeleteContact_ValidId()
		{
			//Act
			bool isDeleted = contactService.DeleteContact(Guid.NewGuid());

			//Assert
			Assert.False(isDeleted);
		}

		#endregion
	}
}
