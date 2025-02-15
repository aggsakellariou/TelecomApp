using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TelecomApp.Models;

[Table("admins")]
public partial class Admin
{
    [Key]
    [Column("admin_Id")]
    public int AdminId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Admins")]
    public virtual User? User { get; set; }
}
