using HikeLog.Data;
using HikeLog.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeLog.Services
{
    public class StatisticsService
    {
        private readonly Guid _userid;

        public StatisticsService(Guid userId)
        {
            _userid = userId;
        }

        public StatsDetail GetStatsForSection(int sectionId)
        {
            var a = AvgMPDSectionPlan(sectionId);
            var b = AvgMPDSectionActual(sectionId);
            var c = AvgMPDSectionActualExclZero(sectionId);
            var d = EstimatedDaysToCompleteSection(sectionId);

            return
                new StatsDetail
                {
                    SectionID = sectionId,
                    AverageMPDPlan = a,
                    AverageMPDActual = b,
                    AverageMPDActualNoZeros = c,
                    EstimatedDaysToComplete = d
                };
        }

        public double AvgMPDSectionPlan(int sectionId)
        {
            
            using (var ctx = new ApplicationDbContext())
            {
                var endDate = ctx.Sections.Where(d => d.SectionId == sectionId).Select(x => x.EndDate).FirstOrDefault();
                var startDate = ctx.Sections.AsEnumerable().Where(d => d.SectionId == sectionId).Select(x => x.StartDate).FirstOrDefault();

                var days = (endDate - startDate).Days+1;

                var miles = ctx.Sections.AsEnumerable().Where(d => d.SectionId == sectionId).Select(m => m.MilesHiked).FirstOrDefault();

                var mpd = Math.Round(miles / days, 1);

                return mpd;
            }
        }

        public double AvgMPDSectionActual(int sectionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                int days = ctx.DailyLogs.Where(d => d.SectionId == sectionId).Count();

                var maxMM = Convert.ToDouble(
                    Math.Max(
                        ctx.DailyLogs.Where(d => d.SectionId == sectionId).Max(m => m.EndMile), 
                        ctx.DailyLogs.Where(d => d.SectionId == sectionId).Max(m => m.StartMile)));
                var minMM = Convert.ToDouble(
                    Math.Min(
                        ctx.DailyLogs.Where(d => d.SectionId == sectionId).Min(m => m.StartMile),
                        ctx.DailyLogs.Where(d => d.SectionId == sectionId).Min(m => m.EndMile)));

                var miles = maxMM - minMM;

                var mpd = Math.Round(miles / days, 1);

                return mpd;
            }
        }

        public double AvgMPDSectionActualExclZero(int sectionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var days = ctx.DailyLogs.AsEnumerable().Where(d => d.SectionId == sectionId).Where(m => AreMilesZero(m)).Count();

                var maxMM = Convert.ToDouble(
                    Math.Max(
                        ctx.DailyLogs.Where(d => d.SectionId == sectionId).Max(m => m.EndMile),
                        ctx.DailyLogs.Where(d => d.SectionId == sectionId).Max(m => m.StartMile)));
                var minMM = Convert.ToDouble(
                    Math.Min(
                        ctx.DailyLogs.Where(d => d.SectionId == sectionId).Min(m => m.StartMile),
                        ctx.DailyLogs.Where(d => d.SectionId == sectionId).Min(m => m.EndMile)));

                var miles = maxMM - minMM;

                var mpd = Math.Round(miles / days, 1);

                return mpd;
            }
        }

        public static bool AreMilesZero(DailyLog n)
        {
            return n.MilesHiked != 0;
        }

        public double EstimatedDaysToCompleteSection(int sectionId)
        {
            
            using (var ctx = new ApplicationDbContext())
            {
                if (
                    ctx.Sections.AsEnumerable().Where(s => s.SectionId == sectionId).Select(d => d.EndMile).FirstOrDefault()
                    > 
                    ctx.Sections.AsEnumerable().Where(s => s.SectionId == sectionId).Select(d => d.StartMile).FirstOrDefault())
                {
                    var milesRemaining =
                        ctx.Sections.AsEnumerable().Where(s => s.SectionId == sectionId).Select(d => d.EndMile).FirstOrDefault()
                        -
                        ctx.DailyLogs.Where(d => d.SectionId == sectionId).Max(m => m.EndMile);
                    
                    var pace = AvgMPDSectionActual(sectionId);

                    var days = Math.Round(milesRemaining / pace, 1);

                    return days;
                }
                else
                {
                    var milesRemaining =
                         ctx.DailyLogs.Where(d => d.SectionId == sectionId).Min(m => m.EndMile)
                         -
                         ctx.Sections.AsEnumerable().Where(s => s.SectionId == sectionId).Select(d => d.EndMile).FirstOrDefault();
                    
                    var pace = AvgMPDSectionActual(sectionId);

                    var days = Math.Round(milesRemaining / pace, 1);

                    return days;
                };
                               
            }
        }

    }
}
