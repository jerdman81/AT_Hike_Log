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
    }
}
