using Contador_para_Wizard.Interfaces;
using Contador_para_Wizard.Models;

namespace Contador_para_Wizard {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IPartidaManager,PartidaManager>();
            builder.Services.AddSession(opt => {
                opt.IdleTimeout = TimeSpan.MaxValue;
                opt.Cookie.Name = "SessionID";
                //opt.Cookie.Expiration = TimeSpan.MaxValue;
                opt.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            app.UseStaticFiles();

            app.UseSession();

            app.MapControllers();

            app.Run();
            }
        }
    }

//Singui ngtumi