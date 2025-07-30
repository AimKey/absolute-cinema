using BusinessObjects.Models;
using Common.Constants;
using Common.ViewModels;
using DataAccessObjects;
using Microsoft.AspNetCore.Mvc;

namespace Absolute_cinema.Controllers.Api;

[Route("api/[controller]")] 
[ApiController]             
public class RoomManagementApiController : ControllerBase
{
    private readonly AbsoluteCinemaContext _context;

    public RoomManagementApiController(AbsoluteCinemaContext context)
    {
        _context = context;
    }

    // GET: api/RoomManagementApi/seattype/all
    [HttpGet("seattype/all")]
    public IActionResult GetAllSeatTypes()
    {
        var seatTypesWithColors = _context.SeatTypes.Select(st => new
        {
            st.Id,
            st.Name,
            st.PriceMutiplier,
            color = GetColorForSeatType(st.Name) 
        }).ToList();
        return Ok(seatTypesWithColors);
    }

    // GET: api/RoomManagementApi/rooms/{roomId}/seats
    [HttpGet("rooms/{roomId}/seats")]
    public IActionResult GetSeatsForRoom(Guid roomId)
    {
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
        List<Seat> seats = _context.Seats.Where(s => s.RoomId == roomId && s.RemovedAt == null).ToList();
        
        // Chuyển đổi từ SeatVM sang Seat
        var updatedSeatEntities = updatedSeats.Select(vm => new Seat
        {
            Id = vm.Id == Guid.Empty ? Guid.NewGuid() : vm.Id,
            SeatRow = vm.SeatRow,
            SeatNumber = vm.SeatNumber,
            Description = vm.Description,
            SeatTypeId = vm.SeatTypeId,
            RoomId = roomId,
            CreatedAt = DateTime.Now,
            CreatedBy = null, 
            UpdatedAt = DateTime.Now, 
            UpdatedBy = null
        }).ToList();

        // Xóa ghế cũ của phòng này
        _context.Seats.RemoveRange(seats);
        
        _context.Seats.AddRange(updatedSeatEntities); 

        // Lưu thay đổi vào cơ sở dữ liệu
        _context.SaveChanges(); 

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
