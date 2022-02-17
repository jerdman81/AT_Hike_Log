using HikeLog.Data;
using HikeLog.Models.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeLog.Services
{
    public class SectionService
    {
        private readonly Guid _userId;

        public SectionService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateSection(SectionCreate model)
        {
            var entity =
                new Section()
                {
                    ProfileId = model.ProfileId,
                    SectionName = model.SectionName,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    StartMile = model.StartMile,
                    EndMile = model.EndMile,
                    Direction = model.Direction,
                    CreatedUtc = DateTimeOffset.Now,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Sections.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<SectionListItem> GetSections()
        {
            using (var ctx = new ApplicationDbContext())
            {
                
                var query =
                    ctx
                        .Sections
                       
                        .Select(
                            e =>
                                new SectionListItem
                                {
                                    SectionId = e.SectionId,
                                    ProfileId = e.ProfileId,
                                    SectionName = e.SectionName,
                                    StartDate = e.StartDate,
                                    EndDate = e.EndDate,
                                    StartMile = e.StartMile,
                                    EndMile = e.EndMile,
                                    Direction = e.Direction,
                                    CreatedUtc = e.CreatedUtc,
                                }
                        );
                return query.ToArray();
            }
        }

    }
}
