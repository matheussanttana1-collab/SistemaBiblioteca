using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Exceptions;
using LibrarySystem.Domain.Modelos;

namespace LibrarySystem.Applications.UseCases.UsuarioCases;

public class BuscarUsuarioPorIdUseCase
{
	private readonly IUsuarioRepository usuarioRepository;

	public BuscarUsuarioPorIdUseCase(IUsuarioRepository usuarioRepository)
	{
		this.usuarioRepository = usuarioRepository;
	}

	public async Task<Usuario> Execute(Guid id)
	{
		var usuario = await usuarioRepository.BuscarUsuarioPeloIdAsync(id);

		if (usuario == null)
			throw new DomainException($"Usuário com ID '{id}' não encontrado.");

		return usuario;
	}
}
