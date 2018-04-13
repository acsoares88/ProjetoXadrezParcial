using System;
using tabuleiro;
using xadrez;
using System.Collections.Generic;


namespace xadrezconsole

{
     class Tela
    {

        public static void imprimirPartida(PartidaDeXadrez partida){

            imprimeTabuleiro(partida.tab);
            Console.WriteLine();

            imprimirPecasCapturadas(partida);

            Console.WriteLine();
            Console.WriteLine(" Turno: " + partida.turno);
            Console.WriteLine(" Aguardando jogada: " + partida.jogadorAtual);
            Console.WriteLine();

            if (partida.xeque)
            {
                Console.WriteLine("Xeque");
            }
        }


        public static void imprimirPecasCapturadas(PartidaDeXadrez partida){




            Console.WriteLine("Pecas capturadas: ");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas, Cor.Branca);
            Console.Write("       ");
            Console.Write("Pretas: ");
            imprimirConjunto(partida.pecasCapturadas, Cor.Preta);
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto, Cor cor)
        {
            Console.Write("[");
            foreach(Peca x in conjunto){
                if (x.cor.Equals(cor))
                {
                    Console.Write(x + " ");
                }
            }
            Console.Write("]");
        }


        public static void imprimeTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++){
                    imprimirPeca(tab.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H ");
        }

        public static void imprimirPeca(Peca peca) {


            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {

                if (peca.cor == Cor.Preta)
                {
                    Console.Write(peca);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(peca);
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write(" ");
            }
        }

        public static PosicaoXadrez lerPosicaoXadrez(){
            string str = Console.ReadLine();

            char coluna = str[0];
            int linha = int.Parse(str[1] + "");

            return new PosicaoXadrez(coluna, linha);
        }

        public static void imprimeTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.Yellow;

            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if(posicoesPossiveis[i,j]){
                        Console.BackgroundColor = fundoAlterado;
                    }else {
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H ");
            Console.BackgroundColor = ConsoleColor.White;
        }
    }
}

