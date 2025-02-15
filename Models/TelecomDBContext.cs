using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TelecomApp.Models;

public partial class TelecomDBContext : DbContext
{
    public TelecomDBContext()
    {
    }

    public TelecomDBContext(DbContextOptions<TelecomDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Call> Calls { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }

    public virtual DbSet<PhoneProgram> PhonePrograms { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-FG8PGK26;Database=TelecomDB;Trusted_Connection=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__admins__43AB7D29977D2BB4");

            entity.HasOne(d => d.User).WithMany(p => p.Admins).HasConstraintName("FK__admins__user_id__02FC7413");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__bills__D706DDB36C98E416");

            entity.HasOne(d => d.Phone).WithMany(p => p.Bills).HasConstraintName("FK__bills__phone_id__05D8E0BE");

            entity.HasMany(d => d.Calls).WithMany(p => p.Bills)
                .UsingEntity<Dictionary<string, object>>(
                    "BillsCall",
                    r => r.HasOne<Call>().WithMany()
                        .HasForeignKey("CallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__billsCall__call___0B91BA14"),
                    l => l.HasOne<Bill>().WithMany()
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__billsCall__bill___0A9D95DB"),
                    j =>
                    {
                        j.HasKey("BillId", "CallId").HasName("PK__billsCal__43210155F96C213C");
                        j.ToTable("billsCalls");
                        j.IndexerProperty<int>("BillId").HasColumnName("bill_id");
                        j.IndexerProperty<int>("CallId").HasColumnName("call_id");
                    });
        });

        modelBuilder.Entity<Call>(entity =>
        {
            entity.HasKey(e => e.CallId).HasName("PK__calls__427DCE68463B2A94");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__clients__BF21A4244782D61C");

            entity.HasOne(d => d.Phone).WithMany(p => p.Clients).HasConstraintName("FK__clients__phone_i__00200768");

            entity.HasOne(d => d.User).WithMany(p => p.Clients).HasConstraintName("FK__clients__user_id__7F2BE32F");
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.HasKey(e => e.PhoneId).HasName("PK__phones__E6BD6DD7AA064F8D");

            entity.HasOne(d => d.Program).WithMany(p => p.Phones).HasConstraintName("FK__phones__program___7C4F7684");
        });

        modelBuilder.Entity<PhoneProgram>(entity =>
        {
            entity.HasKey(e => e.ProgramId).HasName("PK__phonePro__3A7890ACB2258270");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(e => e.SellerId).HasName("PK__sellers__780A0A9700FD2927");

            entity.HasOne(d => d.User).WithMany(p => p.Sellers).HasConstraintName("FK__sellers__user_id__778AC167");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F682AF6AE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
