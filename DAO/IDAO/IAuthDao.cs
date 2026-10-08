using Futsal_Management.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAO.IDAO
{
    public interface IAuthDao
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}
