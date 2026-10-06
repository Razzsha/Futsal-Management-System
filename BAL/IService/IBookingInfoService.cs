using Futsal_Management.Domain.ViewModel;
using Futsal_Management.Domain.Enum;
using Futsal_Management.Domain.GenericResponse;

namespace Futsal_Management.IService
{
    public interface IBookingInfoService
    {
        Task<ResponseResult<BookingInfoDto>> CreateBooking(BookingInfoDto bookingDto);
        Task<ResponseResult<List<BookingInfoDto>>> GetAllBookings();
        Task<ResponseResult<BookingInfoDto>> GetBookingById(int id);
        Task<ResponseResult<BookingInfoDto>> UpdateBookingStatus( int id, BookingStatus status);
        Task<ResponseResult<bool>> DeleteBookings(int id);
    }
}
