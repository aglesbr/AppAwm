using AppAwm.Models;
using AppAwm.Models.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppAwm.Respostas
{
    public class ColaboradorAnswer : BasicAnswer
    {
        public const string messageOfSuccess = "Colaborador {0} com Sucesso.";
        public const string messageOfFalha = "Não foi possivel estabelecer conexão com o banco de dados";
        public const string messageOfConsulta = "Consulta realizada com Sucesso.";

        public Colaborador Colaborador { get; } = new();
        public string[] Erros { get; } = [];
        public List<Colaborador> Colaboradore { get; } = [];
        public List<SelectListItem> Empresas { get; } = [];
        public EnumAcao Acao { get; }

        public ColaboradorAnswer(bool success, string message) : base(success, message) { }

        public ColaboradorAnswer(bool success, string message, string[] erro) : base(success, message) => Erros = erro;

        public ColaboradorAnswer(bool success, string message, List<Colaborador> list) : base(success, message) => Colaboradore = list;

        public ColaboradorAnswer(bool success, string message, Colaborador funcionario) : base(success, message) => Colaborador = funcionario;

        public ColaboradorAnswer(bool success, string message, Colaborador funcionario, List<SelectListItem> list) : base(success, message)
        {
            Colaborador = funcionario;
            Empresas = list;
        }

        public static ColaboradorAnswer DeSucesso(EnumAcao acao) => new(true, string.Format(messageOfSuccess, (acao == EnumAcao.Criar ? "Cadastrado" : "Atualizado")));

        public static ColaboradorAnswer DeSucesso(Colaborador funcionario) => new(true, messageOfConsulta, funcionario);

        public static ColaboradorAnswer Bind(Colaborador? funcionario, List<SelectListItem> empresas) => new(true, string.Empty, funcionario ?? new(), empresas);

        public static ColaboradorAnswer DeSucesso(List<Colaborador> funcionarios) => new(true, messageOfConsulta, funcionarios);

        public static ColaboradorAnswer DeErro(string[] error) => new(false, string.Empty, error);

        public static ColaboradorAnswer DeErro(string error) => new(false, error);

        public static ColaboradorAnswer DeFalha() => new(false, messageOfFalha);
    }
}
