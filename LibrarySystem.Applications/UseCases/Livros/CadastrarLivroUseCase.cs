using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Modelos;


namespace LibrarySystem.Applications.UseCases.Livros;

public class CadastrarLivroUseCase
{
	private readonly ILivroRepository livroRepository;

	public CadastrarLivroUseCase(ILivroRepository livroRepository)
	{
		this.livroRepository = livroRepository;
	}

	public async Task Execute(CadastrarLivroDto dto)
	{
		Livro livro = new Livro(dto.Titulo, dto.Autor, dto.Isbn, dto.AnoPublicacao);

		await livroRepository.AdicionarLivro(livro);
	}
}
