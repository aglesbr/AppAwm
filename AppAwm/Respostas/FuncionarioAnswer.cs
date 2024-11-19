using AppAwm.Models;
using AppAwm.Models.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppAwm.Respostas
{
    public class FuncionarioAnswer : BasicAnswer
    {
        public const string messageOfSuccess = "Colaborador {0} com Sucesso.";
        public const string messageOfFalha = "Não foi possivel estabelecer conexão com o banco de dados";
        public const string messageOfConsulta = "Consulta realizada com Sucesso.";

        public Funcionario Funcionario { get; } = new();
        public string[] Erros { get; } = [];
        public List<Funcionario> Funcionarios { get; } = [];
        public List<SelectListItem> Empresas { get; } = [];
        public EnumAcao Acao { get; }

        public FuncionarioAnswer(bool success, string message) : base(success, message){}

        public FuncionarioAnswer(bool success, string message, string[] erro) : base(success, message) => Erros = erro;

        public FuncionarioAnswer(bool success, string message, List<Funcionario> list) : base(success, message) => Funcionarios = list;

        public FuncionarioAnswer(bool success, string message, Funcionario funcionario) : base(success, message) => Funcionario = funcionario;

        public FuncionarioAnswer(bool success, string message, Funcionario funcionario, List<SelectListItem> list) : base(success, message)
        {
            Funcionario = funcionario;
            Empresas = list;
        }

        public static FuncionarioAnswer DeSucesso(EnumAcao acao) => new(true, string.Format(messageOfSuccess, (acao == EnumAcao.Criar ? "Cadastrado" : "Atualizado")));

        public static FuncionarioAnswer DeSucesso(Funcionario funcionario) => new(true,messageOfConsulta, funcionario);

        public static FuncionarioAnswer Bind(Funcionario? funcionario, List<SelectListItem> empresas) => new(true, string.Empty , funcionario ?? new(), empresas );

        public static FuncionarioAnswer DeSucesso(List<Funcionario> funcionarios) => new(true,messageOfConsulta, funcionarios);

        public static FuncionarioAnswer DeErro(string[] error) => new(false, string.Empty, error);

        public static FuncionarioAnswer DeErro(string error) => new(false, error);

        public static FuncionarioAnswer DeFalha() => new(false, messageOfFalha);
    }
}
