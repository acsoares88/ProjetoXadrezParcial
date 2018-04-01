using System;
using tabuleiro;
using xadrez;
namespace xadrezconsole
{
     class Tela
    {
        public static void imprimeTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++){

                    if (tab.peca(i,j) == null) {
                        Console.Write("- ");
                    }else {
                        imprimirPeca(tab.peca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H ");
        }

        public static void imprimirPeca(Peca peca) {

            if (peca.cor == Cor.Preta) {
                Console.Write(peca);
            }else {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(peca);
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }

        public static PosicaoXadrez lerPosicaoXadrez(){
            string str = Console.ReadLine();

            char coluna = str[0];
            int linha = int.Parse(str[1] + "");

            return new PosicaoXadrez(coluna, linha);
        }
    }
}

