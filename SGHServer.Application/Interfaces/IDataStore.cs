using Microsoft.EntityFrameworkCore;
using SGHServer.Domain;

namespace SGHServer.Application.Interfaces;

public interface IDataStore
{
    public DbSet<User> Users { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken token);
}