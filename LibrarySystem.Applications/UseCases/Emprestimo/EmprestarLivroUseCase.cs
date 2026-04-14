using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Modelos;

namespace LibrarySystem.Applications.UseCases.Emprestimos;

public record EmprestarLivroDto(Guid LivroId, Guid UsuarioId);

public class EmprestarLivroUseCase
{
	private readonly ILivroRepository livroRepository;
	private readonly IUsuarioRepository usuarioRepository;
	private readonly IEmprestimoRepository emprestimoRepository;

	public EmprestarLivroUseCase(ILivroRepository livroRepository, IUsuarioRepository usuarioRepository, IEmprestimoRepository emprestimoRepository)
	{
		this.livroRepository = livroRepository;
		this.usuarioRepository = usuarioRepository;
		this.emprestimoRepository = emprestimoRepository;
	}

	public async Task Execute(EmprestarLivroDto dto)
	{
		var livro = await livroRepository.BuscarLivroPeloId(dto.LivroId);
		if (livro == null)
			throw new InvalidOperationException($"Livro com ID {dto.LivroId} não encontrado.");

		var usuario = await usuarioRepository.BuscarUsuarioPeloId(dto.UsuarioId);
		if (usuario == null)
			throw new InvalidOperationException($"Usuário com ID {dto.UsuarioId} não encontrado.");

		// Validar regras de negócio
		livro.Emprestar(dto.UsuarioId);
		usuario.AdicionarEmprestimoAoUsuario(new Emprestimo(livro, usuario));

		// Persistir mudanças
		await livroRepository.SalvarMudancas(livro);
		await usuarioRepository.SalvarMudancas(usuario);
	}
}
