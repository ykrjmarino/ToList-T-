using backend.models;
using Microsoft.EntityFrameworkCore;

namespace backend.data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) //asp.net db configurations
{
  public DbSet<User> Users { get; set; } //tells EF: User -> Users table
  public DbSet<Todo> Todos { get; set; } //tells EF: Todo -> Todos table

  protected override void OnModelCreating(ModelBuilder modelBuilder){ 
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly); }

  //this tells EF Core to find configuration classes (ex: UserConfiguration.cs) and apply them.
}

//User is the MODEL                     -- SINGULAR
//Users is the database TABLE           -- PLURAL