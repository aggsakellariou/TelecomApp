using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TelecomApp.Models;

[Table("phonePrograms")]
public partial class PhoneProgram
{
    [Key]
    [Column("program_id")]
    public int ProgramId { get; set; }

    [Column("program_Name")]
    [StringLength(50)]
    public string? ProgramName { get; set; }

    [Column("benefits")]
    public string? Benefits { get; set; }

    [Column("charge", TypeName = "decimal(10, 2)")]
    public decimal? Charge { get; set; }

    [InverseProperty("Program")]
    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();
}