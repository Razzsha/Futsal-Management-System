using Futsal_Management.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAO.IDAO
{
    public interface IArenaDao
    {
        Task<Arena?> GetByIdAsync(int id);
        Task<List<Arena>> GetAllAsync();
        Task AddAsync(Arena arena);
        void Delete(Arena arena);
        Task SaveChangesAsync();
    }
}
