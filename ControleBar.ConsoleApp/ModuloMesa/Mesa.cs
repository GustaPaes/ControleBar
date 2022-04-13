using ControleBar.ConsoleApp.Compartilhado;
using ControleBar.ConsoleApp.ModuloGarcom;
using System;
using System.Collections.Generic;

namespace ControleBar.ConsoleApp.ModuloMesa
{
    public class Mesa : EntidadeBase
    {
        public string NumeroMesa { get; set; }

        public Mesa(string mesa)
        {
            NumeroMesa = mesa;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Mesa: " + NumeroMesa + Environment.NewLine;
        }
    }
}
