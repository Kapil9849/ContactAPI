using Microsoft.EntityFrameworkCore;
using WebApi_AzureSQL.Models;

namespace WebApi_AzureSQL.Data
{
    public class ContactAPIDbContext : DbContext
    {
        public ContactAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
