using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAgenda.dominio.Entidades
{
    public class Endereco: EntidadeBase
    {
        public string Lougradouro { get; set; }
        
        public string Numero { get; set; }

        public bool Ativo {  get; set; }

        public long IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

    }
}
