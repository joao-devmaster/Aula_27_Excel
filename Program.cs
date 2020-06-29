using System;

namespace Aula27_dados_excel
{
    class Program
    {
        static void Main(string[] args)
        {
             Produto p = new Produto();
            p.Codigo = 1;
            p.Nome = "AudioBook Revolta de Atlas";
            p.Preco = 21.98f;

            p.Inserir(p);
        }
    }
}
