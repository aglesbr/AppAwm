namespace AppAwm.Hasit.DataBase.Model.Enum
{
    [Flags]
    public enum EnumStatusDocs: int
    {
        None = 0,
        Enviado = 1,
        EmAnalise = 2,
        Aprovado = 3,
        Rejeitado = 4
    }
}
