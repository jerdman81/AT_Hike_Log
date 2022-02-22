using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeLog.Models.Statistics
{
    public class StatsDetail
    {
        public int SectionID { get; set; }
        public double AverageMPDPlan { get; set; }
        public double AverageMPDActual { get; set; }
        public double AverageMPDActualNoZeros { get; set; }
        public double EstimatedDaysToComplete { get; set; }
    }
}
