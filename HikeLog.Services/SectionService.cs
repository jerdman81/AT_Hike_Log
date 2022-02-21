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

        public SectionDetail GetSectionById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Sections
                        .Single(e => e.SectionId == id);
                return
                    new SectionDetail
                    {
                        SectionId = entity.SectionId,
                        ProfileId = entity.ProfileId,
                        SectionName = entity.SectionName,
                        StartDate = entity.StartDate,
                        EndDate = entity.EndDate,
                        StartMile = entity.StartMile,
                        EndMile = entity.EndMile,
                        Direction = entity.Direction,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public IEnumerable<SectionListItem> GetSectionByName(string search)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Sections.Where(s => s.SectionName.ToLower().Contains(search.ToLower()) || search == null)
                    .Select(s => new SectionListItem
                    {
                        SectionId = s.SectionId,
                        ProfileId = s.ProfileId,
                        SectionName = s.SectionName,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate,
                        StartMile = s.StartMile,
                        EndMile = s.EndMile,
                        Direction = s.Direction,
                        CreatedUtc = s.CreatedUtc,
                    });
                return entity.ToList();
            }
        }

        public IEnumerable<SectionListItem> GetSectionByDirection(string search)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Sections.Where(s => s.Direction.ToString().ToLower().Contains(search.ToLower()))
                    .Select(s => new SectionListItem
                    {
                        SectionId = s.SectionId,
                        ProfileId = s.ProfileId,
                        SectionName = s.SectionName,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate,
                        StartMile = s.StartMile,
                        EndMile = s.EndMile,
                        Direction = s.Direction,
                        CreatedUtc = s.CreatedUtc,
                    });
                return entity.ToList();
            }
        }
        public IEnumerable<SectionListItem> GetSectionByDate(string search)
        {
            if (search == "") return GetSections();

            DateTimeOffset inputDate = DateTimeOffset.Parse(search);
            using (var ctx = new ApplicationDbContext())
            {
                
                var entity =
                    ctx
                    .Sections.Where(s => s.EndDate >= inputDate && s.StartDate <= inputDate)
                    .Select(s => new SectionListItem
                    {
                        SectionId = s.SectionId,
                        ProfileId = s.ProfileId,
                        SectionName = s.SectionName,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate,
                        StartMile = s.StartMile,
                        EndMile = s.EndMile,
                        Direction = s.Direction,
                        CreatedUtc = s.CreatedUtc,
                    });
                return entity.ToList();
            }
        }
        public IEnumerable<SectionListItem> GetSectionByMM(string search)
        {
            if (search == "") return GetSections();
            
            double milemarker = Convert.ToDouble(search);
            
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Sections.Where(s => s.EndMile >= milemarker && s.StartMile <= milemarker)
                    .Select(s => new SectionListItem
                    {
                        SectionId = s.SectionId,
                        ProfileId = s.ProfileId,
                        SectionName = s.SectionName,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate,
                        StartMile = s.StartMile,
                        EndMile = s.EndMile,
                        Direction = s.Direction,
                        CreatedUtc = s.CreatedUtc,
                    });
                return entity.ToList();
            }
        }

        public bool UpdateSection(SectionEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Sections
                        .Single(e => e.SectionId == model.SectionId);

                entity.SectionName = model.SectionName;
                entity.StartDate = model.StartDate;
                entity.EndDate = model.EndDate;
                entity.StartMile = model.StartMile;
                entity.EndMile = model.EndMile;
                entity.Direction = model.Direction;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteSection(int sectionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Sections
                        .Single(e => e.SectionId == sectionId);

                ctx.Sections.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

    }
}
