using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure.Repositories;

public class EmprestimoRepository : IEmprestimoRepository
{
	private readonly string _connectionString;

	public EmprestimoRepository(string connectionString)
	{
		 _connectionString = connectionString;
	}

	public Task AdicionarEmprestimoAsync(Emprestimo livro)
	{
		throw new NotImplementedException();
	}

	public Task<Emprestimo> BuscarEmprestimoPeloIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Emprestimo>> BuscarEmprestimosDoUsuarioAsync(Guid UserId)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Emprestimo>> BuscarHistoricoEmprestimosAsync(DateTime? dataInicio, DateTime? dataFim, StatusAtividade? status)
	{
		throw new NotImplementedException();
	}

	public Task<bool> BuscarRegistroDeAtividadeLivroAsync(Guid LivroId)
	{
		throw new NotImplementedException();
	}

	public Task DeletarEmprestimoAsync(Emprestimo livro)
	{
		throw new NotImplementedException();
	}

	public Task SalvarMudancasAsync(Emprestimo livro)
	{
		throw new NotImplementedException();
	}
}
