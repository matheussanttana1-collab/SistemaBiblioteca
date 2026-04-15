using LibrarySystem.Domain.Modelos;

namespace LibrarySystem.Applications.Ports.Out;

public interface IEmprestimoRepository
{
	Task<IEnumerable<Emprestimo>> BuscarLivros(int? take, int? skip);
	Task<IEnumerable<Emprestimo>> BuscarEmprestimosDoUsuario(Guid UserId);
	Task<bool> BuscarRegistroDeAtividadeLivro(Guid LivroId);
	Task<Emprestimo> BuscarEmprestimoPeloId(Guid id);
	Task AdicionarEmprestimo(Emprestimo livro);
	Task SalvarMudancas(Emprestimo livro);
	Task DeletarEmprestimo(Emprestimo livro);
}
