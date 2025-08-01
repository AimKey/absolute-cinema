﻿using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ViewModels.UserVMs;

namespace Services.Interfaces
{
    public interface IDashboardFacade
    {
        DashboardViewModel GetDashboardViewModel();
        ManageUsersVm GetManageUsersViewModel();
    }
}
