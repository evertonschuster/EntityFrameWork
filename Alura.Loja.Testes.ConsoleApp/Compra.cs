namespace Alura.Loja.Testes.ConsoleApp
{
    internal class Compra
    {
        public int Id { get; set; }
        public int Quantidade { get; internal set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; internal set; }//serve enetender que o Produto e Not NULL
        public double Preco { get; internal set; }
    }
}