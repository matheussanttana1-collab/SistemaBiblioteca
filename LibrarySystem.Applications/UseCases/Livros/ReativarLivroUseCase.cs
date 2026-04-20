using LibrarySystem.Applications.Ports.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Applications.UseCases.LivrosCases;

public class ReativarLivroUseCase
{
	private readonly ILivroRepository livroRepo;

	public ReativarLivroUseCase(ILivroRepository livroRepository)
	{
		livroRepo = livroRepository;
	}

	public async Task Execute(Guid id)
	{
		// Verificar existência
		var livro = await livroRepo.BuscarLivroPeloIdAsync(id);
		if (livro == null)
			throw new InvalidOperationException($"Livro com ID {id} não encontrado.");

		livro.ReativarLivro();

		// Persistir mudanças
		await livroRepo.SalvarMudancasAsync(livro);
	}
}
