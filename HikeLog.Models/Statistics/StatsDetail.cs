using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeLog.Models.Statistics
{
    public class StatsDetail
    {
        [Display(Name="Section ID:")]
        public int SectionID { get; set; }
        [Display(Name="Avg MPD - Plan:")]
        public double AverageMPDPlan { get; set; }
        [Display(Name="Avg MPD - Actual:")]
        public double AverageMPDActual { get; set; }
        [Display(Name="Avg MPD - Actual Excl 0:")]
        public double AverageMPDActualNoZeros { get; set; }
        [Display(Name="Est Days to Finish Section:")]
        public double EstimatedDaysToComplete { get; set; }
    }
}
