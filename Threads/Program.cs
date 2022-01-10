using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace ThreadApp
{
	public class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var services = new ServiceCollection();

			ConfigureServices(services);

			using (ServiceProvider serviceProvider = services.BuildServiceProvider())
			{
				var form1 = serviceProvider.GetRequiredService<Form1>();
				Application.Run(form1);
			}

		}

		public static void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<Form1>();
			services.AddDomain();
		}
	}
}
