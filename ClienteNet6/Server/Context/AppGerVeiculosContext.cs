using ClienteNet6.Shared.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.Extensions.Configuration;
using ClienteNet6.Server.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ClienteNet6.Server.Context
{
    /*
     
    Commands:

    CODE FIRST

    Add-Migration [name]
        Gera classes de Migrations

    Remove-Migration
        Remove migration antes de update-database

    Script-Migration -Idempotent
        Geração de scripts não aplicados

    Script-Migration [name]
        Migração mais recente

     
    TABLE FIRST

    Scaffold-DbContext [-Connection] <String> [-Provider] <String> [-OutputDir <String>] 
    [-ContextDir <String>] [-Context <String>] [-Schemas <String[]>] [-Tables <String[]>] 
    [-DataAnnotations] [-UseDatabaseNames] [-Force] [-NoOnConfiguring] [-Project <String>] 
    [-StartupProject <String>] [-Namespace <String>] [-ContextNamespace <String>] [-NoPluralize]
    [-Args <String>] [<CommonParameters>]

     */

    public class AppGerVeiculosContext : IdentityDbContext<User, Role, int,
                                            UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IConfiguration _Configuration;
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Infracao> Infracoes { get; set; }

        public AppGerVeiculosContext(IConfiguration configurantion, [System.Diagnostics.CodeAnalysis.NotNull] DbContextOptions options) : base(options)
        {
            _Configuration = configurantion;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql(
                _Configuration.GetConnectionString("DbGerVeiculos"),
                ServerVersion.AutoDetect(_Configuration.GetConnectionString("DbGerVeiculos"))
            );

            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasCharSet("latin1");

            builder.Entity<Veiculo>().HasCharSet("latin1");
            builder.Entity<Veiculo>().HasIndex(u => u.Chassi).IsUnique();
            builder.Entity<Veiculo>().HasIndex(u => u.Placa);
            builder.Entity<Infracao>().HasCharSet("latin1");

            // identity
            builder.Entity<User>().HasCharSet("latin1");
            builder.Entity<Role>().HasCharSet("latin1");
            builder.Entity<UserToken>().HasCharSet("latin1");
            builder.Entity<UserLogin>().HasCharSet("latin1");
            builder.Entity<RoleClaim>().HasCharSet("latin1");
            builder.Entity<UserRole>().HasCharSet("latin1");
            builder.Entity<UserClaim>().HasCharSet("latin1");

        }
    }
}
