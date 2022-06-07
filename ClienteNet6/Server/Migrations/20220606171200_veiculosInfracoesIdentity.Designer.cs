﻿// <auto-generated />
using System;
using ClienteNet6.Server.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClienteNet6.Server.Migrations
{
    [DbContext(typeof(AppGerVeiculosContext))]
    [Migration("20220606171200_veiculosInfracoesIdentity")]
    partial class veiculosInfracoesIdentity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "latin1");

            modelBuilder.Entity("ClienteNet6.Server.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    MySqlEntityTypeBuilderExtensions.HasCharSet(b, "latin1");
                });

            modelBuilder.Entity("ClienteNet6.Server.Identity.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);

                    MySqlEntityTypeBuilderExtensions.HasCharSet(b, "latin1");
                });

            modelBuilder.Entity("ClienteNet6.Server.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NomeEmpresa")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    MySqlEntityTypeBuilderExtensions.HasCharSet(b, "latin1");
                });

            modelBuilder.Entity("ClienteNet6.Server.Identity.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);

                    MySqlEntityTypeBuilderExtensions.HasCharSet(b, "latin1");
                });

            modelBuilder.Entity("ClienteNet6.Server.Identity.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);

                    MySqlEntityTypeBuilderExtensions.HasCharSet(b, "latin1");
                });

            modelBuilder.Entity("ClienteNet6.Server.Identity.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    MySqlEntityTypeBuilderExtensions.HasCharSet(b, "latin1");
                });

            modelBuilder.Entity("ClienteNet6.Server.Identity.UserToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);

                    MySqlEntityTypeBuilderExtensions.HasCharSet(b, "latin1");
                });

            modelBuilder.Entity("ClienteNet6.Shared.EntityFramework.Infracao", b =>
                {
                    b.Property<string>("Ait")
                        .HasColumnType("varchar(25)");

                    b.Property<DateTime>("Emissao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Renavam")
                        .HasColumnType("int(17)");

                    b.Property<DateTime>("Validade")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Ait");

                    b.HasIndex("Renavam");

                    b.ToTable("Infracao");

                    MySqlEntityTypeBuilderExtensions.HasCharSet(b, "latin1");
                });

            modelBuilder.Entity("ClienteNet6.Shared.EntityFramework.Veiculo", b =>
                {
                    b.Property<int>("Renavam")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(17)");

                    b.Property<string>("Chassi")
                        .IsRequired()
                        .HasColumnType("char(17)");

                    b.Property<string>("Cor")
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Modelo")
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("char(7)");

                    b.HasKey("Renavam");

                    b.HasIndex("Chassi")
                        .IsUnique();

                    b.HasIndex("Placa");

                    b.ToTable("Veiculo");

                    MySqlEntityTypeBuilderExtensions.HasCharSet(b, "latin1");
                });

            modelBuilder.Entity("ClienteNet6.Server.Identity.RoleClaim", b =>
                {
                    b.HasOne("ClienteNet6.Server.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClienteNet6.Server.Identity.UserClaim", b =>
                {
                    b.HasOne("ClienteNet6.Server.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClienteNet6.Server.Identity.UserLogin", b =>
                {
                    b.HasOne("ClienteNet6.Server.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClienteNet6.Server.Identity.UserRole", b =>
                {
                    b.HasOne("ClienteNet6.Server.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClienteNet6.Server.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClienteNet6.Server.Identity.UserToken", b =>
                {
                    b.HasOne("ClienteNet6.Server.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClienteNet6.Shared.EntityFramework.Infracao", b =>
                {
                    b.HasOne("ClienteNet6.Shared.EntityFramework.Veiculo", "Veiculo")
                        .WithMany("Infracoes")
                        .HasForeignKey("Renavam")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Veiculo");
                });

            modelBuilder.Entity("ClienteNet6.Shared.EntityFramework.Veiculo", b =>
                {
                    b.Navigation("Infracoes");
                });
#pragma warning restore 612, 618
        }
    }
}
