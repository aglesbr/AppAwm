using AppAwm.Models;
using AppAwm.Models.Enum;
using Newtonsoft.Json;

namespace AppAwm.Respostas
{
    public class EmpresaAnswer : BasicAnswer
    {
        public const string messageOfSuccess = "Empresa {0} com Sucesso.";
        public const string messageOfError = "Não foi possível criar a empresa";
        public const string messageOfFalha = "Não foi possivel estabelecer conexão com o banco de dados";
        public const string messageOfConsulta = "Consulta realizada com Sucesso.";

        public List<Empresa> Empresas { get; } = [];
        public Empresa Empresa { get; } = new();
        public ReceitaConsumerCnpj ReceitaConsumerCnpj { get; } = new();
        public string[] Erros { get; } = [];

        public EmpresaAnswer(bool success, string message) : base(success, message) { }

        public EmpresaAnswer(bool success, string message, string[] erro) : base(success, message) => Erros = erro;

        public EmpresaAnswer(bool success, string message, string jsonConsumerCnpj) : base(success, message)
        {
            ReceitaConsumerCnpj = JsonConvert.DeserializeObject<ReceitaConsumerCnpj>(jsonConsumerCnpj)!;
        }

        public EmpresaAnswer(bool success, string message, Empresa empresa) : base(success, message) => Empresa = empresa;

        public EmpresaAnswer(bool success, string message, List<Empresa> empresas) : base(success, message) => Empresas = empresas;

        public static EmpresaAnswer DeSucesso(EnumAcao acao) => new(true, string.Format(messageOfSuccess, (acao == EnumAcao.Criar ? "Cadastrada" : "Atualizada")));

        public static EmpresaAnswer DeSucesso(Empresa empresa) => new(true, string.Format(messageOfSuccess, (empresa.Dt_Atualizacao == null ? "Cadastrada" : "Atualizada")), empresa);

        public static EmpresaAnswer DeSucesso(string consumerCnpj) => new(true, messageOfConsulta, consumerCnpj);

        public static EmpresaAnswer DeSucesso(List<Empresa> list) => new(true, messageOfConsulta, list);

        public static EmpresaAnswer DeErro(string error) => new(false, error ?? messageOfError);

        public static EmpresaAnswer DeErro(string[] error) => new(false, string.Empty, error);

        public static EmpresaAnswer DeErro() => new(false, messageOfError);

        public static EmpresaAnswer DeErro(Empresa empresa) => new(false, messageOfError, empresa);

        public static EmpresaAnswer DeFalha() => new(false, messageOfFalha);

        public static EmpresaAnswer DeFalha(string falha) => new(false, falha ?? messageOfFalha);
    }
}
