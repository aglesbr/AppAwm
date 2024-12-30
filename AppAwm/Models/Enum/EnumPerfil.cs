namespace AppAwm.Models.Enum
{
    [Flags]
    public enum EnumPerfil : int
    {
        None = 0,
        Terceiro = 1,
        Funcionario = 2,
        Analista = 4,
        Administrador = Terceiro | Funcionario | Analista
    }
}
