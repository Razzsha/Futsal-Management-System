using DAO.Data;
using DAO.IDAO;
using Futsal_Management.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAO.DAO
{
    public class UserGroupDao : IUserGroupDao
    {
        private readonly AppDbContext _context;
        public UserGroupDao(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByNameAsync(string name, int? excludeUserGroupId = null)
        {
            return await _context.UserGroups
                .AnyAsync(x => x.Name == name && 
                (!excludeUserGroupId.HasValue 
                || x.Id !=  excludeUserGroupId.Value));
        }

        public async Task<UserGroup?> GetByIdAsync(int id)
        {
            return await _context.UserGroups
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UserGroup>> GetAllAsync()
        {
            return await _context.UserGroups
                .AsNoTracking()
                .ToListAsync();
        } 

        public async Task<bool> HasUsersAsync(int UserGroupId)
        {
            return await _context.Users
                .AnyAsync(x => x.UserGroupId == UserGroupId);
        }

        public async Task AddAsync(UserGroup userGroup)
        {
            await _context.UserGroups.AddAsync(userGroup);
        }
        public void Delete(UserGroup userGroup)
        {
            _context.UserGroups.Remove(userGroup);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
