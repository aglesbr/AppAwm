namespace AppAwm.Models.Enum
{
    [Flags]
    public enum EnumStatusDocs : int
    {
        None = 0,
        Enviado = 1,
        EmAnalise = 2,
        Aprovado = 3,
        Rejeitado = 4,
        Resalva = 5,
        Expirado = 6,
    }
}
