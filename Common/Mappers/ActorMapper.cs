using BusinessObjects.Models;
using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Mappers;

public class ActorMapper
{
    public static ActorVM MaptoActorVM(Actor actor)
    {
        return new ActorVM
        {
            Id = actor.Id,
            Name = actor.Name,
            AvatarURL = actor.AvatarURL,
            Gender = actor.Gender,
            Dob = actor.Dob,
            Description = actor.Description
        };
    }
}
