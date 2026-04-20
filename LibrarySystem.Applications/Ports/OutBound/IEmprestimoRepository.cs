using LibrarySystem.Domain.Modelos;

namespace LibrarySystem.Applications.Ports.Out;

public interface IEmprestimoRepository
{
	Task<IEnumerable<Emprestimo>> BuscarEmprestimosDoUsuarioAsync(Guid UserId);
	Task<bool> BuscarRegistroDeAtividadeLivroAsync(Guid LivroId);
	Task<Emprestimo> BuscarEmprestimoPeloIdAsync(Guid id);
	Task<IEnumerable<Emprestimo>> BuscarHistoricoEmprestimosAsync(DateTime? dataInicio, DateTime? dataFim, StatusAtividade? status);
	Task AdicionarEmprestimoAsync(Emprestimo livro);
	Task SalvarMudancasAsync(Emprestimo livro);
	Task DeletarEmprestimoAsync(Emprestimo livro);
}
