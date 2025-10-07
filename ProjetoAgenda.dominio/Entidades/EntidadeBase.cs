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

        private readonly List<string> _notificacoes = new();

        public IReadOnlyCollection<string> Notificacoes => _notificacoes.AsReadOnly();

        public bool Valido => !_notificacoes.Any();
        protected EntidadeBase()
        {
            Id = GeradorIdHelper.ProximoId();
        }
        public void SetId(long id)
            { Id = id; }
        
        public void AdicionarNotificacao(string mensagem)
        {
            _notificacoes.Add(mensagem);

        }

        public void LimparNotificacao()
        {
            _notificacoes.Clear();
        }

        public string ObterMensagensDeErros() => string.Join(" | ", Notificacoes);
    }
}
