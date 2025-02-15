using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TelecomApp.Models;

[Table("bills")]
public partial class Bill
{
    [Key]
    [Column("bill_id")]
    public int BillId { get; set; }

    [Column("phone_id")]
    public int? PhoneId { get; set; }

    [Column("costs", TypeName = "decimal(10, 2)")]
    public decimal? Costs { get; set; }

    [ForeignKey("PhoneId")]
    [InverseProperty("Bills")]
    public virtual Phone? Phone { get; set; }

    [ForeignKey("BillId")]
    [InverseProperty("Bills")]
    public virtual ICollection<Call> Calls { get; set; } = new List<Call>();
}
