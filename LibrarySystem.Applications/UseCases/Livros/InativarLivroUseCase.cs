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

	public async Task Execute(Guid id)
	{
		var livro = await livroRepository.BuscarLivroPeloId(id);

		if (livro == null)
			throw new InvalidOperationException($"Livro com ID {id} não encontrado.");

		livro.Inativar();

		await livroRepository.SalvarMudancas(livro);
	}
}
