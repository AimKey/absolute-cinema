using BusinessObjects.Models;
using Common.Pagination;

namespace Common.ViewModels.UserVMs;

public class ManageUsersVm
{
    public PagedResult<User> Users { get; set; } = new();
    public string Query { get; set; } = "";
    public string StatusFilter { get; set; } = "all"; // all, active, inactive
    public string RoleFilter { get; set; } = "all"; // all, admin, user
    public string SortBy { get; set; } = "createdAt"; // createdAt, username, email
    public bool SortDescending { get; set; } = true; // asc, desc
    
    public int Page { get; set; } = 1;
}