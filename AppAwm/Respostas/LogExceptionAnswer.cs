using AppAwm.Models;

namespace AppAwm.Respostas
{
    public class LogExceptionAnswer : BasicAnswer
    {
        public List<LogException> Logs { get; } = [];

        public LogExceptionAnswer(bool success, string message) : base(success, message) { }
        public LogExceptionAnswer(bool success, string message, string erro) : base(success, message) { }
        public LogExceptionAnswer(bool success, string message, List<LogException> logs) : base(success, message) => Logs = logs;

        public static LogExceptionAnswer DeSucesso() => new(true, "Retorno com sucesso");
        public static LogExceptionAnswer DeSucesso(List<LogException> logs) => new(true, "Listagem com sucesso", logs);
        public static LogExceptionAnswer DeErro(string erro) => new(false, erro ?? "Ocorreu um erro na execução");
    }
}
