using ClienteNet6.Shared.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.Extensions.Configuration;

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

    public class AppGerVeiculosContext : DbContext
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
            optionsBuilder.UseMySql(
                _Configuration.GetConnectionString("DbGerVeiculos"),
                ServerVersion.AutoDetect(_Configuration.GetConnectionString("DbGerVeiculos"))
            );

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasCharSet("latin1");

            builder.Entity<Veiculo>().HasCharSet("latin1");
            builder.Entity<Veiculo>().HasIndex(u => u.Chassi).IsUnique();
            builder.Entity<Veiculo>().HasIndex(u => u.Placa);
            builder.Entity<Infracao>().HasCharSet("latin1");

            base.OnModelCreating(builder);
        }
    }
}
