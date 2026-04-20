using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Services;

namespace LibrarySystem.Applications.UseCases.LivrosCases;


public class RealizarReservaUseCase
{
	private readonly ILivroRepository livroRepository;
	private readonly IUsuarioRepository usuarioRepository;
	private readonly IBibliotecaService bibliotecaService;

	public RealizarReservaUseCase(ILivroRepository livroRepository, IUsuarioRepository usuarioRepository, 
	IBibliotecaService bibliotecaService)
	{
		this.livroRepository = livroRepository;
		this.usuarioRepository = usuarioRepository;
		this.bibliotecaService = bibliotecaService;
	}

	public async Task Execute(ReservarLivroDto dto)
	{
		// Verificar existência
		var livro = await livroRepository.BuscarLivroPeloIdAsync(dto.LivroId);
		if (livro == null)
			throw new InvalidOperationException($"Livro com ID {dto.LivroId} não encontrado.");

		var usuario = await usuarioRepository.BuscarUsuarioComReservasAsync(dto.UsuarioId);
		if (usuario == null)
			throw new InvalidOperationException($"Usuário com ID {dto.UsuarioId} não encontrado.");

		// Realizar reserva (com todas as validações de negócio)
		bibliotecaService.RealizarReserva(livro, usuario);

		// Persistir mudanças
		await livroRepository.SalvarMudancasAsync(livro);
	}
}
