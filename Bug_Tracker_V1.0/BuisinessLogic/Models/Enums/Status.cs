using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.Models.Enums
{
    public enum Status
    {
        Open,
        [Display(Name = "In Progress")]
        InProgress,
        Closed
    }
}
