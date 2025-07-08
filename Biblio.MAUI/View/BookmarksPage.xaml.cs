
using Biblio.MAUI.Model;
using Biblio.MAUI.ViewModel;

namespace Biblio.MAUI.View;

public partial class BookmarksPage : ContentPage
{
    BookViewModel viewModel;

    public BookmarksPage(BookViewModel viewModel)
    {
		InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;  
    }

    protected override void OnAppearing()
    {
        viewModel.GetFavs();
    }

    void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
    {
        viewModel.SortBooks(this);
    }

    private async void ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e != null)
        {
            BookCell item = e.SelectedItem as BookCell;
            await Navigation.PushAsync(new ResumePage(item.book));
        }
    }
}
