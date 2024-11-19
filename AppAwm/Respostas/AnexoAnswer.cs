using AppAwm.Models;

namespace AppAwm.Respostas
{
    public class AnexoAnswer : BasicAnswer
    {
        public const string messageOfError = "Não foi possível anexar o arquivo";
        public const string messageOfSuccess = "Anexo inserido com Sucesso.";
        public const string messageOfConsulta = "Consulta realizada com sucesso.";

        public List<Anexo> Anexos { get; } = [];

        public AnexoAnswer(bool success, string message) : base(success, message) { }
        public AnexoAnswer(bool success, string message, string erro) : base(success, message) { }
        public AnexoAnswer(bool success, string message, List<Anexo> anexos) : base(success, message) => Anexos = anexos;

        public static AnexoAnswer DeErro() => new(false, messageOfError);
        public static AnexoAnswer DeErro(string erro) => new(false, erro ?? messageOfError);

        public static AnexoAnswer DeSucesso() => new(true, messageOfSuccess);
        public static AnexoAnswer DeSucesso(string sucesso) => new(true, sucesso ?? messageOfSuccess);
        public static AnexoAnswer DeSucesso(List<Anexo> anexos) => new(true,messageOfConsulta, anexos);
    }
}
