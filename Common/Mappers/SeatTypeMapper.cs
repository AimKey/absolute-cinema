using BusinessObjects.Models;
using Common.ViewModels.SeatTypeVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Mappers
{
    public class SeatTypeMapper
    {
        public static SeatTypeVM MapSeatToSeatTypeVM(Seat s)
        {
            if (s == null)
            {
                return null;
            }
            return new SeatTypeVM
            {
                Id = s.SeatType.Id,
                Name = s.SeatType.Name,
                PriceMutiplier = s.SeatType.PriceMutiplier,
                ColorHex = s.SeatType.ColorHex,
            };
        }
    }
}
