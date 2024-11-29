using AppAwm.Models;

namespace AppAwm.Respostas
{
    public class DocumentacaoComplementarAnswer : BasicAnswer
    {
        private const string messagemDeSucesso = "Consulta realizada com sucesso";

        public List<DocumentacaoComplementar> DocumentacaoComplementares { get; } = [];

        public DocumentacaoComplementarAnswer(bool success, string message) : base(success, message) { }

        public DocumentacaoComplementarAnswer(bool success, string message, List<DocumentacaoComplementar> documentacoes) : base(success, message) => DocumentacaoComplementares = documentacoes;

        public static DocumentacaoComplementarAnswer DeSucesso(string message) => new(true, messagemDeSucesso);
        public static DocumentacaoComplementarAnswer DeSucesso(List<DocumentacaoComplementar> documentacaos) => new(true, messagemDeSucesso, documentacaos);
        public static DocumentacaoComplementarAnswer DeErro(string erro) => new(false, erro);
    }
}
