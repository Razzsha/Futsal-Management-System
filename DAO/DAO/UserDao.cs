using DAO.Data;
using DAO.IDAO;
using Futsal_Management.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAO.DAO
{
    public class UserDao : IUserDao
    {
        private readonly AppDbContext _context;

        public UserDao(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByEmailAsync(string email, int? excludeUserId = null)
        {
            return await _context.Users
                .AnyAsync(x => x.Email == email && (!excludeUserId.HasValue || x.Id != excludeUserId.Value));
        }

        public async Task<bool> UserGroupExistsAsync(int UserGroupId)
        {
            return await _context.UserGroups
                .AnyAsync(x => x.Id == UserGroupId);
        }

        public async Task<User?> GetByIdAsync(int Id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void  Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
