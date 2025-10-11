using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAgenda.dominio.Entidades
{
    public class Servico: EntidadeBase
    {
        #region Propriedades
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; } = 0;
        public long IdUsuario { get; private set; }
        public Usuario Usuario { get; private set; }
        public List<Disponibilidade> Disponibilidades { get; private set; }
        #endregion


    }


}
