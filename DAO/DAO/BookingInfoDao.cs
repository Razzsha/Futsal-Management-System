using DAO.Data;
using DAO.IDAO;
using Futsal_Management.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAO.DAO
{
    public class BookingInfoDao : IBookingInfoDao
    {
        private readonly AppDbContext _context;

        public BookingInfoDao(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users
                .AnyAsync(x => x.Id == userId);
        }

        public async Task<decimal?> GetArenaHourAsync(int arenaId)
        {
            return await _context.Arenas
                .Where(x => x.Id ==  arenaId)
                .Select(x => (decimal?)x.Hour)
                .FirstOrDefaultAsync();
        }

        public async Task<BookingInfo?> GetByIdAsync(int id)
        {
            return await _context.BookingInfos
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<BookingInfo>> GetAllAsync()
        {
            return await _context.BookingInfos
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(BookingInfo booking)
        {
            await _context.BookingInfos.AddAsync(booking);
        }

        public void Delete(BookingInfo booking)
        {
            _context.BookingInfos.Remove(booking);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
