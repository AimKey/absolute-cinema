using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Filters.Tags;

public class TagFilterCriteria
{
    public string Search { get; set; } = "";
    public string Status { get; set; } = "all";
    public string Sort { get; set; } = "name"; // name, createdAt
    public int Page { get; set; } = 1;
}
