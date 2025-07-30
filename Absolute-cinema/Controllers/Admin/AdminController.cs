using BusinessObjects.Models;
using Common.Constants;
using Common.ViewModels;
using Common.ViewModels.UserVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Absolute_cinema.Controllers.Admin;

[Authorize(Roles = RoleConstants.Admin)]
public class AdminController : Controller
{
    IDashboardFacade _dashboardFacade;
    IUserService _userService;

    public AdminController(IDashboardFacade dashboardFacade, IUserService userService)
    {
        _dashboardFacade = dashboardFacade;
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View(_dashboardFacade.GetDashboardViewModel());
    }

    public IActionResult ManageUsers(ManageUsersVm vm)
    {
        vm = _userService.GetManageUsersViewModel(vm);
        return View(vm);
    }

    [HttpPost]
    public IActionResult ToggleUserStatus(Guid userId)
    {
        try
        {
            var user = _userService.GetById(userId);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
                user.UpdatedAt = DateTime.Now;
                _userService.UpdateUser(user);
                
                TempData["Success"] = $"User {(user.IsActive ? "activated" : "banned")} successfully!";
            }
            else
            {
                TempData["Error"] = "User not found!";
            }
        }
        catch (Exception)
        {
            TempData["Error"] = "An error occurred while updating user status!";
        }

        return RedirectToAction("ManageUsers");
    }
}