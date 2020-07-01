
using System;
using System.Collections.Generic;
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




       
       // 2 aula  
        private string Separar(string _coluna)
        {
            // 0         1
            //nome   =  gibson
            return _coluna.Split("=") [1];
        }

        // 1; celular;600
        private string PrepararLinha(Produto p)
        {
            return $"codigo={p.Codigo};nome={p.Nome};={p.Preco}";
        }












        public List<Produto> Ler()
        {
            //Criamos uma lista de produtos
        List<Produto> produtos = new List<Produto>();

        // Transformamos as linhas encontradas em um array de strings
        string[] linhas = File.ReadAllLines(PATH);

        // Varremos este array de strings
        foreach (var linha in linhas)
        {
             // quebramos cada linha em partes, pegando
             string[] dados = linha.Split(";");

             // Tratamos os dados e adicionamos em um novo produto
             Produto prod = new Produto();
             prod.Codigo = Int32.Parse( Separar(dados[0]) );
             prod.Nome = Separar(dados[1]);
             prod.Preco = float.Parse( Separar(dados[2]) );
             

             // agora adicionamos o produto tratado na lista de produtos a ntes de retorná-la
             produtos.Add(prod);

        } 
        return produtos;

        }

       
 
        




        

        

         



       
        
   







    }
}