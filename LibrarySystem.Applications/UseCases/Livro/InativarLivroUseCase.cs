using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;

namespace LibrarySystem.Applications.UseCases.Livros;

public class InativarLivroUseCase
{
	private readonly ILivroRepository livroRepository;

	public InativarLivroUseCase(ILivroRepository livroRepository)
	{
		this.livroRepository = livroRepository;
	}

	public async Task Execute(InativarLivroDto dto)
	{
		var livro = await livroRepository.BuscarLivroPeloId(dto.LivroId);

		if (livro == null)
			throw new InvalidOperationException($"Livro com ID {dto.LivroId} não encontrado.");

		livro.Inativar();

		await livroRepository.SalvarMudancas(livro);
	}
}
