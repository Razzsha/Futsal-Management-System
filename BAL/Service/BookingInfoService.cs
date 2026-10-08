using DAO.IDAO;
using Futsal_Management.Domain.Enum;
using Futsal_Management.Domain.GenericResponse;
using Futsal_Management.Domain.Model;
using Futsal_Management.Domain.ViewModel;
using Futsal_Management.IService;

namespace Futsal_Management.Service
{
    public class BookingInfoService : IBookingInfoService
    {
        private readonly IBookingInfoDao _bookingInfoDao;

        public BookingInfoService(IBookingInfoDao bookingInfoDao)
        {
            _bookingInfoDao = bookingInfoDao;
        }

        public async Task<ResponseResult<BookingInfoDto>> CreateBooking(
            BookingInfoDto bookingDto)
        {
            try
            {
                if (bookingDto == null)
                {
                    return ResponseResult<BookingInfoDto>.Failure(
                        null,
                        "Please fill all details");
                }

                var userExists =
                    await _bookingInfoDao.UserExistsAsync(
                        bookingDto.UserId);

                if (!userExists)
                {
                    return ResponseResult<BookingInfoDto>.Failure(
                        null,
                        "User not found");
                }

                var arenaHour =
                    await _bookingInfoDao.GetArenaHourAsync(
                        bookingDto.ArenaId);

                if (arenaHour == null)
                {
                    return ResponseResult<BookingInfoDto>.Failure(
                        null,
                        "Arena not found");
                }

                bookingDto.TotalCost = arenaHour.Value;

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
                    Remarks = bookingDto.Remarks
                };

                await _bookingInfoDao.AddAsync(booking);
                await _bookingInfoDao.SaveChangesAsync();

                bookingDto.Id = booking.Id;

                return ResponseResult<BookingInfoDto>.Success(
                    bookingDto,
                    "Booking created successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<BookingInfoDto>.Failure(
                    null,
                    ex.Message);
            }
        }

        public async Task<ResponseResult<List<BookingInfoDto>>> GetAllBookings()
        {
            try
            {
                var bookings =
                    await _bookingInfoDao.GetAllAsync();

                var result = bookings
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
                    .ToList();

                return ResponseResult<List<BookingInfoDto>>.Success(
                    result,
                    "Bookings retrieved successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<List<BookingInfoDto>>.Failure(
                    null,
                    ex.Message);
            }
        }

        public async Task<ResponseResult<BookingInfoDto>> GetBookingById(
            int id)
        {
            try
            {
                var booking =
                    await _bookingInfoDao.GetByIdAsync(id);

                if (booking == null)
                {
                    return ResponseResult<BookingInfoDto>.Failure(
                        null,
                        "Booking not found");
                }

                var result = new BookingInfoDto
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
                    result,
                    "Booking retrieved successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<BookingInfoDto>.Failure(
                    null,
                    ex.Message);
            }
        }

        public async Task<ResponseResult<BookingInfoDto>> UpdateBookingStatus(
            int id,
            BookingStatus status)
        {
            try
            {
                var booking =
                    await _bookingInfoDao.GetByIdAsync(id);

                if (booking == null)
                {
                    return ResponseResult<BookingInfoDto>.Failure(
                        null,
                        "Booking not found");
                }

                booking.Status = status;

                await _bookingInfoDao.SaveChangesAsync();

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
            try
            {
                var booking =
                    await _bookingInfoDao.GetByIdAsync(id);

                if (booking == null)
                {
                    return ResponseResult<bool>.Failure(
                        false,
                        "Booking not found");
                }

                _bookingInfoDao.Delete(booking);

                await _bookingInfoDao.SaveChangesAsync();

                return ResponseResult<bool>.Success(
                    true,
                    "Booking deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<bool>.Failure(
                    false,
                    ex.Message);
            }
        }
    }
}