using System;
using tabuleiro;

namespace xadrezconsole
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Posicao p;

            p = new Posicao(3, 4);
            Console.WriteLine (p.ToString());
        }
    }
}
