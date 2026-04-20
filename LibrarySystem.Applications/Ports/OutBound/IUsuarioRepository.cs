using LibrarySystem.Domain.Modelos;


namespace LibrarySystem.Applications.Ports.Out;

public interface IUsuarioRepository
{
	public Task<Usuario> BuscarUsuariosAsync();
	public Task<Usuario> BuscarUsuarioPeloIdAsync(Guid id);
	public Task<IEnumerable<Usuario>> BuscarUsuarioPeloNomeAsync(string nome);
	public Task<Usuario> BuscarUsuarioComEmprestimoAsync(Guid id);
	public Task<Usuario> BuscarUsuarioComReservasAsync(Guid id);
	public Task AdicionarUsuarioAsync(Usuario usuario);
	public Task SalvarMudancasAsync(Usuario usuario);
}
