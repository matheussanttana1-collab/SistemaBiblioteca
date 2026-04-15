using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.DomainExcpetion.Modelos;

namespace LibrarySystem.Applications.UseCases.Usuarios;

public class CadastrarUsuarioUseCase
{
	private readonly IUsuarioRepository usuarioRepository;

	public CadastrarUsuarioUseCase(IUsuarioRepository usuarioRepository)
	{
		this.usuarioRepository = usuarioRepository;
	}

	public async Task Execute(CadastrarUsuarioDto dto)
	{
		var usuario = new Usuario(dto.Nome, dto.CPF, dto.TipoUsuario);

		await usuarioRepository.AdicionarUsuario(usuario);
	}
}
