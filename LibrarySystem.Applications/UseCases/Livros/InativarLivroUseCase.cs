using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Exceptions;

namespace LibrarySystem.Applications.UseCases.Livros;

public class InativarLivroUseCase
{
	private readonly ILivroRepository livroRepository;
	private readonly IEmprestimoRepository emprestimoRepository;

	public InativarLivroUseCase(ILivroRepository livroRepository, IEmprestimoRepository emprestimoRepository)
	{
		this.livroRepository = livroRepository;
		this.emprestimoRepository = emprestimoRepository;
	}

	public async Task Execute(Guid id)
	{
		var livro = await livroRepository.BuscarLivroPeloId(id);

		if (livro == null)
			throw new InvalidOperationException($"Livro com ID {id} não encontrado.");
		var temHistorico = await emprestimoRepository.BuscarRegistroDeAtividadeLivro(livro.Id);

		if (temHistorico)
			throw new DomainException("Este livro não pode ser excluído permanentemente pois já possui registros " +
			"de empréstimos.");

		livro.InativarLivro();

		await livroRepository.SalvarMudancas(livro);
	}
}
