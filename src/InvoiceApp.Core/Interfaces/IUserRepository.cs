using InvoiceApp.Core.Entities;

namespace InvoiceApp.Core.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
}
