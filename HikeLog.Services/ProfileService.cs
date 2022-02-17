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

        public bool CreateProfile(ProfileCreate model)
        {
            var entity =
                new Profile()
                {
                    UserId = _userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    TrailName = model.TrailName,
                    Hometown = model.Hometown,
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
                                    TrailName= e.TrailName,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
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

        public bool UpdateProfile(ProfileEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Profiles
                        .Single(e => e.ProfileId == model.ProfileId && e.UserId == _userId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.TrailName = model.TrailName;
                entity.Hometown = model.Hometown;
                entity.UpdatedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteProfile(int profileId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Profiles
                        .Single(e => e.ProfileId == profileId && e.UserId == _userId);

                ctx.Profiles.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        
    }
}
