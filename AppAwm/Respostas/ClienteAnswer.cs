using AppAwm.Models;

namespace AppAwm.Respostas
{
    public class ClienteAnswer : BasicAnswer
    {
        public const string messageOfSuccess = "Cliente {0} com Sucesso.";
        public const string messageOfError = "Não foi possível criar a cliente";
        public const string messageOfFalha = "Não foi possivel estabelecer conexão com o banco de dados";
        public const string messageOfConsulta = "Consulta realizada com Sucesso.";

        public Cliente Cliente { get; } = new();
        public List<Cliente> Clientes { get; } = [];
        public string[] Erros { get; } = [];

        public ClienteAnswer(bool success, string message) : base(success, message) { }

        public ClienteAnswer(bool success, string message, string[] erro) : base(success, message) => Erros = erro;

        public ClienteAnswer(bool success, string message, Cliente cliente) : base(success, message) => Cliente = cliente;

        public ClienteAnswer(bool success, string message, List<Cliente> clientes) : base(success, message) => Clientes = clientes;


        public static ClienteAnswer DeSucesso(Cliente cliente) => new(true, string.Format(messageOfSuccess, (cliente.Dt_Atualizacao == null ? "Cadastrada" : "Atualizada")), cliente);

        public static ClienteAnswer DeSucesso(List<Cliente> list) => new(true, messageOfConsulta, list);

        public static ClienteAnswer DeErro(string error) => new(false, error ?? messageOfError);

        public static EmpresaAnswer DeErro(string[] error) => new(false, string.Empty, error);

        public static ClienteAnswer DeErro() => new(false, messageOfError);

        public static ClienteAnswer DeFalha() => new(false, messageOfFalha);

        public static ClienteAnswer DeFalha(string falha) => new(false, falha ?? messageOfFalha);
    }
}
