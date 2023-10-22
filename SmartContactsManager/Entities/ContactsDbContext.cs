using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IdentityEntity;

namespace Entities
{
    public class ContactsDbContext : IdentityDbContext<User,Role,Guid>
    {
        public ContactsDbContext (DbContextOptions options) : base(options) { }

        public DbSet<Contact> ContactTable { set; get; }

        private List<Contact> _contact = new List<Contact>()
        {
            new Contact() { ContactId = Guid.NewGuid(), Name = "Sonu", Email = "sonu@gmail.com", Phone = "7057222817", Description = "Student", Gender = "FEMALE" },
            new Contact() { ContactId = Guid.NewGuid(), Name = "Raj", Email = "raj@gmail.com", Phone = "8557222817", Description = "Teacher", Gender = "MALE" },
            new Contact() { ContactId = Guid.NewGuid(), Name = "Ketan", Email = "ketan@gmail.com", Phone = "9057225817", Description = "Actor", Gender = "MALE" },
            new Contact() { ContactId = Guid.NewGuid(), Name = "Abhijeet", Email = "abhijeet@gmail.com", Phone = "8557225817", Description = "Actor", Gender = "MALE" }
        };

        //bind DbSet to table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>().ToTable("ContactTable");

            //seed to ContactTable 
            modelBuilder.Entity<Contact>().HasData(_contact);
        }
    }
}
