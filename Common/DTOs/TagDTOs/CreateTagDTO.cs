using BusinessObjects.Models;
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs.TagDTOs;
public class CreateTagDTO
{
    public Guid Id { get; set; }

    // Normal Properties
    [StringLength(100, ErrorMessage = "Tag name cannot exceed 100 characters.")]
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
