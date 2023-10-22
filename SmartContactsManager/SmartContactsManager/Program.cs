using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;
using RepositoryContracts;
using Repository;
using IdentityEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IContactsRepository, ContactsRepository>();

//Service added into Ioc Container
builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.AddDbContext<ContactsDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString
        ("ConnectionKey"));
});

//add indentity in this project with DbContext
builder.Services.AddIdentity<User, Role>(Options =>
{
    Options.Password.RequireNonAlphanumeric = false;
    Options.Password.RequireDigit= false;
    Options.Password.RequireLowercase= false;
    Options.Password.RequireUppercase= false;
}).
    AddEntityFrameworkStores<ContactsDbContext>().
    AddUserStore<UserStore<User,Role, ContactsDbContext, Guid>>().
    AddRoleStore<RoleStore<Role, ContactsDbContext, Guid>>();
//Configured repository(IdentityStore) with web container


//user must be login for all action methods
builder.Services.AddAuthorization(Options =>
{
    Options.FallbackPolicy = new AuthorizationPolicyBuilder().
    RequireAuthenticatedUser().Build();
});

//If user is not login then it must at this page
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Home";
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();//identify action method 
app.UseAuthentication();//insure user is login or not
app.UseAuthorization();// insure user has permission or not 
app.MapControllers();
app.Run();
