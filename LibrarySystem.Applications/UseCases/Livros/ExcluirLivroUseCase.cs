using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.DomainExcpetion.Modelos;
using Microsoft.Win32;
using System.Runtime.ConstrainedExecution;

namespace LibrarySystem.Applications.UseCases.Livros;

public class ExcluirLivroUseCase
{
	private readonly ILivroRepository livroRepo;
	private readonly IEmprestimoRepository emprestimoRepo;

	public ExcluirLivroUseCase(ILivroRepository livroRepository, 
	IEmprestimoRepository emprestimoRepository)
	{
		livroRepo = livroRepository;
		emprestimoRepo = emprestimoRepository;
	}

	public async Task Execute(Guid id)
	{
		var livro = await livroRepo.BuscarLivroPeloId(id);

		if (livro == null)
			throw new InvalidOperationException($"Livro com ID {id} não encontrado.");
		var RegistroDeAtividade = await emprestimoRepo.BuscarRegistroDeAtividadeLivro(id);
		if (RegistroDeAtividade)
			throw new InvalidOperationException("Este livro não pode ser excluído permanentemente pois já possui " +
			"registros de empréstimos.");
		await livroRepo.DeletarLivro(livro);
	}
}
