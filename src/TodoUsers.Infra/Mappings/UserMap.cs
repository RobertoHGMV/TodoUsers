using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoUsers.Domain.Models.Users;

namespace TodoUsers.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(c => c.Id);

            builder.OwnsOne(c => c.Name, x => 
            {
                x.Property(l => l.FirstName).HasMaxLength(60).HasColumnName("FirstName");
                x.Property(l => l.LastName).HasMaxLength(60).HasColumnName("LastName");
            });
            builder.OwnsOne(c => c.Login, x =>
            {
                x.Property(l => l.UserName).HasMaxLength(20).HasColumnName("UserName");
                x.Property(l => l.Password).HasMaxLength(32).IsFixedLength().HasColumnName("Password");
                x.Ignore(l => l.ConfirmPassword);
            });

            builder.OwnsOne(c => c.Email).Property(e => e.Address).HasMaxLength(160).HasColumnName("Email");
            builder.Ignore(c => c.Valid);
        }
    }
}
