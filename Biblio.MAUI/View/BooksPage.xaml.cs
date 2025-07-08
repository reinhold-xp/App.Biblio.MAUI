using Biblio.MAUI.Model;
using Biblio.MAUI.ViewModel;
using Microsoft.Maui.Controls;

namespace Biblio.MAUI.View;

public partial class BooksPage : ContentPage
{
    BookViewModel viewModel; 

    public BooksPage(BookViewModel viewModel)
	{
		InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
        viewModel.GetBooks();
       
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
