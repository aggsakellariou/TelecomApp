using System.ComponentModel.DataAnnotations;

namespace TelecomApp.Models.MetaData
{
    public partial class PhoneMetaData
    {
        [Display(Name = "Program")]
        public int? ProgramId { get; set; }

        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}