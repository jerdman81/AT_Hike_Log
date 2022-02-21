﻿using HikeLog.Data;
using HikeLog.Models.DailyLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeLog.Services
{
    public class DailyLogService
    {
        private readonly Guid _userid;

        public DailyLogService(Guid userId)
        {
            _userid = userId;
        }

        public bool CreateDailyLog(DailyLogCreate model)
        {
            var entity =
                new DailyLog()
                {
                    ProfileId = model.ProfileId,
                    SectionId = model.SectionId,
                    Date = model.Date,
                    StartMile = model.StartMile,
                    EndMile = model.EndMile,
                    Notes = model.Notes,
                    IsStarred = model.IsStarred,
                    CreatedUtc = DateTimeOffset.UtcNow
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.DailyLogs.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<DailyLogListItem> GetDailyLogs()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DailyLogs
                        .Select(
                            e =>
                                new DailyLogListItem
                                {
                                    DailyLogId = e.DailyLogId,
                                    ProfileId = e.ProfileId,
                                    SectionId = e.SectionId,
                                    Date = e.Date,
                                    StartMile = e.StartMile,
                                    EndMile = e.EndMile,
                                    Notes = e.Notes,
                                    IsStarred = e.IsStarred,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );
                return query.ToArray();
            }
        }

        public DailyLogDetail GetDailyLogById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .DailyLogs
                        .Single(e => e.DailyLogId == id);
                return
                    new DailyLogDetail
                    {
                        DailyLogId = entity.DailyLogId,
                        ProfileId = entity.ProfileId,
                        SectionId = entity.SectionId,
                        Date = entity.Date,
                        StartMile = entity.StartMile,
                        EndMile = entity.EndMile,
                        Notes = entity.Notes,
                        IsStarred = entity.IsStarred,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateDailyLog(DailyLogEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .DailyLogs
                        .Single(e => e.DailyLogId == model.DailyLogId);

                entity.DailyLogId = model.DailyLogId;
                entity.ProfileId = model.ProfileId;
                entity.SectionId = model.SectionId;
                entity.Date = model.Date;
                entity.StartMile = model.StartMile;
                entity.EndMile = model.EndMile;
                entity.Notes = model.Notes;
                entity.IsStarred = model.IsStarred;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteDailyLog(int dailyLogId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .DailyLogs
                        .Single(e => e.DailyLogId == dailyLogId);

                ctx.DailyLogs.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}