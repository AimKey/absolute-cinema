﻿using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels;

public class ActorVM
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string AvatarURL { get; set; }
    public DateTime Dob { get; set; }
    public string Gender { get; set; }
    public string Description { get; set; }
}
