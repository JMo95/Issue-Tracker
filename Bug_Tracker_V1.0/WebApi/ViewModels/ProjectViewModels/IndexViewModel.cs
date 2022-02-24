using Bug_Tracker_V1._0.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.ViewModels.ProjectViewModels
{
    public class IndexViewModel
    {
        public AuthenticationUser User { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("New Project Name")]
        public string NewProjectName { get; set; }

        [Url]
        public string ProjectRepoUrl { get; set; }

        [DisplayName("New Project Description")]
        [MaxLength(255)]
        public string NewProjectDescription { get; set; }

        [Url]
        public string NewProjectRepoUrl { get; set; }   
    }
}
