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
                PartidaDeXadrez partida = new PartidaDeXadrez(); 

                while (partida.terminada == false) {
                    Console.Clear();
                    Tela.imprimeTabuleiro(partida.tab);

                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();

                    bool[,] possicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                    Console.Clear();
                    Tela.imprimeTabuleiro(partida.tab,possicoesPossiveis);

                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                    partida.executaMovimento(origem, destino);

                }

               
                Tela.imprimeTabuleiro(partida.tab);

            }catch(TabuleiroException e){
                Console.WriteLine(e.Message);
            }

            //PosicaoXadrez posicaoXadrez = new PosicaoXadrez('a', 1);
            //Console.WriteLine(posicaoXadrez);

            //Console.WriteLine(posicaoXadrez.toPosicao());

        }
    }
}
