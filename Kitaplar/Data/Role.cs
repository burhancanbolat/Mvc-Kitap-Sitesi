using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Kitaplar.Data;
   public class Role : IdentityRole<int>
{
     public int UserId { get; set; }
    public virtual User? User { get; set; }
  
}

   public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {

    }

}