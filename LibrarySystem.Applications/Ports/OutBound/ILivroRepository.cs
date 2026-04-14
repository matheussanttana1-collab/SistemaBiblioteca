

using LibrarySystem.Domain.Modelos;

namespace LibrarySystem.Applications.Ports.Out;

public interface ILivroRepository
{
	Task<IEnumerable<Livro>> BuscarLivros();
	Task<Livro> BuscarLivroPeloId(Guid id);
	Task<Livro> BuscarLivroPeloTitulo(string titulo);
	Task<Livro> BuscarLivroPeloAutor(string titulo);
	Task AdicionarLivro (Livro livro);

	Task SalvarMudancas(Livro livro);

	Task DeletarLivro(Livro livro);


}
