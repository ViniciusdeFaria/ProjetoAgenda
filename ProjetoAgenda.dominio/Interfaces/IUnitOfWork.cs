using ProjetoAgenda.dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAgenda.dominio.Interfaces
{
    public interface IUnitOfWork
    {
        #region Metodos Genericos
        Task InserirAsync<T>(T entidade) where T : EntidadeBase;
        Task AtualizarAsync<T>(T entidade) where T : EntidadeBase;
        Task RemoverAsync<T>(T entidade) where T : EntidadeBase;
        Task<T?> ObterPorIdAsync<T>(long id) where T : EntidadeBase;
        Task<IEnumerable<T>> ObterTodosAsync<T>() where T : EntidadeBase;
        Task<IEnumerable<T>> ObterTodosPaginadoAsync<T>(int pagina, int linhasPorPagina) where T : EntidadeBase;
        Task RemoverLoteAsync<T>(IEnumerable<T> lote) where T : EntidadeBase;
        Task<int> CountAsync<T>() where T : EntidadeBase;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        #endregion
    }
}
