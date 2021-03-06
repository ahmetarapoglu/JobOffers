using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace JobOffers.Models
{
    public class Job
    {
        public int Id { get; set; }
        [DisplayName("Job Name")]
        public string JobTitle { get; set; }
        [DisplayName("job Description")]
        public string JobContent { get; set; }
        
        [DisplayName("Job Image")]
        public string JobImage { get; set; }

        [DisplayName("Job Type")]
        public int CategoryId { get; set; }

        public string UserID { get; set; }

        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}