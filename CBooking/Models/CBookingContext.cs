using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CBooking.Models
{
    public partial class CBookingContext : DbContext
    {
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Librarian> Librarian { get; set; }
        public virtual DbSet<Manage> Manage { get; set; }
        public virtual DbSet<User> User { get; set; }

        public CBookingContext(DbContextOptions<CBookingContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CBooking;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Bookname).IsRequired();

                entity.Property(e => e.Category).IsRequired();
            });

            modelBuilder.Entity<Librarian>(entity =>
            {
                entity.Property(e => e.BorrowDay).HasColumnName("Borrow_day");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Librarian)
                    .HasForeignKey(d => d.BookId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Librarian)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Manage>(entity =>
            {
                entity.Property(e => e.ManageDescription).IsRequired();

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Manage)
                    .HasForeignKey(d => d.BookId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Manage)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Username).IsRequired();
            });
        }
    }
}
