using ControleBar.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace ControleBar.ConsoleApp.ModuloProduto
{
    public class Produto : EntidadeBase
    {
        public string Nome { get; set; }
        public decimal Quantidade { get; set; }

        public Produto(string nome, decimal quantidade)
        {
            Nome = nome;
            Quantidade = quantidade;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome do Produto: " + Nome + Environment.NewLine +
                "Quantide no estoque: " + Quantidade + Environment.NewLine;
        }

        public void ReceberProduto(decimal produto)
        {
            Quantidade += produto;
        }
    }
}
