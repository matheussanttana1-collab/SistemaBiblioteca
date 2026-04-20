using Dapper;
using LibrarySystem.Applications.DTOs;
using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Modelos;


namespace LibrarySystem.Infrastructure.Repositories;

public class LivroRepository :  RepositoryBase,ILivroRepository
{

	public LivroRepository(string connectionString) : base(connectionString)
	{}

	public async Task AdicionarLivroAsync(Livro livro)
	{
		using var connection = CreateConnection();
		var sql = @"INSERT INTO Livros(id,titulo,autor,isbm,ano_publicacao,generos,statusLivro)
		VALUES(@Id,@Titulo,@Autor,@Generos)";

		await connection.ExecuteAsync(sql, new
		{
			livro.Id,
			livro.Titulo,
			livro.Autor,
			livro.ISBN,
			livro.AnoPublicação,
			Generos = string.Join(',', livro.Generos),
			Status = (int)livro.StatusDoLivro
		});
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
