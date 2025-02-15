using System.ComponentModel.DataAnnotations;

namespace TelecomApp.Models.MetaData
{
    public partial class ProgramMetaData
    {
        [Display(Name = "Program Name")]
        public string? ProgramName { get; set; }
    }
}