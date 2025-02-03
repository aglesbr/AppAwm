using AppAwm.Models;

namespace AppAwm.Respostas
{
    public class DocumentoCargoAnswer : BasicAnswer
    {
        private const string messageOfConsulta = "Consulta realizada com sucesso.";
        private const string messageOfError = "Erro inesperado";

        public DocumentacaoCargo DocumentacaoCargo { get; } = new();
        public List<DocumentacaoCargo> DocumentacoesCargos { get; } = [];

        public DocumentoCargoAnswer(bool success, string message) : base(success, message){}
        public DocumentoCargoAnswer(bool success, string message, DocumentacaoCargo documentoCargo) : base(success, message) => this.DocumentacaoCargo = documentoCargo;
        public DocumentoCargoAnswer(bool success, string message, List<DocumentacaoCargo> documentosCargos) : base(success, message) => this.DocumentacoesCargos = documentosCargos;

        public static DocumentoCargoAnswer DeSucesso(DocumentacaoCargo documentoCargo) => new(true, messageOfConsulta, documentoCargo);
        public static DocumentoCargoAnswer DeSucesso(List<DocumentacaoCargo> documentosCargos) => new(true, messageOfConsulta, documentosCargos);
        public static DocumentoCargoAnswer DeSucesso(string msg) => new(true, msg);
        public static DocumentoCargoAnswer DeErro(string? erro = null) => new(false, erro ?? messageOfError);
    }
}
