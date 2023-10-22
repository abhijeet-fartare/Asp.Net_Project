using Microsoft.AspNetCore.Identity;

namespace IdentityEntity
{
    public class User: IdentityUser<Guid>
    {
        //Additional property user class
        public string? Name { get; set; }    
    }
}