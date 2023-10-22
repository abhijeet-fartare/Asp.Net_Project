using IdentityEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Dto;
using ServiceContracts.Enums;
using SmartContactManager.Controllers;

namespace SmartContactsManager.Controllers
{
    [AllowAnonymous]//user can access this actions without login also
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly RoleManager<Role> _roleManager;

        public AccountController(UserManager<User> userManager, 
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if(!ModelState.IsValid)
            {
                return View(registerDto);
            }

            User user = new User()
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                PhoneNumber = registerDto.Phone,
                Name = registerDto.Name
            };

            //It is repository layer to perfome operation on dbcontext
            //It add model to dbset and calls savechanges method 
            //all this process happen internally so we call only Bussiness Logic
            await _userManager.CreateAsync(user,registerDto.Password);

            //Check status of radio button
            if (registerDto.UserType == UserTypeOptions.ADMIN)
            {
                //Create 'Admin' role
                if (await _roleManager.FindByNameAsync(UserTypeOptions.ADMIN.ToString()) is null)
                {
                    Role role = new Role() { Name = UserTypeOptions.ADMIN.ToString() };
                    await _roleManager.CreateAsync(role);
                }

                //Add the new user into 'Admin' role
                await _userManager.AddToRoleAsync(user, UserTypeOptions.ADMIN.ToString());
            }
            else
            {
                //Create User Role
                Role role = new Role() { Name = UserTypeOptions.USER.ToString() };
                await _roleManager.CreateAsync(role);
                
                //Add the new user into 'User' role
                await _userManager.AddToRoleAsync(user, UserTypeOptions.USER.ToString());
            }

            //If we want save cookiee even after close brower and access same without login when brower open
            //make isPersistent true
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction(nameof(ContactController.Index),"Contact");
            //contact/index

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.Email,loginDto.Password,isPersistent:false,lockoutOnFailure:false);
           
            if (result.Succeeded)
            {
                /*     For Admin Area
               User user = await _userManager.FindByEmailAsync(loginDto.Email);

               if (await _userManager.IsInRoleAsync(user, UserTypeOptions.ADMIN.ToString()))
               {
                   return RedirectToAction("Index", "Home", new { area = "Admin" });
               }
                */
                return RedirectToAction(nameof(ContactController.Index), "Contact");
				//contact/index
			}
            
            ModelState.AddModelError("Login", "Inalid Email or Password");
			return View(loginDto);
		}

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(ContactController.Index), "Contact");
            //contact/index
        }

        public async Task<IActionResult> IsEmailDupicate(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true); //valid
            }
            else
            {
                return Json(false); //invalid
            }
        }
    }
}
