using System.ComponentModel.DataAnnotations;

namespace TelecomApp.Models.MetaData
{
    public partial class BillMetaData
    {
        [Required]
        [Display(Name = "Phone")]
        public int? PhoneId { get; set; }
    }
}
