using System;
using tabuleiro;

namespace xadrezconsole
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            Tela.imprimeTabuleiro(tab);

        }
    }
}
