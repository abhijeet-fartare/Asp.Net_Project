using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Contact
    {
        [Key]
        public Guid ContactId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public string? Gender { get; set; }

    }
}
