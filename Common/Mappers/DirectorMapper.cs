using BusinessObjects.Models;
using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Mappers;

public class DirectorMapper
{
    public static DirectorVM MaptoDirectorVM(Director director)
    {
        return new DirectorVM
        {
            Id = director.Id,
            Name = director.Name,
            AvatarURL = director.AvatarURL,
            Gender = director.Gender,
            Dob = director.Dob,
            Description = director.Description
        };
    }
}
