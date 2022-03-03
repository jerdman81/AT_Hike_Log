using HikeLog.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeLog.Models.Section
{
    public class SectionEdit
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        [Display(Name = "Start Date")]
        public DateTimeOffset StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTimeOffset EndDate { get; set; }
        [Display(Name = "Start Mile")]
        public double StartMile { get; set; }
        [Display(Name = "End Mile")]
        public double EndMile { get; set; }
        public HikeDirection Direction { get; set; }
    }
}
