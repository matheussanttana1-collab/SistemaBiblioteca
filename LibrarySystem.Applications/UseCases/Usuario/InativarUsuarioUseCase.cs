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

	public async Task Execute(Guid id)
	{
		var usuario = await usuarioRepository.BuscarUsuarioPeloId(id);

		if (usuario == null)
			throw new InvalidOperationException($"Usuário com ID {id} não encontrado.");

		usuario.DesativarUsuario();

		await usuarioRepository.SalvarMudancas(usuario);
	}
}
