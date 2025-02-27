using AppAwm.Models;

namespace AppAwm.Respostas
{
    public class DocumentoEmpresaAnswer : BasicAnswer
    {

        private const string messageOfConsulta = "Consulta realizada com sucesso.";
        private const string messageOfError = "Erro inesperado";

        public DocumentacaoEmpresa DocumentacaoEmpresa { get; } = new();
        public List<DocumentacaoEmpresa> DocumentacoesEmpresasas { get; } = [];

        public DocumentoEmpresaAnswer(bool success, string message) : base(success, message) { }
        public DocumentoEmpresaAnswer(bool success, string message, DocumentacaoEmpresa documentoEmpresa) : base(success, message) => this.DocumentacaoEmpresa = documentoEmpresa;
        public DocumentoEmpresaAnswer(bool success, string message, List<DocumentacaoEmpresa> documentosEmpresas) : base(success, message) => this.DocumentacoesEmpresasas = documentosEmpresas;

        public static DocumentoEmpresaAnswer DeSucesso(DocumentacaoEmpresa documentoEmpresa) => new(true, messageOfConsulta, documentoEmpresa);
        public static DocumentoEmpresaAnswer DeSucesso(List<DocumentacaoEmpresa> documentosEmpresas) => new(true, messageOfConsulta, documentosEmpresas);
        public static DocumentoEmpresaAnswer DeSucesso(string msg) => new(true, msg);
        public static DocumentoEmpresaAnswer DeErro(string? erro = null) => new(false, erro ?? messageOfError);
    }
}
