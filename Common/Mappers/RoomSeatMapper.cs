using BusinessObjects.Models;
using Common.ViewModels;
using Common.ViewModels.SeatTypeVMs;
using Common.ViewModels.SeatVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Mappers
{
    public class RoomSeatMapper
    {
        public static RoomSeatsVM MapToRoomSeatVM(Room room, List<SeatTypeVM> seatTypes, List<SeatWithStatusVM> seats, 
            Showtime showtime)
        {
            return new RoomSeatsVM
            {
                Room = room,
                SeatTypes = seatTypes,
                Seats = seats,
                TicketBasePrice = showtime.BasePrice
            };
        }

        public static SeatWithStatusVM MapToSeatWithStatusVM(Seat seat, ShowtimeSeat showtimeSeat)
        {
            bool isBooked = false;

            if (showtimeSeat != null)
            {
                // IF there is a showtimeSeat associated with the showtime for this seat, it is booked
                isBooked = seat.ShowtimeSeats.Any(ss => ss.ShowtimeId == showtimeSeat.ShowtimeId);
            }

            return new SeatWithStatusVM
            {
                Id = seat.Id,
                SeatRow = seat.SeatRow,
                SeatNumber = seat.SeatNumber,
                Description = seat.Description,
                IsAvailable = !isBooked
            };
        }
    }
}
