using ControleBar.ConsoleApp.Compartilhado;
using ControleBar.ConsoleApp.ModuloGarcom;
using ControleBar.ConsoleApp.ModuloMesa;
using ControleBar.ConsoleApp.ModuloPedido;
using System;
using System.Collections.Generic;

namespace ControleBar.ConsoleApp.ModuloConta
{
    public class TelaCadastroConta : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Conta> _repositorioConta;
        private readonly Notificador _notificador;

        private readonly IRepositorio<Garcom> _repositorioGarcom;
        private readonly TelaCadastroGarcom _telaCadastroGarcom;

        private readonly IRepositorio<Mesa> _repositorioMesa;
        private readonly TelaCadastroMesa _telaCadastroMesa;

        private readonly IRepositorio<Pedido> _repositorioPedido;
        private readonly TelaCadastroPedido _telaCadastroPedido;

        int numeroGarcom = 0;
        int numeroMesa = 0;
        int numeroPedido= 0;

        public TelaCadastroConta(IRepositorio<Conta> repositorioConta, 
            IRepositorio<Garcom> repositorioGarcom, TelaCadastroGarcom telaCadastroGarcom, 
            IRepositorio<Mesa> repositorioMesa, TelaCadastroMesa telaCadastroMesa, 
            IRepositorio<Pedido> repositorioPedido, TelaCadastroPedido telaCadastroPedido, Notificador notificador)
            : base("Cadastro de Contas")
        {
            _repositorioConta = repositorioConta;
            _repositorioGarcom = repositorioGarcom;
            _telaCadastroGarcom = telaCadastroGarcom;
            _repositorioMesa = repositorioMesa;
            _telaCadastroMesa = telaCadastroMesa;
            _repositorioPedido = repositorioPedido;
            _telaCadastroPedido = telaCadastroPedido;
            _notificador = notificador;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para abrir uma Conta");
            Console.WriteLine("Digite 2 para adicionar Pedidos");
            Console.WriteLine("Digite 3 para fechar uma Conta");
            Console.WriteLine("Digite 4 para Visualizar Contas");
            Console.WriteLine("Digite 5 para Visualizar total faturado (dia)");
            Console.WriteLine("Digite 6 para Visualizar total gorjetas");
            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;

        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Conta");

            Conta novaConta = ObterConta();

            _repositorioConta.Inserir(novaConta);

            _notificador.ApresentarMensagem("Conta cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void InserirPedidos()
        {
            MostrarTitulo("Cadastro de Pedidos");

            Conta novaConta = ObterConta();

            _repositorioConta.Inserir(novaConta);

            _notificador.ApresentarMensagem("Pedido cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Conta");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma conta cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroGenero = ObterNumeroRegistro();

            Conta contaAtualizado = ObterConta();

            bool conseguiuEditar = _repositorioConta.Editar(numeroGenero, contaAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Conta editada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Conta");

            bool temFuncionariosRegistrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma conta cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            decimal valorGorjeta = ValorGorjeta();

            _notificador.ApresentarMensagem("Nenhuma conta cadastrado para excluir.", TipoMensagem.Atencao);

            int numeroConta = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioConta.Excluir(numeroConta);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Conta excluída com sucesso!", TipoMensagem.Sucesso);
        }

        public decimal ValorGorjeta()
        {
            _notificador.ApresentarMensagem("Qual valor deseja dar de gorjeta para o Garçom ?.", TipoMensagem.Atencao);
            decimal valorpassado = Convert.ToDecimal(Console.ReadLine());

            return valorpassado;
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Contas Cadastradas");

            List<Conta> contas = _repositorioConta.SelecionarTodos();

            if (contas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma Conta disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Conta conta in contas)
                Console.WriteLine(conta.ToString());

            Console.ReadLine();

            return true;
        }

        private Conta ObterConta()
        {
            Garcom garcom = ObtemGarcom();

            Mesa mesa = ObtemMesa();

            return new Conta(garcom, mesa);
        }

        public Garcom ObtemGarcom()
        {
            bool temGarcom = _telaCadastroGarcom.VisualizarRegistros("Pesquisando");

            if (!temGarcom)
            {
                _notificador.ApresentarMensagem("Sem garçom registrado!", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o ID do Garçom: ");
            numeroGarcom = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Garcom garcomRequisitado = _repositorioGarcom.SelecionarRegistro(numeroGarcom);

            return garcomRequisitado;
        }

        public Mesa ObtemMesa()
        {
            bool temMesa = _telaCadastroMesa.VisualizarRegistros("Pesquisando");

            if (!temMesa)
            {
                _notificador.ApresentarMensagem("Sem mesa registrada!", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o ID da Mesa: ");
            numeroMesa = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Mesa mesaRequisitada = _repositorioMesa.SelecionarRegistro(numeroMesa);

            return mesaRequisitada;
        }

        public Pedido ObtemPedido()
        {
            bool temPedido = _telaCadastroPedido.VisualizarRegistros("Pesquisando");

            if (!temPedido)
            {
                _notificador.ApresentarMensagem("Sem pedido registrado!", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o ID do Pedido: ");
            numeroPedido = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Pedido pedidoRequisitado = _repositorioPedido.SelecionarRegistro(numeroPedido);

            return pedidoRequisitado;
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da Conta que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioConta.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da conta não foi encontrada, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
