using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace JobOffers.Models
{
    public class ApplayForJob
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ApplayData { get; set; }
        public int JobId { get; set; }
        public string UserId { get; set; }

        public virtual Job job { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}