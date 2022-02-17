using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeLog.Models.Profile
{
    public class ProfileEdit
    {
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TrailName { get; set; }
        public string Hometown { get; set; }

    }
}
