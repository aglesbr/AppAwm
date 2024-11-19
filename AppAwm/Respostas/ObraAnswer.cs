using AppAwm.Models;

namespace AppAwm.Respostas
{
    public class ObraAnswer : BasicAnswer
    {
        public const string messageOfSuccess = "Obra {0} com Sucesso.";
        public const string messageOfConsulta = "Consulta realizada com sucesso.";
        public const string messageOfError = "Erro inesperado";

        public List<Obra> Obras { get; } = [];

        public ObraAnswer(bool success, string message) : base(success, message){}
        public ObraAnswer(bool success, string message, List<Obra> obras) : base(success, message) => Obras = obras;

        public static ObraAnswer DeErro(string erro) => new(false, erro ?? messageOfError);
        public static ObraAnswer DeSucesso() => new(true, messageOfSuccess);
        public static ObraAnswer DeSucesso(string? msg) => new(true, string.Format(messageOfSuccess, msg ?? "inserida" ));
        public static ObraAnswer DeSucesso(List<Obra> obras) => new(true, messageOfConsulta, obras); 
    }
}
