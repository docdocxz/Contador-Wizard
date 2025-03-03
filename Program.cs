using Contador_para_Wizard.Interfaces;
using Contador_para_Wizard.Models;

namespace Contador_para_Wizard {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IPartidaManager,PartidaManager>();

            var app = builder.Build();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
            }
        }
    }
