using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
     class PartidaDeXadrez
    {
        public Tabuleiro tab { get; set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set;}
        public bool terminada { get; private set; }
        public  HashSet<Peca> pecas;
        public HashSet<Peca> pecasCapturadas;


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            pecasCapturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void executaMovimento(Posicao posOrigem, Posicao posDestino){

            Peca p = tab.retirarPeca(posOrigem);
            p.incrementarQuantidadeMovimento();

            Peca pecaCapturada = tab.retirarPeca(posDestino);
            tab.colocarPeca(p, posDestino);

            if(pecaCapturada != null) 
            {
                pecasCapturadas.Add(pecaCapturada);
            }
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca){
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas() {

            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));


            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
        }

        public HashSet<Peca> pecasCapturadasPorCor(Cor cor){
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca x in pecasCapturadas) {
                if (x.cor.Equals(cor)){
                    aux.Add(x);
                }
            }

            return aux;
        }

        public HashSet<Peca> pecasEmJogoPorCor(Cor cor){
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor.Equals(cor))
                {
                    aux.Add(x);
                }
            }

            aux.ExceptWith(pecasCapturadasPorCor(cor));

            return aux;
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
