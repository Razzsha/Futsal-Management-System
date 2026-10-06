using Futsal_Management.Domain.ViewModel;
using Futsal_Management.Domain.Enum;
using Futsal_Management.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Futsal_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingInfoController : ControllerBase
    {
        private readonly IBookingInfoService _bookingInfoService;

        public BookingInfoController(
            IBookingInfoService bookingInfoService)
        {
            _bookingInfoService = bookingInfoService;
        }

    //    [Authorize(
    //Roles = UserGroupRole.Customer + "," +
    //        UserGroupRole.Admin + "," +
    //        UserGroupRole.SuperAdmin)]
        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking(
            [FromBody] BookingInfoDto bookingInfoDto)
        {
            var result = await _bookingInfoService
                .CreateBooking(bookingInfoDto);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(
            Roles = UserGroupRole.SuperAdmin + "," +
                    UserGroupRole.Staff)]
        [HttpGet("GetAllBookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var result = await _bookingInfoService
                .GetAllBookings();

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }

        [Authorize(
            Roles = UserGroupRole.SuperAdmin + "," +
                    UserGroupRole.Staff)]
        [HttpGet("GetBookingById/{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var result = await _bookingInfoService
                .GetBookingById(id);

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }

        [Authorize(
            Roles = UserGroupRole.SuperAdmin + "," +
                    UserGroupRole.Admin)]
        [HttpPut("UpdateBookingStatus/{id}")]
        public async Task<IActionResult> UpdateBookingStatus(
            int id,
            [FromBody] BookingStatusDto statusDto)
        {
            var result = await _bookingInfoService
                .UpdateBookingStatus(id, statusDto.Status);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(
            Roles = UserGroupRole.SuperAdmin + "," +
                    UserGroupRole.Staff)]
        [HttpDelete("DeleteBooking/{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var result = await _bookingInfoService
                .DeleteBookings(id);

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }
    }
}