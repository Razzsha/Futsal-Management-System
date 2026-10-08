using Futsal_Management.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAO.IDAO
{
    public interface IUserDao
    {
        Task<bool> ExistsByEmailAsync(string email, int? excludeUserId = null);
        Task<bool> UserGroupExistsAsync(int UserGroupId);
        Task<User?> GetByIdAsync(int Id);
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        void Delete(User user);
        Task SaveChangesAsync();
    }
}