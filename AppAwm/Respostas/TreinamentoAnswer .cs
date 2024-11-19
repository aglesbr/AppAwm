using AppAwm.Models;

namespace AppAwm.Respostas
{
    public class TreinamentoAnswer: BasicAnswer
    {
        private const string messageOfConsulta = "Consulta realizada com sucesso.";
        private const string messageOfError = "Erro inesperado";

        public Treinamento? Treinamento { get; } = null;
        public List<Treinamento> Treinamentos { get; } = [];

        public TreinamentoAnswer(bool success, string message) : base(success, message) { }
        public TreinamentoAnswer(bool success, string message, List<Treinamento> treinamentos) : base(success, message) => Treinamentos = treinamentos;
        public TreinamentoAnswer(bool success, string message, Treinamento treinamento) : base(success, message) => Treinamento = treinamento;

        public static TreinamentoAnswer DeSucesso(Treinamento treinamento) => new(true, messageOfConsulta, treinamento);
        public static TreinamentoAnswer DeSucesso(List<Treinamento> treinamentos) => new(true, messageOfConsulta, treinamentos);
        public static TreinamentoAnswer DeErro(string? error = null) => new(false, error ?? messageOfError);
    }
}
