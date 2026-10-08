using Futsal_Management.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAO.IDAO
{
    public interface IBookingInfoDao
    {
        Task<bool> UserExistsAsync(int userId);
        Task<decimal?> GetArenaHourAsync(int arenaId);
        Task<BookingInfo?> GetByIdAsync(int id);
        Task<List<BookingInfo>> GetAllAsync();
        Task AddAsync(BookingInfo booking);
        void Delete(BookingInfo booking);
        Task SaveChangesAsync();
    }
}
