using Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;

namespace FootballGo.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            services.AddScoped<ClienteService>();
            services.AddScoped<EmpleadoService>();
            services.AddScoped<Form1>();
            services.AddScoped<dgvClientes>();
            services.AddScoped<LoginForm>();
            services.AddScoped<MenuForm>();
            services.AddTransient<EmpleadoLoginForm>();
            services.AddTransient<EmpleadoDashboardForm>();

            var provider = services.BuildServiceProvider();

            // arrancar con el menú
            Application.Run(provider.GetRequiredService<MenuForm>());
        }
    }

    public static class SesionActual
    {
        public static string MailUsuario { get; set; } = string.Empty;
        public static int IdUsuario { get; set; }
    }
}
