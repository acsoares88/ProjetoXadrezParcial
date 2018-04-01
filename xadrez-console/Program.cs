using System;
using tabuleiro;
using xadrez;

namespace xadrezconsole
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
                tab.colocarPeca(new Rei(tab, Cor.Branca), new Posicao(6, 4));

                Tela.imprimeTabuleiro(tab);

            }catch(TabuleiroException e){
                Console.WriteLine(e.Message);
            }

            //PosicaoXadrez posicaoXadrez = new PosicaoXadrez('a', 1);
            //Console.WriteLine(posicaoXadrez);

            //Console.WriteLine(posicaoXadrez.toPosicao());

        }
    }
}
