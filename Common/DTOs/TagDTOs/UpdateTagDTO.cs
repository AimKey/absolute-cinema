using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.TagDTOs;

public class UpdateTagDTO
{
    public Guid Id { get; set; }

    // Normal Properties
    [StringLength(100, ErrorMessage = "Tag name cannot exceed 100 characters.")]
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
