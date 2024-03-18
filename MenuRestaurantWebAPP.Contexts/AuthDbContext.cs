﻿using MenuRestaurantWebAPP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace MenuRestaurantWebAPP.Contexts;

public class AuthDbContext : IdentityDbContext<AuthUser>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
        try
        {
            var databaseCreator =
                Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (databaseCreator != null)
            {
                if (!databaseCreator.CanConnect()) { databaseCreator.Create(); }
                if (!databaseCreator.HasTables()) { databaseCreator.CreateTables(); }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
