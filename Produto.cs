using System.Collections.Generic;
using System.IO;
using System;

namespace Aula27_28_29_30
{
    public class Produto
    {
        
        public int Codigo {get;set;}
        public string Nome {get;set;}
        public float Preco {get;set;}

        private const string PATH = "Database/Produto.csv";

        public Produto()
        {

            //DESAFIO
            string pasta = PATH.Split('/')[0]; //Criando array com o arquivo Database

            if(!Directory.Exists(pasta))//Se o diretório não existe, crie o diretório
            {
                Directory.CreateDirectory(pasta);
            }


            if(!File.Exists(PATH))//criando pasta Database manualmente
            {
                File.Create(PATH).Close();
            }
        
        }

        /// <summary>
        /// Adicionando novos produtos
        /// </summary>
        /// <param name="p"></param>
        public void Inserir(Produto p){
            var linha = new string [] {p.PrepararLinhaCSV(p)};
            File.AppendAllLines(PATH, linha);
        }

        /// <summary>
        /// Organizar os dados para ficarem em linhas (codigo=x; nome=a; preco=0)
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        private string PrepararLinhaCSV(Produto produto){

            return $"Cod= {produto.Codigo}; Nome = {produto.Nome}; Preco = {produto.Preco}";
        }

        //-------------------------------------------------------
        //Aula28 - Ler Dados Excel

        public List<Produto> Ler(){

            //Criamos uma lista de produtos
            List<Produto> produto1 = new List<Produto>();

            //Transformamos as linhas encontradas em uma array de strings
            string [] lista = File.ReadAllLines(PATH);

            //Varremos este array de strings
            foreach(var linha in lista){

                //Quebramos cada linha da lista em partes
                string[] dados = linha.Split(';'); //dividindo a string em cada ;

                //Tratamento dos dados e adicionar um novo produto
                Produto prod = new Produto();
                prod.Codigo = Int32.Parse (SepararDados(dados[0]));
                prod.Nome   = SepararDados(dados[1]);
                prod.Preco  = float.Parse (SepararDados(dados[2]));

                //Adicionar o produto tratado na lista                
                produto1.Add(prod);

            }

            return produto1;

        }

        /// <summary>
        /// Remove uma ou mais linhas que contenham o termo
        /// </summary>
        /// <param name="_termo">termo para ser buscado</param>
        public void Remover(string _termo){

            // Criamos uma lista que servirá como uma espécie de backup para as linhas do csv
            List<string> linhas = new List<string>();

            // Utilizamos a bliblioteca StreamReader para ler nosso .csv
            using(StreamReader arquivo = new StreamReader(PATH))
            {
                string linha;
                while((linha = arquivo.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }

            // Removemos as linhas que tiverem o termo passado como argumento
            // Cod= 1; Nome = AudioBook Revolta de Atlas; Preco = 21,98
            // AudioBook Revolta de Atlas 
            linhas.RemoveAll(l => l.Contains(_termo));

            // Reescrevemos nosso csv do zero
            using(StreamWriter output = new StreamWriter(PATH))
            {
                foreach(string ln in linhas)
                {
                    output.Write(ln + "\n");
                }
            }
        }



        /// <summary>
        /// Busca de produto
        /// </summary>
        /// <param name="_nome"></param>
        /// <returns></returns>
        public List<Produto> Filtrar(string _nome){
            return Ler().FindAll( a => a.Nome == _nome);
        }


        /// <summary>
        /// Tratamento da array lista para obter somente nome, preço e código
        /// </summary>
        /// <param name="_coluna"></param>
        /// <returns></returns>
        private string SepararDados(string _coluna){
            
            //cada informação esta assim
            //0       1
            //Preco = The Last Of Us II
            return _coluna.Split("=")[1];

        }

    
    }
}