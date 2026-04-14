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

	public async Task Execute(ExcluirLivroDto dto)
	{
		var livro = await livroRepository.BuscarLivroPeloId(dto.LivroId);

		if (livro == null)
			throw new InvalidOperationException($"Livro com ID {dto.LivroId} não encontrado.");

		await livroRepository.DeletarLivro(livro);
	}
}
