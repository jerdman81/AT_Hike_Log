using HikeLog.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeLog.Models.Section
{
    public class SectionListItem
    {
        public int SectionId { get; set; }
        public int ProfileId { get; set; }
        [Display(Name="Section Name")]
        public string SectionName { get; set; }
        [Display(Name ="Start Date")]
        public DateTimeOffset StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTimeOffset EndDate { get; set; }
        [Display(Name = "Start MM")]
        public double StartMile { get; set; }
        [Display(Name = "End MM")]
        public double EndMile { get; set; }
        public HikeDirection Direction { get; set; }
        [Display(Name="Created")]
        public DateTimeOffset CreatedUtc { get; set; }


    }
}
