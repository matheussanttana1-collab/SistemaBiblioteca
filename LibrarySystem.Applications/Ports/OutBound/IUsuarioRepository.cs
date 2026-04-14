using LibrarySystem.Domain.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Applications.Ports.Out;

public interface IUsuarioRepository
{
	public Task<Usuario> BuscarUsuarios();
	public Task<Usuario> BuscarUsuarioPeloId(Guid id);
	public Task<Usuario> BuscarUsuarioComEmprestimo(Guid id);
	public Task<Usuario> BuscarUsuarioComReservas(Guid id);
	public Task AdicionarUsuario (Usuario usuario);
	public Task SalvarMudancas(Usuario usuario);

}
