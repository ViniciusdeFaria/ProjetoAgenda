using ProjetoAgenda.dominio.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAgenda.dominio.Entidades
{
    public abstract class EntidadeBase
    {
        public long Id { get; private set; }
        protected EntidadeBase()
        {
            Id = GeradorIdHelper.ProximoId();
        }
        public void SetId(long id)
            { Id = id; }

    }
}
