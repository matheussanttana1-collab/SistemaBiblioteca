using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;

namespace LibrarySystem.Applications.UseCases.Livros;

public class ExcluirLivroUseCase
{
	private readonly ILivroRepository livroRepository;

	public ExcluirLivroUseCase(ILivroRepository livroRepository)
	{
		this.livroRepository = livroRepository;
	}

	public async Task Execute(Guid id)
	{
		var livro = await livroRepository.BuscarLivroPeloId(id);

		if (livro == null)
			throw new InvalidOperationException($"Livro com ID {id} não encontrado.");

		await livroRepository.DeletarLivro(livro);
	}
}
