using Biblio.MAUI.Services;
using Biblio.MAUI.View;
using Biblio.MAUI.ViewModel;


namespace Biblio.MAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        // Initialise le builder MAUI
        var builder = MauiApp.CreateBuilder();

        // Définit la classe App comme point de départ
        builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        // MAUI gère le cycle de vie automatiquement
        // Injection des services MVVM (singleton = une seule instance partagée)
        builder.Services.AddSingleton<BookService>();   // Service de récupération des livres via API
        builder.Services.AddSingleton<BookViewModel>(); // ViewModel principal pour la page liste
        builder.Services.AddSingleton<BooksPage>();     // Page affichant la liste des livres
        builder.Services.AddSingleton<BookmarksPage>(); // Page affichant les favoris

        // Construit et retourne l'application MAUI complète
        return builder.Build();
	}
}

