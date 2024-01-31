using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PizzaOrderAPI.Data.Services.ConfigurationServices;
using PizzaOrder.Data.Models;

namespace PizzaOrder.Data.Models
{
    public partial class PizzaOrderDBContext : DbContext
    {
        public PizzaOrderDBContext()
        {

        }

        public PizzaOrderDBContext(DbContextOptions<PizzaOrderDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Basket> Baskets { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Pizza> Pizzas { get; set; } = null!;
        public virtual DbSet<PizzaIngredient> PizzaIngredients { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(new ConfigurationService().GetMyConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>(entity =>
            {
                entity.ToTable("basket");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.CustomizedPizzaUuid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("customized_pizza_uuid");

                entity.Property(e => e.ExtraIngredientId).HasColumnName("extra_ingredient_id");

                entity.Property(e => e.PizzaId).HasColumnName("pizza_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("discount");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.DiscountRate).HasColumnName("discount_rate");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("ingredient");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Image)
                    .HasMaxLength(4096)
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Type)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.CustomizedPizza)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("customized_pizza");

                entity.Property(e => e.OrderedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ordered_at");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.TotalPrice).HasColumnName("total_price");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.ToTable("pizza");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Image)
                    .HasMaxLength(4096)
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PizzaIngredient>(entity =>
            {
                entity.ToTable("pizza_ingredient");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.PizzaId).HasColumnName("pizza_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Passwordhash)
                    .HasMaxLength(512)
                    .HasColumnName("passwordhash");

                entity.Property(e => e.Passwordsalt)
                    .HasMaxLength(512)
                    .HasColumnName("passwordsalt");

                entity.Property(e => e.Surname)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("surname");

                entity.Property(e => e.Token)
                    .HasMaxLength(512)
                    .IsUnicode(false)
                    .HasColumnName("token");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
