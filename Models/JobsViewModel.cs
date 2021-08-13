using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobOffers.Models
{
    public class JobsViewModel
    {
        public string JobTitle { get; set; }
        public IEnumerable<ApplayForJob> Items { get; set; }
    }
}