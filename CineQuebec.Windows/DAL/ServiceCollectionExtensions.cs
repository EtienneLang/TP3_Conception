using CineQuebec.Windows;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.InterfacesForRepositories;
using CineQuebec.Windows.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AjoutDesServices(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        
        services.AddSingleton<IAbonneRepository, AbonneRepository>();
        services.AddSingleton<IAbonneService, AbonneService>();
        services.AddSingleton<IFilmRepository, FilmRepository>();
        services.AddSingleton<IFilmService, FilmService>();
        services.AddSingleton<IProjectionService, ProjectionService>();
        services.AddSingleton<IProjectionRepository, ProjectionRepository>();
        
        return services;
    }
}