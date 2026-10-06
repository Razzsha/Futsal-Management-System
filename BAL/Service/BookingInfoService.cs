using DAO.Data;
using Futsal_Management.Domain.ViewModel;
using Futsal_Management.Domain.Model;
using Futsal_Management.Domain.Enum;
using Futsal_Management.Domain.GenericResponse;
using Futsal_Management.IService;
using Microsoft.EntityFrameworkCore;

namespace Futsal_Management.Service
{
    public class BookingInfoService : IBookingInfoService
    {
        private readonly AppDbContext _context;

        public BookingInfoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<BookingInfoDto>> CreateBooking(BookingInfoDto bookingDto)
        {
            var userExists = await _context.Users
                .AnyAsync(x => x.Id == bookingDto.UserId);
            if (!userExists)
            {
                return ResponseResult<BookingInfoDto>.Failure(null, "User not Found");
            }

            var arena = await _context.Arenas
                .FirstOrDefaultAsync(x => x.Id == bookingDto.ArenaId);

            if (arena == null)
            {
                return ResponseResult<BookingInfoDto>.Failure(null, "Arene not found");
            }

            bookingDto.TotalCost = arena.Hour;

            var booking = new BookingInfo
            {
                UserId = bookingDto.UserId,
                ArenaId = bookingDto.ArenaId,
                RequestDate = bookingDto.RequestDate,
                RequestTime = bookingDto.RequestTime,
                Status = bookingDto.Status,
                FullName = bookingDto.FullName,
                ContactNo = bookingDto.ContactNo,
                Email = bookingDto.Email,
                TotalCost = bookingDto.TotalCost,
                Remarks = bookingDto.Remarks,
            };

            await _context.BookingInfos.AddAsync(booking);
            await _context.SaveChangesAsync();

            bookingDto.Id = booking.Id;
            return ResponseResult<BookingInfoDto>.Success(bookingDto, "Booking created sucessfully");
        }

        public async Task<ResponseResult<List<BookingInfoDto>>> GetAllBookings()
        {
            var bookings = await _context.BookingInfos
                .AsNoTracking()
                .Select(x => new BookingInfoDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    ArenaId = x.ArenaId,
                    RequestDate = x.RequestDate,
                    RequestTime = x.RequestTime,
                    Status = x.Status,
                    FullName = x.FullName,
                    ContactNo = x.ContactNo,
                    Email = x.Email,
                    TotalCost = x.TotalCost,
                    Remarks = x.Remarks
                })
                .ToListAsync();

            return ResponseResult<List<BookingInfoDto>>.Success(
                bookings,
                "Bookings retrieved successfully");
        }
        public async Task<ResponseResult<BookingInfoDto>> GetBookingById(int id)
        {
            var booking = await _context.BookingInfos
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new BookingInfoDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    ArenaId = x.ArenaId,
                    RequestDate = x.RequestDate,
                    RequestTime = x.RequestTime,
                    Status = x.Status,
                    FullName = x.FullName,
                    ContactNo = x.ContactNo,
                    Email = x.Email,
                    TotalCost = x.TotalCost,
                    Remarks = x.Remarks
                })
                .FirstOrDefaultAsync();

            if (booking == null)
            {
                return ResponseResult<BookingInfoDto>.Failure(null, "Booking not found");
            }
            return ResponseResult<BookingInfoDto>.Success(
        booking,
        "Booking retrieved successfully");
        }

        public async Task<ResponseResult<BookingInfoDto>> UpdateBookingStatus(
    int id,
    BookingStatus status)
        {
            try
            {
                var booking = await _context.BookingInfos
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (booking == null)
                {
                    return ResponseResult<BookingInfoDto>.Failure(
                        null,
                        "Booking not found");
                }

                booking.Status = status;

                await _context.SaveChangesAsync();

                var response = new BookingInfoDto
                {
                    Id = booking.Id,
                    UserId = booking.UserId,
                    ArenaId = booking.ArenaId,
                    RequestDate = booking.RequestDate,
                    RequestTime = booking.RequestTime,
                    Status = booking.Status,
                    FullName = booking.FullName,
                    ContactNo = booking.ContactNo,
                    Email = booking.Email,
                    TotalCost = booking.TotalCost,
                    Remarks = booking.Remarks
                };

                return ResponseResult<BookingInfoDto>.Success(
                    response,
                    "Booking status updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<BookingInfoDto>.Failure(
                    null,
                    $"Error updating booking status: {ex.Message}");
            }
        }
        public async Task<ResponseResult<bool>> DeleteBookings(int id)
        {
            var booking = await _context.BookingInfos
        .FirstOrDefaultAsync(x => x.Id == id);

            if (booking == null)
            {
                return ResponseResult<bool>.Failure(
                    false,
                    "Booking not found");
            }
            _context.BookingInfos.Remove(booking);
            await _context.SaveChangesAsync();

            return ResponseResult<bool>.Success(true, "Booking deleted successfully");

        }
    }
}
