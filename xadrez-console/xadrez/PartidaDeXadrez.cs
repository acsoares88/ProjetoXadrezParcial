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
        public Peca vulneravelEnPassant { get; private set; }



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
            vulneravelEnPassant = null;
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

            // Roque Pequeno
            if(p is Rei && posDestino.coluna == posOrigem.coluna + 2){
                Posicao origemTorre = new Posicao(posOrigem.linha, posOrigem.coluna + 3);
                Posicao destinoTorre = new Posicao(posOrigem.linha, posOrigem.coluna + 1);
                Peca t = tab.retirarPeca(origemTorre);
                t.incrementarQuantidadeMovimento();
                tab.colocarPeca(t, destinoTorre);

            }


            // Roque Grande
            if (p is Rei && posDestino.coluna == posOrigem.coluna - 2)
            {
                Posicao origemTorre = new Posicao(posOrigem.linha, posOrigem.coluna - 4);
                Posicao destinoTorre = new Posicao(posOrigem.linha, posOrigem.coluna - 1);
                Peca t = tab.retirarPeca(origemTorre);
                t.incrementarQuantidadeMovimento();
                tab.colocarPeca(t, destinoTorre);

            }

            // Jogada Especial en Passant

            if (p is Peao ) {
                if (posOrigem.coluna != posDestino.coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                    {
                        posP = new Posicao(posDestino.linha + 1, posDestino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(posDestino.linha - 1, posDestino.coluna);
                    }

                    pecaCapturada = tab.retirarPeca(posP);
                    pecasCapturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca){
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas() {

            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));
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

            Peca peca = tab.peca(destino);

            // Promocao Peao

            if (peca is Peao) {
                if (peca.cor == Cor.Branca && destino.linha == 0 || peca.cor == Cor.Preta && destino.linha == 7){
                    peca = tab.retirarPeca(destino);
                    pecas.Remove(peca);
                    Peca dama = new Dama(tab, peca.cor);
                    tab.colocarPeca(dama, destino);
                    pecas.Add(dama);
                }
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



            // Jogada especial en passant

            if (peca is Peao && (destino.linha == origem.linha - 2|| destino.linha == origem.linha + 2)) {
                vulneravelEnPassant = peca;
            }else {
                vulneravelEnPassant = null;
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

            // Roque Pequeno
            if (p is Rei && dest.coluna == origem.coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca t = tab.retirarPeca(destinoTorre);
                t.decrementarQuantidadeMovimento();
                tab.colocarPeca(t, origemTorre);

            }

            // Roque Pequeno
            if (p is Rei && dest.coluna == origem.coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna  -4 );
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);
                Peca t = tab.retirarPeca(destinoTorre);
                t.decrementarQuantidadeMovimento();
                tab.colocarPeca(t, origemTorre);

            }

            // jogada Especial een Passant

            if (p is Peao) {
                if (origem.coluna != dest.coluna && pecaCapturada == vulneravelEnPassant) {
                    Peca peao = tab.retirarPeca(dest);
                    Posicao posP;
                    if(p.cor == Cor.Branca){
                        posP = new Posicao(3, dest.coluna);
                    }else {
                        posP = new Posicao(4, dest.coluna);
                    }
                    tab.colocarPeca(peao, posP);
                }
            }

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
            if (tab.peca(origem).movimentoPossivel(destino) == false){
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
                if (mat[r.posicao.linha, r.posicao.coluna])
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
