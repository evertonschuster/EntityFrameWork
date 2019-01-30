using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{    
    //Include()     -> pega tabela relacionada
    //ThenInclude() -> pegar tabelas em um segundo nivel (segundo... JOIN)
    //Load()        -> carrega os objetos relacionado, que ainda nao foram caregados, (Objetos internos)
    class Program
    {
        static void Main(string[] args)
        {
            RecuperarDadosRelacionadosUmParaUm();
            Console.ReadLine();
        }


        private static void RecuperarDadosRelacionadosUmParaUm()
        {
            using (var contexto = new LojaContext())
            {
                var cliente = contexto.Clientes.Include(c => c.EndereDeEntrega).FirstOrDefault();//faz o JOIN na tabela de Enderecos
                Console.WriteLine($"Enderco de entrega {cliente.EndereDeEntrega.Logradouro}");

                ////Recupera as compras de um produto
                var produto = contexto.Produtos.Include(p => p.Compras).FirstOrDefault();
                foreach (var item in produto.Compras)
                {
                    Console.WriteLine($"Compas do produto {item.Produto.Nome}");
                }



                Console.WriteLine($"==============================================");
                //filtra pelo valor compra
                produto = contexto.Produtos.FirstOrDefault(); //recupera o produto, sem o Include

                contexto.Entry(produto)             //para as entidades da referencia produto
                    .Collection(p => p.Compras)     //pega a colecao de Compras
                    .Query()                        //realiza uma consulta  
                    .Where(c => c.Preco > 9)       //com a WHERE
                    .Load();                        //carrega para a referencia de produtos
                //foi removido o INNER no primeiro momento, para poder realizar a consulta no INNER com filtro
                //ja que as compras nao foram carregas de cara, nao ah diferencao de desempenho

                foreach (var item in produto.Compras)
                {
                    Console.WriteLine($"Compas do produto {item.Produto.Nome}");
                }
            }
        }
        private static void RecuperarDadosRelacionadosMuitosParaMuitos()
        {
            //using (var contexto = new LojaContext())
            //{

            //    var promocao = new Promocao();
            //    promocao.Descricao = "Queima de Estoque";
            //    promocao.DataInicio = new DateTime(2017, 1, 1);
            //    promocao.DataTermino = new DateTime(2017, 1, 30);

            //    var produtos = contexto.Produtos.Where(p => p.Categoria == "Bebidas").ToList();//Select com WHERE

            //    foreach (var item in produtos )
            //    {
            //        promocao.IncluiProduto(item);
            //    }

            //    contexto.Promocaos.Add(promocao);
            //    contexto.SaveChanges();
            //}

            using (var contexto = new LojaContext())
            {
                var promocao = contexto.Promocaos
                    .Include(p => p.Produtos) //Include Desce um nivel no relacionamento
                    .ThenInclude(pp => pp.Produto)//Os proximos Join deve usar ThenInclude
                    .FirstOrDefault();
                foreach (var item in promocao.Produtos )
                {
                    Console.WriteLine(item.Produto.Nome);
                }
            }

        }

        private static void SalvarUmParaUm()
        {
            var fulano = new Cliente();
            fulano.Nome = "Fulaninho";
            fulano.EndereDeEntrega = new Endereco()
            {
                Numero = 12,
                Logradouro = "Rua dos Invalidos",
                Complemento = "Sobrado",
                Bairro = "Centro",
                Cidade = "Cidade"
            };

            using (var contexto = new LojaContext())
            {
                contexto.Clientes.Add(fulano);
                contexto.SaveChanges();
            }

        }


        private static void SalvarMuitoParaMuitos()
        {
            var p1 = new Produto();
            var p2 = new Produto();
            var p3 = new Produto();

            var promocaoDePasco = new Promocao();
            promocaoDePasco.Descricao = "Pascoa Feliz";
            promocaoDePasco.DataInicio = DateTime.Now;
            promocaoDePasco.DataTermino = DateTime.Now;
            promocaoDePasco.IncluiProduto(p1);
            promocaoDePasco.IncluiProduto(p2);
            promocaoDePasco.IncluiProduto(p3);

            using (var contexto = new LojaContext())
            {
                contexto.Promocaos.Add(promocaoDePasco);
                contexto.SaveChanges();
            }

        }

        private static void SalvarUmParaMuitos()
        {
            var paoFrances = new Produto();
            paoFrances.Nome = "Pao frances";
            paoFrances.PrecoUnidade = 0.40;
            paoFrances.Unidade = "Unidade";
            paoFrances.Categoria = "Padaria";

            var compra = new Compra();
            compra.Quantidade = 6;
            compra.Produto = paoFrances;
            compra.Preco = paoFrances.PrecoUnidade * compra.Quantidade;

            using (var contexto = new LojaContext())
            {
                contexto.Compras.Add(compra);
                contexto.SaveChanges();

            }
        }


        private static void getLogger()
        {
            using (var context = new LojaContext())
            {
                var serviceProvider = context.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var produtos = context.Produtos.ToList();

                foreach (var item in produtos)
                {

                }
            }

        }


        private static void AtualizarProduto()
        {
            using (var context = new ProdutoDAOEntity())
            {
                Produto produtos = context.Produtos()[0];

                produtos.Nome = "Teste test3333e";

                context.Atualizar(produtos);
            }
        }

        private static void RemoverProdutos()
        {
            using (var context = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = context.Produtos();

                context.Remover (produtos[0]);

            }
        }

        private static void RecuperarProdutos()
        {
            using (var context = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = context.Produtos();

                foreach (var item in produtos)
                {
                    Console.WriteLine(item.Nome);
                }
            }
        }

        private static void GravarUsandoEntity()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.PrecoUnidade = 19.89;

            using (var context = new ProdutoDAOEntity())
            {
                context.Adicionar(p);
            }
        }

        private static void GravarUsandoAdoNet()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.PrecoUnidade = 19.89;

            using (var repo = new ProdutoDAO())
            {
                repo.Adicionar(p);
            }
        }
    }
}
