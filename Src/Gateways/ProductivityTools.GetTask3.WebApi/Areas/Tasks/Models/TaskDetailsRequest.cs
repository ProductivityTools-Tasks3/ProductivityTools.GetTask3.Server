using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.WebApi.Areas.Tasks.Models
{
    public class TaskDetailsRequest
    {
        [Required]
        int TaskId { get; set; }
    }
}
