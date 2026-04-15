using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.DomainExcpetion.Modelos;
using LibrarySystem.DomainExcpetion.Services;

namespace LibrarySystem.Applications.UseCases.Emprestimos;

public record EmprestarLivroDto(Guid LivroId, Guid UsuarioId);

public class EmprestarLivroUseCase
{
	private readonly ILivroRepository livroRepository;
	private readonly IUsuarioRepository usuarioRepository;
	private readonly IEmprestimoRepository emprestimoRepository;
	private readonly BibliotecaService bibliotecaService;

	public EmprestarLivroUseCase(ILivroRepository livroRepository, IUsuarioRepository usuarioRepository, 
	IEmprestimoRepository emprestimoRepository, BibliotecaService bibliotecaService)
	{
		this.livroRepository = livroRepository;
		this.usuarioRepository = usuarioRepository;
		this.emprestimoRepository = emprestimoRepository;
		this.bibliotecaService = bibliotecaService;
	}

	public async Task Execute(EmprestarLivroDto dto)
	{
		var livro = await livroRepository.BuscarLivroPeloId(dto.LivroId);
		if (livro == null)
			throw new InvalidOperationException($"Livro com ID {dto.LivroId} não encontrado.");

		var usuario = await usuarioRepository.BuscarUsuarioComEmprestimo(dto.UsuarioId);
		if (usuario == null)
			throw new InvalidOperationException($"Usuário com ID {dto.UsuarioId} não encontrado.");

		var emprestimo = bibliotecaService.RealizarEmprestimo(livro, usuario);

		await emprestimoRepository.AdicionarEmprestimo(emprestimo);
	}
}
