using AppAwm.Models;
using AppAwm.Models.Enum;

namespace AppAwm.Respostas
{
    public class DepartamentoAnswer : BasicAnswer
    {
        public const string messageOfSuccess = "Departamento {0} com Sucesso.";
        public const string messageOfFalha = "Não foi possivel estabelecer conexão com o banco de dados";
        public const string messageOfConsulta = "Consulta realizada com Sucesso.";

        public string[] Erros { get; } = [];
        public Departamento Departamento { get; } = new();
        public List<Departamento> Departamentos { get; } = [];
        public EnumAcao Acao { get; }

        public DepartamentoAnswer(bool success, string message) : base(success, message) { }
        public DepartamentoAnswer(bool success, string message, string[] erro) : base(success, message) => Erros = erro;
        public DepartamentoAnswer(bool success, string message, Departamento departamento) : base(success, message) => Departamento = departamento;
        public DepartamentoAnswer(bool success, string message, List<Departamento> departamentos) : base(success, message) => Departamentos = departamentos;


        public static DepartamentoAnswer DeSucesso(EnumAcao acao) => new(true, string.Format(messageOfSuccess, (acao == EnumAcao.Criar ? "Cadastrado" : "Atualizado")));
        public static DepartamentoAnswer DeSucesso(Departamento departamento) => new(true, messageOfSuccess, departamento);
        public static DepartamentoAnswer DeSucesso(List<Departamento> departamentos) => new(true, messageOfSuccess, departamentos);

        public static DepartamentoAnswer DeErro(string[] error) => new(false, string.Empty, error);
        public static DepartamentoAnswer DeErro(string error) => new(false, error);

        public static DepartamentoAnswer DeFalha() => new(false, messageOfFalha);
    }
}
