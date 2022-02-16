using HikeLog.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeLog.Data
{
    public class Section
    {
        [Key]
        public int SectionId { get; set; }
        [Required]
        [ForeignKey(nameof(Profile))]
        public int ProfileId { get; set; }
        [Required]
        public string SectionName { get; set; }
        [Required]
        public DateTimeOffset StartDate { get; set; }
        [Required]
        public DateTimeOffset EndDate { get; set; }
        [Required]
        public double StartMile { get; set; }
        [Required]
        public double EndMile { get; set; }
        public double MilesHiked
        {
            get
            {
                return Math.Abs(EndMile-StartMile);
            }
        }
        [Required]
        public HikeDirection Direction { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset ModifiedUtc { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
