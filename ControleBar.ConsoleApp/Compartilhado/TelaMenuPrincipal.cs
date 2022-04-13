using ControleBar.ConsoleApp.ModuloGarcom;
using ControleBar.ConsoleApp.ModuloProduto;
using ControleBar.ConsoleApp.ModuloConta;
using ControleBar.ConsoleApp.ModuloMesa;
using ControleBar.ConsoleApp.ModuloPedido;
using System;

namespace ControleBar.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private readonly IRepositorio<Garcom> repositorioGarcom;
        private readonly TelaCadastroGarcom telaCadastroGarcom;

        private readonly IRepositorio<Produto> repositorioProduto;
        private readonly TelaCadastroProduto telaCadastroProduto;

        private readonly IRepositorio<Conta> repositorioConta;
        private readonly TelaCadastroConta telaCadastroConta;

        private readonly IRepositorio<Mesa> repositorioMesa;
        private readonly TelaCadastroMesa telaCadastroMesa;

        private readonly IRepositorio<Pedido> repositorioPedido;
        private readonly TelaCadastroPedido telaCadastroPedido;

        public TelaMenuPrincipal(Notificador notificador)
        {
            repositorioGarcom = new RepositorioGarcom();
            telaCadastroGarcom = new TelaCadastroGarcom(repositorioGarcom, notificador);

            repositorioMesa = new RepositorioMesa();
            telaCadastroMesa = new TelaCadastroMesa(repositorioMesa, notificador);

            repositorioPedido = new RepositorioPedido();
            telaCadastroPedido = new TelaCadastroPedido(repositorioPedido, notificador);

            repositorioProduto = new RepositorioProduto();
            telaCadastroProduto = new TelaCadastroProduto(repositorioProduto, notificador);

            repositorioConta = new RepositorioConta();
            telaCadastroConta = new TelaCadastroConta(repositorioConta, repositorioGarcom, telaCadastroGarcom, 
                repositorioMesa, telaCadastroMesa, repositorioPedido, telaCadastroPedido, notificador);

            PopularAplicacao();
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("Controle de Mesas de Bar 1.5");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Gerenciar Garçons");

            Console.WriteLine("Digite 2 para Gerenciar Mesas");

            Console.WriteLine("Digite 3 para Gerenciar Conta");

            Console.WriteLine("Digite 4 para Gerenciar Estoque");

            Console.WriteLine("Digite s para sair");

            string opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = telaCadastroGarcom;

            else if (opcao == "2")
                tela = telaCadastroMesa;

            else if (opcao == "3")
                tela = telaCadastroConta;

            else if (opcao == "4")
                tela = telaCadastroProduto;

            else if (opcao == "5")
                tela = null;

            return tela;
        }

        private void PopularAplicacao()
        {
            var garcom = new Garcom("Julinho", "230.232.519-98");
            repositorioGarcom.Inserir(garcom);

            var pedidos = new Pedido("Camarão a Milanesa");
            repositorioPedido.Inserir(pedidos);

            var mesas = new Mesa("4");
            repositorioMesa.Inserir(mesas);
        }
    }
}
