using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeLog.Models.Profile
{
    public class ProfileCreate
    {
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Display(Name="Last Name")]
        public string LastName { get; set; }
        [Display(Name="Trail Name")]
        public string TrailName { get; set; }
        public string Hometown { get; set; }
    }
}
