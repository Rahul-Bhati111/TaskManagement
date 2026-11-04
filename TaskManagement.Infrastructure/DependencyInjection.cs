
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Interfaces;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		string connectionString)
	{
		if (string.IsNullOrWhiteSpace(connectionString))
		{
			throw new ArgumentException(
				"Database connection string is required.",
				nameof(connectionString));
		}

		services.AddSingleton<IDbConnectionFactory>(
			_ => new SqlConnectionFactory(connectionString));

		services.AddScoped<ITaskRepository, TaskRepository>();

		services.AddScoped<IUserRepository, UserRepository>();

		services.AddScoped<IPriorityRepository, PriorityRepository>();

		services.AddScoped<IStatusRepository, StatusRepository>();

		return services;
	}
}
