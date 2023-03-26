using Kitaplar.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Kitaplar;

public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(
            builder =>
            {
                builder
                .HasIndex(p => new { p.Name })
                .IsUnique(true);

                builder
                .Property(p => p.Name)
                .HasMaxLength(450)
                .IsRequired(true);
                builder
                .HasMany(p => p.Books)
                .WithOne(p => p.Genre)
                .HasForeignKey(p => p.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            });
        modelBuilder.Entity<Book>(
            builder =>
            {
                builder
                .HasIndex(p => new { p.Name })
                .IsUnique(false);

                builder
                .Property(p => p.Name)
                .HasMaxLength(450)
                .IsRequired(true);


                builder
                .Property(p => p.Image)
                .IsUnicode(false);

            });
        base.OnModelCreating(modelBuilder);
    }
    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }
}
