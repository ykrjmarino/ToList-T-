using backend.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.data.configurations;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{ 
  public void Configure(EntityTypeBuilder<Todo> builder)
  { 
    builder.HasKey(t => t.TodoId); //HasKey: defining the PrimaryKey

    builder.Property(t => t.Title)
      .IsRequired();
    builder.Property(t => t.Description);

    //configuring realationship to User (Model) 
    builder 
      .HasOne(t => t.User) //Todo has one owner User 
      .WithMany(u => u.Todos) //User has many Todos 
      .HasForeignKey(t => t.UserId); //use Todo.UserId as the FK 
  } 
} 
 
//UserConfiguration --> responsible for configuring Models for EF Core