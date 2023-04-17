using Biblio.MAUI.Services;
using Biblio.MAUI.View;
using Biblio.MAUI.ViewModel;

namespace Biblio.MAUI;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<BookService>();
		builder.Services.AddSingleton<BookViewModel>();
        builder.Services.AddSingleton<BooksPage>();
        builder.Services.AddSingleton<BookmarksPage>();

        return builder.Build();
	}
}

