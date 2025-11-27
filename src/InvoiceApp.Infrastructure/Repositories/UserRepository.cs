using InvoiceApp.Core.Entities;
using InvoiceApp.Core.Interfaces;
using InvoiceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Infrastructure.Repositories;

public class UserRepository(AppDbContext db) : IUserRepository
{
    public Task<User?> GetByUsernameAsync(string username) =>
        db.Users.FirstOrDefaultAsync(u => u.Username == username);

    public async Task AddAsync(User user)
    {
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
    }
}
