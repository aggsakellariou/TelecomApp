using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TelecomApp.Models;

[Table("phones")]
public partial class Phone
{
    [Key]
    [Column("phone_id")]
    public int PhoneId { get; set; }

    [Column("phone_Number")]
    [StringLength(15)]
    public string? PhoneNumber { get; set; }

    [Column("program_id")]
    public int? ProgramId { get; set; }

    [InverseProperty("Phone")]
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    [InverseProperty("Phone")]
    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    [ForeignKey("ProgramId")]
    [InverseProperty("Phones")]
    public virtual PhoneProgram? Program { get; set; }
}
