using System;
using System.Collections.Generic;

namespace AppDocManager.Models
{
    public class Treinamento
    {
        public int Cd_Treinamento { get; set; }

        public  string Nome { get; set; }

        public DateTime Validade_Treinamento { get; set; }

        public bool Status { get; set; }
        public virtual ICollection<TreinamentoHabilidade> Habilidades { get; set; } =  null;

    }
}
