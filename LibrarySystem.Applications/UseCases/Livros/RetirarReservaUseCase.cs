

using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Services;

namespace LibrarySystem.Applications.UseCases.Livros;

public class RetirarReservaUseCase
{
	private readonly ILivroRepository livroRepo;

	public RetirarReservaUseCase(ILivroRepository livroRepository)
	{
		livroRepo = livroRepository;
	}

	public async Task Execute(Guid id)
	{
		// Verificar existência
		var livro = await livroRepo.BuscarLivroPeloId(id);
		if (livro == null)
			throw new InvalidOperationException($"Livro com ID {id} não encontrado.");

		livro.RetirarReserva();

		// Persistir mudanças
		await livroRepo.SalvarMudancas(livro);
	}
}
