using ControleBar.ConsoleApp.Compartilhado;
using ControleBar.ConsoleApp.ModuloConta;

namespace ControleBar.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TelaMenuPrincipal telaMenuPrincipal = new TelaMenuPrincipal(new Notificador());

            while (true)
            {
                TelaBase telaSelecionada = telaMenuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    break;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is TelaCadastroConta)
                    GerenciarCadastroContas(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is ITelaCadastravel)
                {
                    ITelaCadastravel telaCadastroBasico = (ITelaCadastravel)telaSelecionada;

                    if (opcaoSelecionada == "1")
                        telaCadastroBasico.Inserir();

                    if (opcaoSelecionada == "2")
                        telaCadastroBasico.Editar();

                    if (opcaoSelecionada == "3")
                        telaCadastroBasico.Excluir();

                    if (opcaoSelecionada == "4")
                        telaCadastroBasico.VisualizarRegistros("Tela");
                }
            }
        }

        private static void GerenciarCadastroContas(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroConta telaCadastroConta = telaSelecionada as TelaCadastroConta;

            if (telaCadastroConta is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroConta.Inserir();

            if (opcaoSelecionada == "2")
                telaCadastroConta.ObtemPedido();

            if (opcaoSelecionada == "3")
                telaCadastroConta.Excluir();

            if (opcaoSelecionada == "4")
                telaCadastroConta.VisualizarRegistros("Tela");

            if (opcaoSelecionada == "5")
                telaCadastroConta.VisualizarRegistros("Tela");

            if (opcaoSelecionada == "6")
                telaCadastroConta.VisualizarRegistros("Tela");
        }

    }
}
