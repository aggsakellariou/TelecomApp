using System.ComponentModel.DataAnnotations;

namespace TelecomApp.Models.MetaData
{
    public partial class ClientMetaData
    {
        [Display(Name = "Username")]
        public int UserId { get; set; }

        [Display(Name = "Full Name")]
        public virtual User User { get; set; } = null!;

        [Display(Name = "Phone")]
        public int? PhoneId { get; set; }
    }
}