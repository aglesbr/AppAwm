using AppAwm.Models;

namespace AppAwm.Respostas
{
    public class ChartAnswer : BasicAnswer
    {
        public Chart Chart { get; } = new();

        public ChartAnswer(bool success, string message) : base(success, message) { }
        public ChartAnswer(bool success, string message, string erro) : base(success, message) { }
        public ChartAnswer(bool success, string message, Chart chart) : base(success, message) => Chart = chart;

        public static ChartAnswer DeSucesso(Chart chart) => new(true, "Retorno com sucesso", chart);
        public static ChartAnswer DeErro(string erro) => new(false, erro ?? "Ocorreu um erro na execução");
    }
}
