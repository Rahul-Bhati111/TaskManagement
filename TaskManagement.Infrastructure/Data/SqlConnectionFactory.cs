
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace TaskManagement.Infrastructure.Data;

public class SqlConnectionFactory : IDbConnectionFactory
{
	private readonly string _connectionString;

	public SqlConnectionFactory(string connectionString)
	{
		if (string.IsNullOrWhiteSpace(connectionString))
			throw new ArgumentException(
				"Connection string cannot be empty.",
				nameof(connectionString));

		_connectionString = connectionString;
	}

	public DbConnection CreateConnection()
	{
		return new SqlConnection(_connectionString);
	}
}
