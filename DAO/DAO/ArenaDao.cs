using DAO.Data;
using DAO.IDAO;
using Futsal_Management.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DAO.DAO
{
    public class ArenaDao : IArenaDao
    {
        private readonly AppDbContext _context;

        public ArenaDao(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Arena?> GetByIdAsync(int id)
        {
            return await _context.Arenas
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Arena>> GetAllAsync()
        {
            return await _context.Arenas
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Arena arena)
        {
            await _context.Arenas.AddAsync(arena);
        }

        public void Delete(Arena arena)
        {
            _context.Arenas.Remove(arena);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}