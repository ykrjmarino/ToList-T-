using backend.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.data.configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(u => u.UserId); //HasKey: defining the PrimaryKey

    builder.Property(u => u.Username)
      .IsRequired();

    builder.Property(u => u.Email)
      .IsRequired();

    builder.Property(u => u.PasswordHash)
      .IsRequired();

    builder.Property(u => u.Bio)
      .HasMaxLength(500);

    builder.Property(u => u.Role)
      .IsRequired();
  }
}

//UserConfiguration --> responsible for configuring Models for EF Core