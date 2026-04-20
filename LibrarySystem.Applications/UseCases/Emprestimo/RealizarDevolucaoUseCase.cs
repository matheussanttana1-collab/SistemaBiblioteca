using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Services;

namespace LibrarySystem.Applications.UseCases.EmprestimoCases;

public class RealizarDevolucaoUseCase
{
	private readonly IEmprestimoRepository emprestimoRepository;
	private readonly ILivroRepository livroRepository;
	private readonly IUsuarioRepository usuarioRepository;
	private readonly IBibliotecaService bibliotecaService;

	public RealizarDevolucaoUseCase(IEmprestimoRepository emprestimoRepository, ILivroRepository livroRepository, IUsuarioRepository usuarioRepository, IBibliotecaService bibliotecaService)
	{
		this.emprestimoRepository = emprestimoRepository;
		this.livroRepository = livroRepository;
		this.usuarioRepository = usuarioRepository;
		this.bibliotecaService = bibliotecaService;
	}

	public async Task Execute(RealizarDevolucaoDto dto)
	{
		// Verificar existência
		var emprestimo = await emprestimoRepository.BuscarEmprestimoPeloIdAsync(dto.EmprestimoId);
		if (emprestimo == null)
			throw new InvalidOperationException($"Empréstimo com ID {dto.EmprestimoId} não encontrado.");

		var livro = await livroRepository.BuscarLivroPeloIdAsync(dto.LivroId);
		if (livro == null)
			throw new InvalidOperationException($"Livro com ID {dto.LivroId} não encontrado.");

		var usuario = await usuarioRepository.BuscarUsuarioPeloIdAsync(dto.UsuarioId);
		if (usuario == null)
			throw new InvalidOperationException($"Usuário com ID {dto.UsuarioId} não encontrado.");

		// Realizar devolução (com todas as validações de negócio)
		bibliotecaService.RealizarDevolucao(emprestimo, livro, usuario);

		// Persistir mudanças
		await emprestimoRepository.SalvarMudancasAsync(emprestimo);
		await livroRepository.SalvarMudancasAsync(livro);
		await usuarioRepository.SalvarMudancasAsync(usuario);
	}
}
