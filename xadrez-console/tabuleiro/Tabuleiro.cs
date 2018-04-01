﻿using System;
namespace tabuleiro
{
     class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            this.pecas = new Peca[linhas, colunas];
        }

        public Peca peca (int linha, int coluna){
            return pecas[linha, coluna];
        }

        public Peca peca (Posicao pos) {
            return pecas[pos.linha, pos.coluna];
        }

        public void colocarPeca(Peca p , Posicao pos) {

            if (existePeca(pos) == false) {
                pecas[pos.linha, pos.coluna] = p;
                p.posicao = pos;
            }else {
                throw new TabuleiroException("Já existe uma peca nessa posicao");
            }

        }

        public bool posicaoValida(Posicao pos) {
            if (pos.linha < 0 || pos.linha >= linhas || pos.coluna <0 || pos.coluna >= colunas) {
                return false;
            }

            return true;
        }

        public void validarPosicao(Posicao pos) {

            if (posicaoValida(pos) == false) {
                throw new TabuleiroException("Posicao Inválida!!");
            }
        }

        public bool existePeca(Posicao pos)
        {

            validarPosicao(pos);

            if (peca(pos) != null) {
                return true;
            }

            return false;

            //return peca(pos) != null;
        }
    }
}