namespace Alura.Loja.Testes.ConsoleApp
{
    public class Endereco
    {
        //public int Id { get; set; } Como endereco depende de Cliente 1 para 1, entao endereco nao presa de chave primaria,  a PK de cliente faz isso , e isso e definido na LojaContexto
        public int Numero { get; internal set; }
        public string Logradouro { get; internal set; }
        public string Complemento { get; internal set; }
        public string Bairro { get; internal set; }
        public string Cidade { get; internal set; }
        public Cliente Cliente { get; set; }
    }
}