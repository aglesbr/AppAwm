using AppAwm.Models;

namespace AppAwm.Respostas
{
    public class CargoAnswer : BasicAnswer
    {
        private const string messageOfConsulta = "Consulta realizada com sucesso.";
        private const string messageOfError = "Erro inesperado";

        public Cargo? Cargo { get;} = null;
        public List<Cargo> Cargos { get; } = [];

        public CargoAnswer(bool success, string message) : base(success, message){}
        public CargoAnswer(bool success, string message, Cargo cargo) : base(success, message) => this.Cargo = cargo;
        public CargoAnswer(bool success, string message, List<Cargo> cargos) : base(success, message) => this.Cargos = cargos;

        public static CargoAnswer DeSucesso(Cargo cargo) => new(true, messageOfConsulta, cargo);
        public static CargoAnswer DeSucesso(List<Cargo> cargos) => new(true, messageOfConsulta, cargos);
        public static CargoAnswer DeSucesso(string msg) => new(true, msg);
        public static CargoAnswer DeErro(string? erro = null) => new(false, erro?? messageOfError);
    }
}
