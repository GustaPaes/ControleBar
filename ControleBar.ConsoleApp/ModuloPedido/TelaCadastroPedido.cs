using ControleBar.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace ControleBar.ConsoleApp.ModuloPedido
{
    public class TelaCadastroPedido : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Pedido> _repositorioPedido;
        private readonly Notificador _notificador;

        public TelaCadastroPedido(IRepositorio<Pedido> repositorioPedido, Notificador notificador)
            : base("Cadastro de Pedidos")
        {
            _repositorioPedido = repositorioPedido;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Pedidos");

            Pedido novoPedido = ObterPedido();

            _repositorioPedido.Inserir(novoPedido);

            _notificador.ApresentarMensagem("Pedido cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Pedidos");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum pedido cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroGenero = ObterNumeroRegistro();

            Pedido pedidoAtualizado = ObterPedido();

            bool conseguiuEditar = _repositorioPedido.Editar(numeroGenero, pedidoAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Pedido editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Pedido");

            bool temFuncionariosRegistrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum pedido cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroPedido = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioPedido.Excluir(numeroPedido);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Pedido excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Pedido Cadastrados");

            List<Pedido> pedidos = _repositorioPedido.SelecionarTodos();

            if (pedidos.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum pedido disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Pedido pedido in pedidos)
                Console.WriteLine(pedido.ToString());

            Console.ReadLine();

            return true;
        }

        private Pedido ObterPedido()
        {
            Console.Write("Digite o Pedido: ");
            string pedido = Console.ReadLine();

            return new Pedido(pedido);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do pedido que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioPedido.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do pedido não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
