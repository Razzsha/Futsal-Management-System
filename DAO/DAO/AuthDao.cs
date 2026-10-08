using DAO.Data;
using DAO.IDAO;
using Futsal_Management.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAO.DAO
{
    public class AuthDao : IAuthDao
    {
        private readonly AppDbContext _context;
        public AuthDao(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
