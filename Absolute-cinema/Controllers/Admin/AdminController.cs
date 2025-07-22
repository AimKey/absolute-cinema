using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Absolute_cinema.Controllers.Admin;

public class AdminController : Controller
{
    IDashboardFacade _dashboardFacade;

    public AdminController(IDashboardFacade dashboardFacade)
    {
        _dashboardFacade = dashboardFacade;
    }
    public IActionResult Index()
    {
        return View(_dashboardFacade.GetDashboardViewModel());
    }
}
