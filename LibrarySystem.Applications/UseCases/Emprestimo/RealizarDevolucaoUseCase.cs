using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.DomainExcpetion.Services;

namespace LibrarySystem.Applications.UseCases.Emprestimos;

public record RealizarDevolucaoDto(Guid EmprestimoId, Guid LivroId, Guid UsuarioId);

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
		var emprestimo = await emprestimoRepository.BuscarEmprestimoPeloId(dto.EmprestimoId);
		if (emprestimo == null)
			throw new InvalidOperationException($"Empréstimo com ID {dto.EmprestimoId} não encontrado.");

		var livro = await livroRepository.BuscarLivroPeloId(dto.LivroId);
		if (livro == null)
			throw new InvalidOperationException($"Livro com ID {dto.LivroId} não encontrado.");

		var usuario = await usuarioRepository.BuscarUsuarioPeloId(dto.UsuarioId);
		if (usuario == null)
			throw new InvalidOperationException($"Usuário com ID {dto.UsuarioId} não encontrado.");

		// Realizar devolução (com todas as validações de negócio)
		bibliotecaService.RealizarDevolucao(emprestimo, livro, usuario);

		// Persistir mudanças
		await emprestimoRepository.SalvarMudancas(emprestimo);
		await livroRepository.SalvarMudancas(livro);
		await usuarioRepository.SalvarMudancas(usuario);
	}
}
