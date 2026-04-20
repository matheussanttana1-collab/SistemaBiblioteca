using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Services;

namespace LibrarySystem.Applications.UseCases.EmprestimoCases;

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

	public async Task Execute(RealizarEmprestimoDto dto)
	{
		var livro = await livroRepository.BuscarLivroPeloIdAsync(dto.LivroId);
		if (livro == null)
			throw new InvalidOperationException($"Livro com ID {dto.LivroId} não encontrado.");

		var usuario = await usuarioRepository.BuscarUsuarioComEmprestimoAsync(dto.UsuarioId);
		if (usuario == null)
			throw new InvalidOperationException($"Usuário com ID {dto.UsuarioId} não encontrado.");

		var emprestimo = bibliotecaService.RealizarEmprestimo(livro, usuario);

		await emprestimoRepository.AdicionarEmprestimoAsync(emprestimo);
	}
}
