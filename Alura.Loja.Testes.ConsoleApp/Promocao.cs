using System;
using System.Collections.Generic;

namespace Alura.Loja.Testes.ConsoleApp
{
    internal class Promocao
    {
        public Promocao()
        {
            this.Produtos = new List<PromocaoProduto>();
        }

        public int Id { get; set; }
        public string Descricao { get; internal set; }
        public DateTime DataInicio { get; internal set; }
        public DateTime DataTermino { get; internal set; }

        public IList<PromocaoProduto> Produtos { get; set; }
        //este metodo existe, para nao usar a propriedade Produtos no codigo
        internal void IncluiProduto(Produto p)
        {
            this.Produtos.Add(new PromocaoProduto() { Produto = p } );
        }
    }
}