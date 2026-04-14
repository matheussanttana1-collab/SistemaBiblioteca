using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;

namespace LibrarySystem.Applications.UseCases.Usuarios;

public class InativarUsuarioUseCase
{
	private readonly IUsuarioRepository usuarioRepository;

	public InativarUsuarioUseCase(IUsuarioRepository usuarioRepository)
	{
		this.usuarioRepository = usuarioRepository;
	}

	public async Task Execute(InativarUsuarioDto dto)
	{
		var usuario = await usuarioRepository.BuscarUsuarioPeloId(dto.UsuarioId);

		if (usuario == null)
			throw new InvalidOperationException($"Usuário com ID {dto.UsuarioId} não encontrado.");

		usuario.DesativarUsuario();

		await usuarioRepository.SalvarMudancas(usuario);
	}
}
