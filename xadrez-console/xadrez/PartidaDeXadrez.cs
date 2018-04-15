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
        public bool xeque { get; private set; }



        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            pecasCapturadas = new HashSet<Peca>();
            colocarPecas();
            xeque = false;
        }

        public Peca executaMovimento(Posicao posOrigem, Posicao posDestino){

            Peca p = tab.retirarPeca(posOrigem);
            p.incrementarQuantidadeMovimento();

            Peca pecaCapturada = tab.retirarPeca(posDestino);
            tab.colocarPeca(p, posDestino);

            if(pecaCapturada != null) 
            {
                pecasCapturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
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
           Peca pecaCapturada = executaMovimento(origem, destino);

            if (estaEmCheque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Vc nao pode se colocar me xeque!");
            }

            if (estaEmCheque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }else
            {
                xeque = false;
            }

            if (testeXequeMate(adversaria(jogadorAtual))){
                terminada = true;
            }else {
                turno++;
                mudaJogador();  
            }

        }

        public void desfazMovimento(Posicao origem, Posicao dest, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(dest);
            p.decrementarQuantidadeMovimento();

            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, dest);
                pecasCapturadas.Remove(pecaCapturada);
            }

            tab.colocarPeca(p, origem);

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

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }

            return Cor.Branca;

        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogoPorCor(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }

            return null;
        }

        public bool estaEmCheque(Cor cor)
        {
            Peca r = rei(cor);

            if (r == null) { 
              throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro");
            }

            foreach (Peca x in pecasEmJogoPorCor(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();

                if(mat[r.posicao.linha, r.posicao.coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool testeXequeMate(Cor cor){
            
            if (estaEmCheque(cor) == false) {
                return false;
            }

            foreach(Peca x in pecasEmJogoPorCor(cor)){
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++){
                    for (int j = 0; j < tab.colunas; j++){

                        if (mat[i,j]){
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca auxPecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmCheque(cor);
                            desfazMovimento(origem, destino, auxPecaCapturada);
                            if (testeXeque == false) {
                                return false;
                            }
                        }

                    }

                }
            }

            return true;

        }
    }
}
