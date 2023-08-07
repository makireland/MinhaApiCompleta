using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto fornecedor);
        Task Atualizar(Produto fornecedor);
        Task Remover(Guid id);
    }
}
