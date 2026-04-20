using Microsoft.Data.SqlClient;

namespace LibrarySystem.Infrastructure.Repositories;

public class RepositoryBase
{
	private readonly string _connectionString;

	protected RepositoryBase(string connectionString)
	{
		_connectionString = connectionString;
	}

	protected SqlConnection CreateConnection() => new SqlConnection(_connectionString);
}
}
