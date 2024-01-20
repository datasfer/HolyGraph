using HolyGraph.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace HolyGraph.API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users {get;set;}
}
