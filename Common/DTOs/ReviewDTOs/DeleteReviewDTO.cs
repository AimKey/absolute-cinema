﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.ReviewDTOs;

public class DeleteReviewDTO
{
    [Required]
    public Guid MovieId { get; set; }
}
