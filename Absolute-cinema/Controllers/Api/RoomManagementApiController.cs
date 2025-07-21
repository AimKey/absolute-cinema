using BusinessObjects.Models;
using Common.Constants;
using Common.ViewModels;
using DataAccessObjects;
using Microsoft.AspNetCore.Mvc;

namespace Absolute_cinema.Controllers.Api;

[Route("api/[controller]")] // Định nghĩa route cho API Controller
[ApiController] // Đánh dấu đây là một API Controller
                // [Authorize(Roles = "Admin")] // Có thể thêm Authorize nếu API cần bảo mật
public class RoomManagementApiController : ControllerBase
{
    // Dữ liệu mẫu (thay thế bằng DbContext của bạn)
    private readonly AbsoluteCinemaContext _context;

    public RoomManagementApiController(AbsoluteCinemaContext context)
    {
        _context = context;
    }



    // GET: api/RoomManagementApi/seattype/all
    [HttpGet("seattype/all")]
    public IActionResult GetAllSeatTypes()
    {
        // Trong thực tế: return Ok(_context.SeatTypes.ToList());
        // Thêm màu sắc cho mục đích hiển thị trong JS
        var seatTypesWithColors = _context.SeatTypes.Select(st => new
        {
            st.Id,
            st.Name,
            st.PriceMutiplier,
            color = GetColorForSeatType(st.Name) // Hàm GetColorForSeatType sẽ được định nghĩa
        }).ToList();
        return Ok(seatTypesWithColors);
    }

    // GET: api/RoomManagementApi/rooms/{roomId}/seats
    [HttpGet("rooms/{roomId}/seats")]
    public IActionResult GetSeatsForRoom(Guid roomId)
    {
        // Trong thực tế: return Ok(_context.Seats.Where(s => s.RoomId == roomId && s.RemovedAt == null).ToList());
        var seats = _context.Seats.Where(s => s.RoomId == roomId && s.RemovedAt == null).ToList();
        var seatVMs = seats.Select(s => new SeatVM
        {
            Id = s.Id,
            SeatRow = s.SeatRow,
            SeatNumber = s.SeatNumber,
            Description = s.Description,
            SeatTypeId = s.SeatType.Id,
            RoomId = s.RoomId
        }).ToList();

        return Ok(seatVMs);
    }

    // POST: api/RoomManagementApi/rooms/{roomId}/seats
    [HttpPost("rooms/{roomId}/seats")]
    public IActionResult SaveSeatsForRoom(Guid roomId, [FromBody] List<SeatVM> updatedSeats)
    {
        // Đây là nơi bạn sẽ thực hiện logic lưu vào cơ sở dữ liệu thực tế.
        // Ví dụ:
        // 1. Xóa tất cả ghế cũ của phòng này trong DB
        //    _context.Seats.RemoveRange(_context.Seats.Where(s => s.RoomId == roomId));
        // 2. Thêm tất cả ghế mới từ updatedSeats vào DB
        //    _context.Seats.AddRange(updatedSeats);
        // 3. Lưu thay đổi
        //    _context.SaveChanges();

        List<Seat> seats = _context.Seats.Where(s => s.RoomId == roomId && s.RemovedAt == null).ToList();
        // Chuyển đổi từ SeatVM sang Seat
        var updatedSeatEntities = updatedSeats.Select(vm => new Seat
        {
            Id = vm.Id == Guid.Empty ? Guid.NewGuid() : vm.Id, // Tạo mới nếu Id là Guid.Empty
            SeatRow = vm.SeatRow,
            SeatNumber = vm.SeatNumber,
            Description = vm.Description,
            SeatTypeId = vm.SeatTypeId,
            RoomId = roomId,
            CreatedAt = DateTime.UtcNow, // Thêm thời gian tạo
            CreatedBy = null, // Có thể thêm thông tin người tạo nếu cần
            UpdatedAt = DateTime.UtcNow, // Cập nhật thời gian cập nhật
            UpdatedBy = null // Có thể thêm thông tin người cập nhật nếu cần
        }).ToList();

        // Xóa ghế cũ của phòng này
        _context.Seats.RemoveRange(seats); // Xóa ghế cũ của phòng này
        
        _context.Seats.AddRange(updatedSeatEntities); // Thêm ghế mới vào cơ sở dữ liệu
        // Lưu thay đổi vào cơ sở dữ liệu
        _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

        return Ok(new { message = "Sơ đồ ghế đã được lưu thành công!" });
    }

    // Hàm hỗ trợ để gán màu cho loại ghế (có thể tùy chỉnh)
    private static string GetColorForSeatType(string seatTypeName)
    {
        return seatTypeName switch
        {
            SeatTypeConstants.STANDARD => "#4CAF50",    // Green
            SeatTypeConstants.VIP => "#FFC107",         // Amber
            SeatTypeConstants.COUPLE => "#2196F3",      // Blue
            _ => "#6B7280",                             // Default gray
        };
    }
}
