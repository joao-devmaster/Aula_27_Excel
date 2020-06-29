using System.IO;

namespace Aula27_dados_excel
{
    public class Produto
    {
           public int Codigo {get;set;}
        public string Nome {get;set;}
        public float Preco {get;set;}

        private const string PATH = "Database/Produto.csv";

        //criando pasta Database manualmente
        public Produto()
        {

            if(!File.Exists(PATH))
            {
                File.Create(PATH).Close();
            }
        
        }

        public void Inserir(Produto p){
            var linha = new string [] {p.PrepararLinhaCSV(p)};
            File.AppendAllLines(PATH, linha);
        }

        private string PrepararLinhaCSV(Produto produto){

            return $"Cod= {produto.Codigo}; Nome = {produto.Nome}; Preco = {produto.Preco}";
        }
    }
}