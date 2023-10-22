using Microsoft.AspNetCore.Identity;

namespace IdentityEntity
{
    public class Role: IdentityRole<Guid>
    {
		//Additional property role class
		public string? RoleName { set; get; }
    }
}