using Dapper;
using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure.Repositories;

public class UsuarioRepository : RepositoryBase ,IUsuarioRepository
{

	public UsuarioRepository(string connectionString) : base(connectionString) { }

	public async Task AdicionarUsuarioAsync(Usuario usuario)
	{
		using var connection = CreateConnection();

		var sql = @"INSERT INTO Usuarios(id, nome, cpf, tipoUsuario, AtividadesUsuario) 
		VALUES (@Id,@Nome,@CPF,@TipoUsuario,@AtividadeUsuario)";

		await connection.ExecuteAsync(sql, usuario);
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
