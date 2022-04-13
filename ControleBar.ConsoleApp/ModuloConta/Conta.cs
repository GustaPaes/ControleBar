using ControleBar.ConsoleApp.Compartilhado;
using ControleBar.ConsoleApp.ModuloGarcom;
using ControleBar.ConsoleApp.ModuloMesa;
using ControleBar.ConsoleApp.ModuloPedido;
using System;
using System.Collections.Generic;

namespace ControleBar.ConsoleApp.ModuloConta
{
    public class Conta : EntidadeBase
    {
        private Garcom GarcomE;
        private Mesa Mesa;
        private Pedido Pedidos;
        public string Garcom { get; set; }
        public decimal ValorConta { get; set; } = 0m;

        public Conta(Garcom garcom, Mesa mesa)
        {
            GarcomE = garcom;
            Mesa = mesa;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Mesa: " + Mesa + Environment.NewLine +
                "Garçom que atendeu: " + GarcomE + Environment.NewLine +
                "Pedido: " + Pedidos + Environment.NewLine +
                "Valor Conta: R$" + ValorConta + Environment.NewLine;
        }

        public void ReceberValorConta(decimal ValorContaCalculada)
        {
            ValorConta += ValorContaCalculada;
        }
    }
}
