using DocumentManagement_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DocumentManagement_API.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<CSDSDocumentEntity> CSDSDocument { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CSDSDocumentEntity>().
                HasKey(c => new
                {
                    c.PropertyId,
                    c.CaseNumber,
                    c.Filename
                });

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
