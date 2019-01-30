using System.Collections.Generic;

namespace Alura.Loja.Testes.ConsoleApp
{
    internal class Produto
    {
        public int Id { get; internal set; }
        public string Nome { get; internal set; }
        public string Categoria { get; internal set; }
        public string  Unidade { get; internal set; }
        //public double Preco { get; internal set; }
        public double PrecoUnidade { get; internal set; }
        public IList<PromocaoProduto> Promocoes { get; set; }
        public IList<Compra> Compras { get; set; }
    }
}
