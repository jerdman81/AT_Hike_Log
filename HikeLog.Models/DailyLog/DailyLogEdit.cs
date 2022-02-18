using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeLog.Models.DailyLog
{
    public class DailyLogEdit
    {
        public int DailyLogId { get; set; }
        public int ProfileId { get; set; }
        public int? SectionId { get; set; }
        public DateTimeOffset Date { get; set; }
        public double StartMile { get; set; }
        public double EndMile { get; set; }
        public string Notes { get; set; }
        public bool IsStarred { get; set; }
    }
}
