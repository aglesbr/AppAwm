using AppAwm.Models;

namespace AppAwm.Respostas
{
    public class DownloadAnswer : BasicAnswer
    {
        public const string messageOfSuccess = "Uploada realizado com sucesso.";
        public const string messageOfConsulta = "Consulta realizada com sucesso.";
        public const string messageOfError = "Ocorreu um erro inesperado";

        public List<Download> Downloads { get; } = [];
        public Download? Download { get; }

        public DownloadAnswer(bool success, string message) : base(success, message){}
        public DownloadAnswer(bool success, string message, List<Download> downloads) : base(success, message) => Downloads = downloads;
        public DownloadAnswer(bool success, string message, Download download) : base(success, message) => Download = download;

        public static DownloadAnswer DeSucesso(List<Download> downloads) => new(true, messageOfSuccess, downloads);
        public static DownloadAnswer DeSucesso(Download download) => new(true, messageOfSuccess, download);
        public static DownloadAnswer DeSucesso(string? messageSucesso =  null) => new(true, messageSucesso ?? messageOfSuccess);
        public static DownloadAnswer DeErroOuVazio(string? messageErro = null) => new(false, messageErro ?? messageOfSuccess);
    }
}
