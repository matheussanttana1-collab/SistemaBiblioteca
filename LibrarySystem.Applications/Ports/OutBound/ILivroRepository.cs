
using LibrarySystem.Applications.DTOs;
using LibrarySystem.Domain.Modelos;

namespace LibrarySystem.Applications.Ports.Out;

public interface ILivroRepository
{
	Task<IEnumerable<Livro>> BuscarLivrosAsync(BuscarLivrosFilterDto dto);
	Task<Livro> BuscarLivroPeloIdAsync(Guid id);
	Task<IEnumerable<Livro>> BuscarLivroPeloTituloAsync(string titulo);
	Task<IEnumerable<Livro>> BuscarLivroPeloAutorAsync(string autor);
	Task<IEnumerable<Livro>> BuscarLivrosPeloGeneroAsync(string genero);
	Task AdicionarLivroAsync(Livro livro);
	Task SalvarMudancasAsync(Livro livro);
	Task DeletarLivroAsync(Livro livro);
}
