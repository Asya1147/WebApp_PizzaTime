using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp_PizzaTime;

public partial class WebAppPizzatimeContext : DbContext
{
    public WebAppPizzatimeContext()
    {
    }

    public WebAppPizzatimeContext(DbContextOptions<WebAppPizzatimeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrdersPizza> OrdersPizzas { get; set; }

    public virtual DbSet<Pizza> Pizzas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\WebApp_PizzaTime\\MyDatabase.mdf;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Customer)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("customer");
            entity.Property(e => e.Summa).HasColumnName("summa");
        });

        modelBuilder.Entity<OrdersPizza>(entity =>
        {
            entity.ToTable("OrdersPizza");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Pizzaid).HasColumnName("pizzaid");

            entity.HasOne(d => d.Order).WithMany(p => p.OrdersPizzas)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdersPizza_ToOrders");

            entity.HasOne(d => d.Pizza).WithMany(p => p.OrdersPizzas)
                .HasForeignKey(d => d.Pizzaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdersPizza_ToPizza");
        });

        modelBuilder.Entity<Pizza>(entity =>
        {
            entity.ToTable("Pizza");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(13)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
