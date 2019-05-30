using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.ViewModels
{
    public class LogViewModel
    {
        public Guid AccessLogID { get; set; }

        [Required]
        [Display(Name ="User ID")]
        public string UserID { get; set; }

        [Required]
        [Display(Name ="Log Time")]
        public DateTime LogTime { get; set; }
        
        [Required]
        [Display(Name = "Access Type")]
        public string AccessType { get; set; }
    }
}
