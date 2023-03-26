using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static Kitaplar.Data.User;

namespace Kitaplar.Data;

public enum Genders
{
    Male, Female
}



public class User : IdentityUser<int>
{

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public  Genders? Gender { get; set; }
    public bool Enabled { get; set; } = true;

    [NotMapped]
    public string Name => $"{FirstName} {LastName}";


    public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();
}


public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasIndex(p => new { p.FirstName, p.LastName })
            .IsUnique(false);

        builder
            .Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(450);

        builder
            .Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(450);
        builder
            .HasMany(p => p.Roles)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
