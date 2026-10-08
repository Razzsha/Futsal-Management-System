using Futsal_Management.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAO.IDAO
{
    public interface IUserGroupDao
    {
        Task<bool> ExistsByNameAsync(string name, int? excludeUserGroupId = null);
        Task<UserGroup?> GetByIdAsync(int id);
        Task<List<UserGroup>> GetAllAsync();
        Task<bool> HasUsersAsync(int UserGroupId);
        Task AddAsync(UserGroup userGroup);
        void Delete(UserGroup userGroup);
        Task SaveChangesAsync();
    }
}
