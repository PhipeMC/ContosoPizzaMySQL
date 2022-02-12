using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ContosoPizza.Models;

namespace ContosoPizza.Data
{
    public partial class ContosoPizzaContext : DbContext
    {
        public ContosoPizzaContext()
        {
        }

        public ContosoPizzaContext(DbContextOptions<ContosoPizzaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pizza> Pizzas { get; set; } = null!;
        public virtual DbSet<Sauce> Sauces { get; set; } = null!;
        public virtual DbSet<Topping> Toppings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.ToTable("pizza");

                entity.HasIndex(e => e.SauceId, "sauceId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.SauceId).HasColumnName("sauceId");

                entity.HasOne(d => d.Sauce)
                    .WithMany(p => p.Pizzas)
                    .HasForeignKey(d => d.SauceId)
                    .HasConstraintName("pizza_ibfk_1");

                entity.HasMany(d => d.Toppings)
                    .WithMany(p => p.Pizzas)
                    .UsingEntity<Dictionary<string, object>>(
                        "Pizzatopping",
                        l => l.HasOne<Topping>().WithMany().HasForeignKey("ToppingsId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("pizzatopping_ibfk_2"),
                        r => r.HasOne<Pizza>().WithMany().HasForeignKey("PizzasId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("pizzatopping_ibfk_1"),
                        j =>
                        {
                            j.HasKey("PizzasId", "ToppingsId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("pizzatopping");

                            j.HasIndex(new[] { "ToppingsId" }, "ToppingsId");
                        });
            });

            modelBuilder.Entity<Sauce>(entity =>
            {
                entity.ToTable("sauce");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsVegan)
                    .HasColumnType("bit(1)")
                    .HasColumnName("isVegan");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Topping>(entity =>
            {
                entity.ToTable("topping");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Calories)
                    .HasColumnType("text")
                    .HasColumnName("calories");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
