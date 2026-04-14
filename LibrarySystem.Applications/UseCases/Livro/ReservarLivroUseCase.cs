using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;

namespace LibrarySystem.Applications.UseCases.Livros;

public class ReservarLivroUseCase
{
	private readonly ILivroRepository livroRepository;

	public ReservarLivroUseCase(ILivroRepository livroRepository)
	{
		this.livroRepository = livroRepository;
	}

	public async Task Execute(ReservarLivroDto dto)
	{
		var livro = await livroRepository.BuscarLivroPeloId(dto.LivroId);

		if (livro == null)
			throw new InvalidOperationException($"Livro com ID {dto.LivroId} não encontrado.");

		livro.Reservar(dto.UsuarioId);

		await livroRepository.SalvarMudancas(livro);
	}
}
