﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Webhooks.Data;

#nullable disable

namespace Webhooks.Migrations
{
    [DbContext(typeof(WebhooksDbContext))]
    [Migration("20250114133326_Create_Database")]
    partial class Create_Database
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Webhooks.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("Webhooks.Models.WebhookDeliveryAttempt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Payload")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ResponseStatusCode")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Success")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("WebhookSubscriptionId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("WebhookSubscriptionId");

                    b.ToTable("delivery_attempts", "webhooks");
                });

            modelBuilder.Entity("Webhooks.Models.WebhookSubscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateOnUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WebhookUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("subscriptions", "webhooks");
                });

            modelBuilder.Entity("Webhooks.Models.WebhookDeliveryAttempt", b =>
                {
                    b.HasOne("Webhooks.Models.WebhookSubscription", null)
                        .WithMany()
                        .HasForeignKey("WebhookSubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
