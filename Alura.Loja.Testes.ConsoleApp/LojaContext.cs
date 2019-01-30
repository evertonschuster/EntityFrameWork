using Microsoft.EntityFrameworkCore;
using System;

namespace Alura.Loja.Testes.ConsoleApp
{

    //COMANDOS
    //  Install-Package Microsoft.EntryFramework.Tools
    //  Add-Migration
    //  Remove-Migration
    //  Update-Database


    //Include()     -> pega tabela relacionada
    //ThenInclude() -> pegar tabelas em um segundo nivel (segundo... JOIN)
    //Load()        -> carrega os objetos relacionado, que ainda nao foram caregados, (Objetos internos)

    //+--------+ 1          *    +----------------+
    //|Promocao| --------------  |Promocao produto|
    //+--------+                 +----------------+
    //                                    | *                                                 
    //                                    |
    //                                    |
    //                                    |1
    //+--------+ *            1  +----------------+
    //|Compra  | --------------  |Produto         |
    //+--------+                 +----------------+
    //
    //
    //+--------+ 1            0  +----------------+
    //|Cliente | --------------  |Endereco        |
    //+--------+                 +----------------+

    internal class LojaContext : DbContext
    {
        //        Objeto \/  tabela\/  
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Compra> Compras{ get; set; }
        public DbSet<Promocao> Promocaos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //declaracao das chaves compostas da PromocaoProduto (Procamo, Produto)
            modelBuilder.Entity<PromocaoProduto>().HasKey(pp => new { pp.ProdutoId, pp.PromocaoId });



            //esta dizendo que a tabela Endereco possui uma coluna de ClienteId, e que nao esta mapeada na Clasee
            //e neste caso precisa explicitar o tipo da coluna, no caso <INT>
            modelBuilder.Entity<Endereco>().Property<int>("ClienteId");
            //Agr esta dizendo quem e a chave Primaria da tabela
            modelBuilder.Entity<Endereco>().HasKey("ClienteId");


            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LojaDB;Trusted_Connection=true;");
        }

    }
}