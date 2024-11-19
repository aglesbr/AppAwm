using AppAwm.Models;
using AppAwm.Models.Enum;

namespace AppAwm.Respostas
{
    public class UsuarioAnswer : BasicAnswer
    {
        public const string messageOfSuccess = "Usuário {0} com Sucesso.";
        public const string messageOfError = "Não foi possível criar o usuairo";
        public const string messageOfFalha = "Não foi possivel estabelecer conexão com o banco de dados";
        public const string messageOfConsulta = "Consulta realizada com Sucesso.";
        public const string messageOfPadrao = "Execução realizada, sem retorno expecífico";

        public Usuario? Usuario { get; } = new();
        public string[] Erros { get; } = [];
        public List<Usuario> Usuarios { get; } = [];
        public EnumAcao Acao { get; } 

        public UsuarioAnswer(bool success, string message) : base(success, message) { }

        public UsuarioAnswer(bool success, string message, string[] erro) : base(success, message)  => Erros = erro;

        public UsuarioAnswer(bool success, string message, List<Usuario> list ) : base(success, message) => Usuarios = list;

        public UsuarioAnswer(bool success, string message, Usuario _usuario, EnumAcao _acao) : base(success, message)
        {
            Usuario = _usuario;
            Acao = _acao;
        }

        public UsuarioAnswer(bool success, string message, Usuario usuario) : base(success, message) => Usuario = usuario;

        public UsuarioAnswer(bool success, string message, EnumAcao acao) : base(success, message) => Acao = acao;

        public static UsuarioAnswer DeSucesso(Usuario usuario, EnumAcao acao) => new(true, string.Format(messageOfSuccess, (usuario.Dt_Atualizacao == null ? "Cadastrado" : "Atualizado" )), usuario, acao) ;

        public static UsuarioAnswer DeSucesso(List<Usuario> list) => new(true, messageOfConsulta, list) ;

        public static UsuarioAnswer DeErro(string error) => new(false, error ?? messageOfError);

        public static UsuarioAnswer DeErro(string[] error) => new(false, string.Empty, error);

        public static UsuarioAnswer DeErro() => new(false, messageOfError);

        public static UsuarioAnswer DeErro(Usuario usuario) => new(false, messageOfError, usuario);

        public static UsuarioAnswer Falha() => new(false, messageOfFalha);

        public static UsuarioAnswer Falha(string falha, EnumAcao acao) => new(false, falha ?? messageOfFalha, acao);

    }
}
