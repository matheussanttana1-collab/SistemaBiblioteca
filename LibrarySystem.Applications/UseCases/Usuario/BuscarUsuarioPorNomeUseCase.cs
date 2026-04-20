using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Modelos;

namespace LibrarySystem.Applications.UseCases.UsuarioCases;

public class BuscarUsuarioPorNomeUseCase
{
	private readonly IUsuarioRepository usuarioRepository;

	public BuscarUsuarioPorNomeUseCase(IUsuarioRepository usuarioRepository)
	{
		this.usuarioRepository = usuarioRepository;
	}

	public async Task<IEnumerable<Usuario>> Execute(string nome)
	{
		var usuarios = await usuarioRepository.BuscarUsuarioPeloNomeAsync(nome);


		return usuarios;
	}
}
