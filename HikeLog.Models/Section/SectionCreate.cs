using HikeLog.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeLog.Models.Section
{
    public class SectionCreate
    {
        
        [Required]
        [ForeignKey(nameof(Profile))]
        public int ProfileId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage ="Please enter at least 2 characters.")]
        [MaxLength(100, ErrorMessage ="Please limit name to 100 charachters.")]
        public string SectionName { get; set; }
        [Required]
        public DateTimeOffset StartDate { get; set; }
        [Required]
        public DateTimeOffset EndDate { get; set; }
        [Required]
        [Range(0, 2193.1, ErrorMessage = "Please enter a MM between 0 and 2193.1")]
        public double StartMile { get; set; }
        [Required]
        [Range(0, 2193.1, ErrorMessage = "Please enter a MM between 0 and 2193.1")]
        public double EndMile { get; set; }
        public HikeDirection Direction { get; set; }
    }
}
