using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Modelos;

namespace LibrarySystem.Applications.UseCases.LivrosCases;

public class BuscarLivros
{
	private readonly ILivroRepository livroRepository;

	public BuscarLivros(ILivroRepository livroRepository)
	{
		this.livroRepository = livroRepository;
	}

	public async Task<IEnumerable<Livro>> Execute(BuscarLivrosFilterDto filtros)
	{
		var livros = await livroRepository.BuscarLivrosAsync(filtros);

		return livros;
	}
}
