using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Data.Entities
{
    public class JobPosting
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public double Wage { get; set; }
    }
}
