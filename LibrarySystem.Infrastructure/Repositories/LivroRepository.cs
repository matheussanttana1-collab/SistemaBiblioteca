using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure.Repositories;

public class LivroRepository : ILivroRepository
{
	private readonly string _connectionString;

	public LivroRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public Task AdicionarLivroAsync(Livro livro)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Livro>> BuscarLivroPeloAutorAsync(string autor)
	{
		throw new NotImplementedException();
	}

	public Task<Livro> BuscarLivroPeloIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Livro>> BuscarLivroPeloTituloAsync(string titulo)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Livro>> BuscarLivrosAsync(BuscarLivrosFilterDto dto)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Livro>> BuscarLivrosPeloGeneroAsync(string genero)
	{
		throw new NotImplementedException();
	}

	public Task DeletarLivroAsync(Livro livro)
	{
		throw new NotImplementedException();
	}

	public Task SalvarMudancasAsync(Livro livro)
	{
		throw new NotImplementedException();
	}
}
