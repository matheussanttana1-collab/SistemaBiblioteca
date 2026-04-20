using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Exceptions;
using LibrarySystem.Domain.Modelos;

namespace LibrarySystem.Applications.UseCases.LivrosCases;

public class BuscarLivroPorIdUseCase
{
	private readonly ILivroRepository livroRepository;

	public BuscarLivroPorIdUseCase(ILivroRepository livroRepository)
	{
		this.livroRepository = livroRepository;
	}

	public async Task<Livro> Execute(Guid id)
	{
		var livro = await livroRepository.BuscarLivroPeloIdAsync(id);

		if (livro == null)
			throw new DomainException($"Livro com ID '{id}' não encontrado.");

		return livro;
	}
}
