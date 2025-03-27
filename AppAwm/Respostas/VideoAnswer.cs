using AppAwm.Models;

namespace AppAwm.Respostas
{
    public class VideoAnswer : BasicAnswer
    {
        public const string messageOfSuccess = "Video {0} com Sucesso.";
        public const string messageOfError = "Não foi possível subir o video";
        public const string messageOfFalha = "Não foi possivel estabelecer conexão com o banco de dados";
        public const string messageOfConsulta = "Consulta realizada com Sucesso.";

        public Video Video { get; } = new();
        public List<Video> Videos { get; } = [];
        public string[] Erros { get; } = [];

        public VideoAnswer(bool success, string message) : base(success, message) { }
        public VideoAnswer(bool success, string message, string[] erro) : base(success, message) => Erros = erro;
        public VideoAnswer(bool success, string message, Video video) : base(success, message) => Video = video;
        public VideoAnswer(bool success, string message, List<Video> videos) : base(success, message) => Videos = videos;

        public static VideoAnswer DeSucesso(Video video, bool isNovo) => new(true, string.Format(messageOfSuccess, (isNovo ? "Cadastrado" : "Atualizado")), video);
        public static VideoAnswer DeSucesso(string messgem) => new(true, messgem);
        public static VideoAnswer DeSucesso(List<Video> list) => new(true, messageOfConsulta, list);

        public static VideoAnswer DeErro(string error) => new(false, error ?? messageOfError);
        public static VideoAnswer DeErro(string[] error) => new(false, string.Empty, error);
        public static VideoAnswer DeFalha(string falha) => new(false, falha ?? messageOfFalha);
    }
}
