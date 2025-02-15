using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TelecomApp.Models;

[Table("clients")]
public partial class Client
{
    [Key]
    [Column("client_id")]
    public int ClientId { get; set; }

    [Column("AFM")]
    [StringLength(50)]
    public string? Afm { get; set; }

    [Column("phone_id")]
    public int? PhoneId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [ForeignKey("PhoneId")]
    [InverseProperty("Clients")]
    public virtual Phone? Phone { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Clients")]
    public virtual User? User { get; set; }
}
