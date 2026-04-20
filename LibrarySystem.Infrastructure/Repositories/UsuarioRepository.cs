using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{

	private readonly string _connectionString;

	public UsuarioRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public Task AdicionarUsuarioAsync(Usuario usuario)
	{
		throw new NotImplementedException();
	}

	public Task<Usuario> BuscarUsuarioComEmprestimoAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<Usuario> BuscarUsuarioComReservasAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<Usuario> BuscarUsuarioPeloIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Usuario>> BuscarUsuarioPeloNomeAsync(string nome)
	{
		throw new NotImplementedException();
	}

	public Task<Usuario> BuscarUsuariosAsync()
	{
		throw new NotImplementedException();
	}

	public Task SalvarMudancasAsync(Usuario usuario)
	{
		throw new NotImplementedException();
	}
}
