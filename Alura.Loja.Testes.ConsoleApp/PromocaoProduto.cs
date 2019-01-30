using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class PromocaoProduto
    {
        //A chave primaria e definida no Contexto (LojaContexto) pois se trata de uma chave composta
        public int ProdutoId { get; set; }//serve enetender que o Produto e Not NULL
        public Produto Produto{ get; set; }
        public int PromocaoId { get; set; }//serve enetender que o Promocao e Not NULL
        public Promocao Promocao{ get; set; }
    }
}
