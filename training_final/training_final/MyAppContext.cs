using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using training_final.Data;

namespace training_final
{
    public class MyAppContext : DbContext
    {
        public DbSet<Book> Book {  get; set; }

        public DbSet<Customer> Customer { get; set; }
        
        public DbSet<BorrowedBook> borrowedBooks { get; set; }
            
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-FAJC3VE\\SQLEXPRESS;Database=ApiDemo;" +
                "Trusted_Connection=True;trustservercertificate=true");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public class bookConfig : IEntityTypeConfiguration<Book>
        {
            public void Configure(EntityTypeBuilder<Book> builder)
            {

            }
        }

        public class customerConfig : IEntityTypeConfiguration<Customer>
        {
            public void Configure(EntityTypeBuilder<Customer> builder)
            {

            }
        }

        public class borrowedBookConfig : IEntityTypeConfiguration<BorrowedBook>
        {
            public void Configure(EntityTypeBuilder<BorrowedBook> builder)
            {
                builder.HasKey(i => new { i.Book_Name, i.Customer_Name });
            }
        }
    }
}
