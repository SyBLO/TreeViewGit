using MauiAppDT.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Radzen;
using MauiAppDTClassLibrary.Data;
using MauiAppDTClassLibrary.Services;
using FollowUpDash.Server.Controllers;
using MauiAppDTClassLibrary.ServicesApi;
using MauiAppDTClassLibrary.ServicesApi.dataBaseService;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MauiAppDTClassLibrary.ServicesApi.TreeNodeService;
using MauiAppDTClassLibrary.ServicesApi.FileService;
using MauiAppDTClassLibrary.ServicesApi.ArchivageService;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;


namespace MauiAppDT
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services
                    .AddBlazorise(options =>
                    {
                        options.Immediate = true;
                    })
                    .AddBootstrapProviders()
                    .AddFontAwesomeIcons();



            // Register AppDbContext
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "C:\\App\\maui\\BaseSQLite\\databaseApp.db");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

            var dbPathFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                     "C:\\App\\maui\\BaseSQLite\\filedirectories.db");
            builder.Services.AddDbContext<FileDbContext>(options => options.UseSqlite($"Data Source={dbPathFile}"));

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddRadzenComponents();

            builder.Services.AddScoped<ThService>();
            builder.Services.AddScoped<ExampleService>();
            //Login and Register
         

            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();

            builder.Services.AddSingleton<AdtPageService>();
            builder.Services.AddSingleton<EcoPageService>();
            builder.Services.AddSingleton<DeveloperService>();
            builder.Services.AddSingleton<EtudeService>();
            builder.Services.AddSingleton<ServerMethodsService>();
            builder.Services.AddSingleton<ToDoListService>();
            builder.Services.AddSingleton<ExcelService>();
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<ActionItemService>();

            //FileContext          
            //builder.Services.AddSingleton<FileService>();
            //builder.Services.AddDbContext<FileDbContext>();
            builder.Services.AddSingleton<FileIndexService>();
            builder.Services.AddSingleton<FileServiceTree>();

            builder.Services.AddTransient<FileService>();
            builder.Services.AddTransient<FileManagerService>();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();

#endif


            return builder.Build();
        }
    }
}
