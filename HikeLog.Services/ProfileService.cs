using HikeLog.Data;
using HikeLog.Models;
using HikeLog.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeLog.Services
{
    public class ProfileService
    {
        private readonly Guid _userId;

        public ProfileService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateProflie(ProfileCreate model)
        {
            var entity =
                new Profile()
                {
                    UserId = _userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    TrailName = model.TrailName,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Profiles.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ProfileListItem> GetProfiles()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Profiles
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new ProfileListItem
                                {
                                    ProfileId = e.ProfileId,
                                    FullName = e.FullName,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public ProfileDetail GetProfileById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Profiles
                        .Single(e => e.ProfileId == id && e.UserId == _userId);
                return
                    new ProfileDetail
                    {
                        ProfileId = entity.ProfileId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        TrailName = entity.TrailName,
                        Hometown = entity.Hometown,
                        CreatedUtc = entity.CreatedUtc,
                        UpdatedUtc = entity.UpdatedUtc
                    };
            }
        }
        
    }
}
