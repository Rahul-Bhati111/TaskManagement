using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace TaskManagement.Infrastructure.Data;

public interface IDbConnectionFactory
{
	DbConnection CreateConnection();
}