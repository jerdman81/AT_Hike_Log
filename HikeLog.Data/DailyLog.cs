using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeLog.Data
{
    public class DailyLog
    {
        [Key]
        public int DailyLogId { get; set; }
        [ForeignKey(nameof(Profile))]
        public int ProfileId { get; set; }
        [ForeignKey(nameof(Section))]
        public int? SectionId { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; }
        [Required]
        [Range(0,2193.1,ErrorMessage ="Please enter a MM between 0 and 2193.1")]
        public double StartMile { get; set; }
        [Required]
        [Range(0, 2193.1, ErrorMessage = "Please enter a MM between 0 and 2193.1")]
        public double EndMile { get; set; }
        public double MilesHiked
        {
            get
            {
                return Math.Abs(EndMile - StartMile);
            }
        }
        [MaxLength(8000, ErrorMessage = "Note exceeds the maximum length.")]
        public string Notes { get; set; }
        public bool IsStarred { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset ModifiedUtc { get; set; }


        public virtual Profile Profile { get; set; }
        public virtual Section Section { get; set; }
    }
}
