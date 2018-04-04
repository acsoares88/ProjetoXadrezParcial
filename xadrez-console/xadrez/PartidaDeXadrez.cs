using System;
using tabuleiro;

namespace xadrez
{
     class PartidaDeXadrez
    {
        public Tabuleiro tab { get; set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set;}
        public bool terminada { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            colocarPecas();
            terminada = false;
        }

        public void executaMovimento(Posicao posOrigem, Posicao posDestino){

            Peca p = tab.retirarPeca(posOrigem);
            p.incrementarQuantidadeMovimento();

            Peca pecaCapturada = tab.retirarPeca(posDestino);
            tab.colocarPeca(p, posDestino);
        }

        private void colocarPecas() {
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c',1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c',2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('d',2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e',2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e',1).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 1).toPosicao());

            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 8).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('d', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 8).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('d', 8).toPosicao());
        }

        public void realizaJogada(Posicao origem, Posicao destino){
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        public void mudaJogador(){
            if (jogadorAtual == Cor.Branca) {
                jogadorAtual = Cor.Preta;
            }else {
                jogadorAtual = Cor.Branca;
            }
        }

        public  void validarPosicaoDeOrigem(Posicao pos){

            if (tab.peca(pos) == null){
                throw new TabuleiroException("Não existe peça na posicao escolhida");
            }

            if (jogadorAtual != tab.peca(pos).cor){
                throw new TabuleiroException("A peca de origem escolhida não é a sua");
            }

            if (tab.peca(pos).exiteMovimentosPossiveis() == false)
            {
                throw new TabuleiroException("Não ecxiste jogada disponivel para peça selecionada");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino){
            if (tab.peca(origem).podeMoverPara(destino) == false){
                throw new TabuleiroException("Posicao de Destino inválida");
            }
        }
    }
}
