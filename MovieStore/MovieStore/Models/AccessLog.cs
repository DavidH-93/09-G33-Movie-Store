using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class AccessLog
    {
        public Guid AccessLogID { get; set; }

        public string UserID { get; set; }
        public DateTime LogTime { get; set; }
        public string AccessType { get; set; }

    }
}
