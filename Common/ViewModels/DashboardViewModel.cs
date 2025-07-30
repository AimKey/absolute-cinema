using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels;

public class DashboardViewModel
{
    // Các thuộc tính để chứa số liệu thống kê
    public int TotalMovies { get; set; }
    public int MoviesToday { get; set; }
    public int NewUsers { get; set; }
    public int TotalUsers { get; set; }
    public int TotalScreenings { get; set; }
    public int TotalCinemaRooms { get; set; }
    public int TotalGenres { get; set; }

    // Các thuộc tính mới cho thống kê doanh thu và vé bán ra
    public decimal TotalRevenue { get; set; }
    public decimal RevenueToday { get; set; }
    public int TotalTicketsSold { get; set; }
    public int TicketsSoldToday { get; set; }

    public DashboardViewModel()
    {
        // Gán các giá trị mặc định (bạn sẽ thay thế bằng dữ liệu từ database)
        TotalMovies = 1234;
        MoviesToday = 56;
        NewUsers = 89;
        TotalUsers = 5678;
        TotalScreenings = 2345;
        TotalCinemaRooms = 15;
        TotalGenres = 20;

        // Dữ liệu mẫu cho các thuộc tính mới
        TotalRevenue = 12500000000M; // 12.5 tỷ VND
        RevenueToday = 75000000M;    // 75 triệu VND
        TotalTicketsSold = 500000;
        TicketsSoldToday = 3000;
    }
}

