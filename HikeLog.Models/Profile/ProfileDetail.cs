﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeLog.Models.Profile
{
    public class ProfileDetail
    {
        public int ProfileId { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name="Last Name")]
        public string LastName { get; set; }
        [Display(Name="Trail Name")]
        public string TrailName { get; set; }
        public string Hometown { get; set; }
        [Display(Name="Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name="Modified")]
        public DateTimeOffset? UpdatedUtc { get; set; }
    }
}
