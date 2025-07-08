using Biblio.MAUI.Model;
using Biblio.MAUI.Services;

namespace Biblio.MAUI.View;

public partial class ResumePage : ContentPage
{
	public Book Book { get; set; }
	public ResumePage(Book book)
	{
		InitializeComponent();
		this.Book = book;
		BindingContext = this;
		
	}
}
