using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels;

public class ReviewVM
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string Content { get; set; }
    public DateTime ReviewDate { get; set; } = DateTime.Now;
    public User User { get; set; }
    public DateTime? CreatedAt { get; set; }
}
