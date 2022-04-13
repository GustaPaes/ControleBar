using ControleBar.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace ControleBar.ConsoleApp.ModuloPedido
{
    public class Pedido : EntidadeBase
    {
        public string Mesa { get; set; }
        public string Pedidoo { get; set; }

        public Pedido(string pedido)
        {
            Pedidoo = pedido;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Pedido: " + Pedidoo + Environment.NewLine;
        }
    }
}
