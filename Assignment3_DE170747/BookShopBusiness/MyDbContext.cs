using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BookShopBusiness
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {

        }

        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Shippings> Shippings { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("BookStore"));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shippings>()
            .HasOne(s => s.UserOrder)
            .WithMany()
            .HasForeignKey(s => s.UserOrderId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Shippings>()
                .HasOne(s => s.UserApprove)
                .WithMany()
                .HasForeignKey(s => s.UserApproveId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Shippings>()
             .HasOne(s => s.Books)
             .WithMany()
             .HasForeignKey(s => s.BookId)
             .OnDelete(DeleteBehavior.Restrict);
            // Seed data for Category
            modelBuilder.Entity<Categories>().HasData(
                new Categories { CategoryID = 1, CategoryName = "Fiction" },
                new Categories { CategoryID = 2, CategoryName = "Non-fiction" }
            );

            // Seed data for Book
            modelBuilder.Entity<Books>().HasData(
                new Books { BookId = 1, BookName = "The Great Gatsby", Price = 10, CategoryId = 1 },
                new Books { BookId = 2, BookName = "Sapiens", Price = 12, CategoryId = 2 }
            );


            modelBuilder.Entity<Users>().HasData(
                new Users { UserId = 1, UserName = "Test1", Password = "password123" },
                new Users { UserId = 2, UserName = "Test2", Password = "password456" }
            );


            modelBuilder.Entity<Shippings>().HasData(
                new Shippings { ShippingId = 1, BookId = 1, DateBooking = DateTime.Now, DateShip = DateTime.Now.AddDays(2), LocationShip = "New York", UserOrderId = 1, UserApproveId = 2, Status="isApprove" },
                new Shippings { ShippingId = 2, BookId = 2, DateBooking = DateTime.Now, DateShip = DateTime.Now.AddDays(3), LocationShip = "California", UserOrderId = 2, UserApproveId = 1, Status = "isApprove" }
            );


            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
