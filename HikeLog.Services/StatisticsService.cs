﻿using HikeLog.Data;
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
                var endDate = Convert.ToInt32(ctx.Sections.Where(d => d.SectionId == sectionId).Select(x => x.EndDate));
                var startDate = Convert.ToInt32(ctx.Sections.Where(d => d.SectionId == sectionId).Select(x => x.StartDate));

                int days = endDate - startDate;

                var miles = Convert.ToDouble(ctx.Sections.AsEnumerable().Where(d => d.SectionId == sectionId).Select(m => m.MilesHiked));

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
                int days = ctx.DailyLogs.Where(d => d.SectionId == sectionId).Count(m => m.MilesHiked != 0);

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

        public double EstimatedDaysToCompleteSection(int sectionId)
        {
            
            using (var ctx = new ApplicationDbContext())
            {
                if (
                    Convert.ToDouble(ctx.Sections.Where(s => s.SectionId == sectionId).Select(d => d.EndMile)) 
                    > 
                    Convert.ToDouble(ctx.Sections.Where(s => s.SectionId == sectionId).Select(d => d.StartMile)))
                {
                    var milesRemaining =
                        Convert.ToDouble(ctx.Sections.Where(s => s.SectionId == sectionId).Select(d => d.EndMile))
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
                         Convert.ToDouble(ctx.Sections.Where(s => s.SectionId == sectionId).Select(d => d.EndMile));
                    
                    var pace = AvgMPDSectionActual(sectionId);

                    var days = Math.Round(milesRemaining / pace, 1);

                    return days;
                };
                               
            }
        }

    }
}
