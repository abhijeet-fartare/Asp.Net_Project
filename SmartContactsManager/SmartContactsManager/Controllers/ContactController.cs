using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.Dto;
using ServiceContracts.Enums;
using Services;

namespace SmartContactManager.Controllers
{
    [Route("contact")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        //constructor
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [Route("/")]
		[Route("index")]
        public IActionResult Index(string SearchBy, string SearchString , 
            string sortBy = nameof(ContactResponse.Name), SortOrderOptions sortOrder = SortOrderOptions.DESC)
        {
            ViewBag.SearchFields = new Dictionary<String, String>()
            {
                {nameof (ContactResponse.Name), "Name"},
                {nameof(ContactResponse.Email),"Email" },
                {nameof(ContactResponse.Phone ),"Phone" },
                {nameof (ContactResponse.Description), "Description"},
                {nameof(ContactResponse.Gender),"Gender" }
             };

            //filterd 
            List<ContactResponse> contacts = _contactService.GetFilteredContact(SearchBy,SearchString);
            ViewBag.CurrentSearchBy = SearchBy;
            ViewBag.CurrentSearchString = SearchString;

            //sorted
            List<ContactResponse> sortedcontacts = _contactService.GetSortedContact(contacts, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();
            return View(sortedcontacts);
        }

        //Executes when the Contact clicks on "Create Person" hyperlink (while opening the create view)
        [Route("create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
		[Route("create")]
		public IActionResult Create(ContactAddRequest contactAddRequest)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				return View();
			}

			//call the service method
			ContactResponse contactResponse = _contactService.AddContact(contactAddRequest);

            //navigate to Index() action method (it makes another get request to "Contact/index"
            return RedirectToAction("Index", "Contact");
		}

        [Route("[action]/{contactId}")]
        public IActionResult Edit(Guid contactId)
        {
            ContactResponse? contactResponse = _contactService.GetContactById(contactId);
            if (contactResponse == null)
            {
                return RedirectToAction("Index");
            }

            ContactUpdateRequest contactUpdateRequest = contactResponse.ToContactUpdateRequest();

            return View(contactUpdateRequest);
        }

        [HttpPost]
        [Route("[action]/{contactId}")]
        public IActionResult Edit(ContactUpdateRequest contactUpdateRequest)
        {
           ContactResponse? contactResponse = _contactService.GetContactById(contactUpdateRequest.ContactId);
           
            if (contactResponse == null)
            {
                return RedirectToAction("Index");
            }
            
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            _contactService.UpdateContact(contactUpdateRequest);

            return RedirectToAction("Index");

        }

        [Route("[action]/{contactId}")]
        public IActionResult Delete(Guid contactId)
        {
            ContactResponse? contactResponse = _contactService.GetContactById(contactId);

            _contactService.DeleteContact(contactResponse.ContactId);

            return RedirectToAction("Index");
        }
    }
}
