using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MySql.Data.MySqlClient;
using WarfaceLineupsV1._1.Database.ModelConfiguration;
using WarfaceLineupsV1._1.Database.Models;

namespace WarfaceLineupsV1._1.Database;

public class Context : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Lineup> Lineups { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Maps> Maps { get; set; }
    

    public Context()
    {
        Database.EnsureCreated(); // создаем бд с новой схемой
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            Database = "warfacelineups",
            Port = 3306,
            UserID = "",
            Password = "",
        };
        optionsBuilder.UseMySQL(connectionString.ConnectionString)
            .LogTo(str => Debug.WriteLine(str), new[] { RelationalEventId.CommandExecuted })
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountsConfig());
        modelBuilder.ApplyConfiguration(new LineupsConfig());
        modelBuilder.ApplyConfiguration(new CommentsConfig());
    }
}