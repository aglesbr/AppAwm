﻿namespace AppAwm.Models.Enum
{
    [Flags]
    public enum EnumPerfil : int
    {
        None = 0,
        Terceiro = 1,
        Master = 2,
        Analista = 4,
        Administrador = Terceiro | Master | Analista
    }
}
