using HikeLog.Data;
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

        public IEnumerable<DailyLogListItem> GetDailyLogByMilesHiked(string search) 
        {
            if (search == "") return GetDailyLogs();

            double milesHiked = Convert.ToDouble(search);

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .DailyLogs.AsEnumerable()
                    .Where(d => IsMilesHiked(d, milesHiked))
                    .Select(d =>
                       new DailyLogListItem
                        {
                            DailyLogId = d.DailyLogId,
                            ProfileId = d.ProfileId,
                            SectionId = d.SectionId,
                            Date = d.Date,
                            StartMile = d.StartMile,
                            EndMile = d.EndMile,
                            Notes = d.Notes,
                            IsStarred = d.IsStarred,
                            CreatedUtc = d.CreatedUtc
                        }
                       );
                return entity.ToList();
            }
        }

        public static bool IsMilesHiked(DailyLog n, double milesHiked)
        {
            return n.MilesHiked == milesHiked;
        }

        public IEnumerable<DailyLogListItem> GetDailyLogByDate(string search)
        {
            if (search == "") return GetDailyLogs();

            DateTimeOffset inputDate = DateTimeOffset.Parse(search);
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .DailyLogs.Where(d => d.Date == inputDate)
                    .Select(d => new DailyLogListItem
                    {
                        DailyLogId = d.DailyLogId,
                        ProfileId=d.ProfileId,
                        SectionId=d.SectionId,
                        Date=d.Date,
                        StartMile=d.StartMile,
                        EndMile=d.EndMile,
                        Notes=d.Notes,
                        IsStarred=d.IsStarred,
                        CreatedUtc=d.CreatedUtc
                    });
                return entity.ToList();
            }
        }
        public IEnumerable<DailyLogListItem> GetDailyLogByMileMarker(string search)
        {
            if (search == "") return GetDailyLogs();

            double milemarker = Convert.ToDouble(search);

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .DailyLogs.Where(d => d.EndMile >= milemarker && d.StartMile <= milemarker)
                    .Select(d => new DailyLogListItem
                    {
                        DailyLogId = d.DailyLogId,
                        ProfileId = d.ProfileId,
                        SectionId = d.SectionId,
                        Date = d.Date,
                        StartMile = d.StartMile,
                        EndMile = d.EndMile,
                        Notes = d.Notes,
                        IsStarred = d.IsStarred,
                        CreatedUtc = d.CreatedUtc
                    });
                return entity.ToList();
            }
        }
        public IEnumerable<DailyLogListItem> GetDailyLogByHasNotes(string search)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .DailyLogs.Where(d => d.Notes != null)
                    .Select(d => new DailyLogListItem
                    {
                        DailyLogId = d.DailyLogId,
                        ProfileId = d.ProfileId,
                        SectionId = d.SectionId,
                        Date = d.Date,
                        StartMile = d.StartMile,
                        EndMile = d.EndMile,
                        Notes = d.Notes,
                        IsStarred = d.IsStarred,
                        CreatedUtc = d.CreatedUtc
                    });
                return entity.ToList();
            }
        }
        public IEnumerable<DailyLogListItem> GetDailyLogByNoteContent(string search)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .DailyLogs.Where(d => d.Notes.Contains(search.ToLower()))
                    .Select(d => new DailyLogListItem
                    {
                        DailyLogId = d.DailyLogId,
                        ProfileId = d.ProfileId,
                        SectionId = d.SectionId,
                        Date = d.Date,
                        StartMile = d.StartMile,
                        EndMile = d.EndMile,
                        Notes = d.Notes,
                        IsStarred = d.IsStarred,
                        CreatedUtc = d.CreatedUtc
                    });
                return entity.ToList();
            }
        }

        public IEnumerable<DailyLogListItem> GetDailyLogByStarStatus(string search)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .DailyLogs.Where(d => d.IsStarred == true)
                    .Select(d => new DailyLogListItem
                    {
                        DailyLogId = d.DailyLogId,
                        ProfileId = d.ProfileId,
                        SectionId = d.SectionId,
                        Date = d.Date,
                        StartMile = d.StartMile,
                        EndMile = d.EndMile,
                        Notes = d.Notes,
                        IsStarred = d.IsStarred,
                        CreatedUtc = d.CreatedUtc
                    });
                return entity.ToList();
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
