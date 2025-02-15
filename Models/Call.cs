using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TelecomApp.Models;

[Table("calls")]
public partial class Call
{
    [Key]
    [Column("call_id")]
    public int CallId { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [ForeignKey("CallId")]
    [InverseProperty("Calls")]
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
