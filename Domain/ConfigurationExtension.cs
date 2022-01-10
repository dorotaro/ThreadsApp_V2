using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
	public static class ConfigurationExtension
	{
		public static IServiceCollection AddDomain(this IServiceCollection service)
		{
			service.AddSingleton<IThreadService, ThreadService>();

			return service;
		}
	}
}
