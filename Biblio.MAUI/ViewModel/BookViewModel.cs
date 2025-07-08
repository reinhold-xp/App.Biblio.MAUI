using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Web;
using System.Windows.Input;
using Biblio.MAUI.Model;
using Biblio.MAUI.Services;
using Biblio.MAUI.View;
using Newtonsoft.Json;

namespace Biblio.MAUI.ViewModel
{
    public class BookViewModel : IPreferences
    {
        private enum e_Sorting
        {
            TRI_NONE,
            TRI_TITLE,
            TRI_ID
        }

        // Tri par défaut
        private e_Sorting tri = e_Sorting.TRI_NONE;

        private BookService bookService;
        public List<String> favBooks = new List<string>();

        // Data binding avec des collections observables
        public ObservableCollection<BookCell> Books { get; } = new();
        public ObservableCollection<BookCell> Favs { get; } = new();

        public BookViewModel(BookService bookService)
        {
            this.bookService = bookService;
            LoadFavList();
        }

        public void GetBooks()
        {
           var task = DownloadDataAsync((books) =>
           {
               if (books != null)   
                   SetBooksCollection(books, Books);
           });
        }

        // Paramètre delegate type action (principe de simple responsabilité)
        private async Task DownloadDataAsync(Action<List<BookCell>> action)
        {
            try
            {
          
                var books = GetBooksCells(await bookService.GetBooksAsync(), favBooks);


                // Appel delegate ...
                action.Invoke(books);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Unable to get books", "OK");
            }
        }

        private List<BookCell> GetBooksCells(List<Book> rootBooks, List<String> favBooks)
        {
            List<BookCell> cellBooks = new List<BookCell>();

            foreach (Book book in rootBooks)
            {
                bool isFav = favBooks.Contains(book.titre);
                cellBooks.Add(new BookCell { book = book, isFavorite = isFav, favChangedAction = OnFavChanged });
            }

            return cellBooks;
        }

        public void GetFavs()
        {
            if (favBooks != null && Books != null)
            {
                List<BookCell> favsList = new List<BookCell>();

                Favs.Clear();

                foreach (BookCell bookCell in Books)
                {
                    if (favBooks.Contains(bookCell.book.titre))
                    {
                        Favs.Add(new BookCell { book = bookCell.book, isFavorite = true });
                    }
                }
            }
        }

        public void SortBooks(ContentPage page)
        {
            List<BookCell> sortedList;

            if (page is BooksPage)
                sortedList = new List<BookCell>(Books);
            else
                sortedList = new List<BookCell>(Favs);

            if (tri == e_Sorting.TRI_NONE)
            {
                // Tri croissant
                sortedList.Sort((book1, book2) => { return book1.book.titre.CompareTo(book2.book.titre); });
                tri = e_Sorting.TRI_TITLE;

            }
            else if (tri == e_Sorting.TRI_TITLE)
            {
                // Tri decroissant
                sortedList.Sort((book1, book2) => { return book2.book.titre.CompareTo(book1.book.titre); });
                tri = e_Sorting.TRI_ID;
            }
            else if (tri == e_Sorting.TRI_ID)
            {
                // Tri decroissant
                sortedList.Sort((book1, book2) => { return book1.book.id.CompareTo(book2.book.id); });
                tri = e_Sorting.TRI_NONE;
            }   

            if (page is BooksPage)
                SetBooksCollection(sortedList, Books);
            else
                SetBooksCollection(sortedList, Favs);
        }

        private void SetBooksCollection(List<BookCell> sortedList, ObservableCollection<BookCell> observableCollection)
        {
            observableCollection.Clear();
            foreach (var book in sortedList)
            {
                observableCollection.Add(book);
            }
        }

        private void OnFavChanged(BookCell bookCell)
        {
            bool isInFavList = favBooks.Contains(bookCell.book.titre);

            if (bookCell.isFavorite && !isInFavList)
            {
                favBooks.Add(bookCell.book.titre);

            }
            else if (!bookCell.isFavorite && isInFavList)
            {
                favBooks.Remove(bookCell.book.titre);
            }

            SaveFavList();
        }

        // Persistance des favoris en local / user settings
        private void SaveFavList()
        {
            string json = JsonConvert.SerializeObject(favBooks);
            Preferences.Default.Set("[KEY_FAV]", json);
        }

        private void LoadFavList()
        {
            if (Preferences.Default.ContainsKey("[KEY_FAV]"))
            {
                string json = Preferences.Default.Get("[KEY_FAV]", "");
                favBooks = JsonConvert.DeserializeObject<List<string>>(json);
            }
        }
        public bool ContainsKey(string key, string sharedName = null)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key, string sharedName = null)
        {
            throw new NotImplementedException();
        }

        public void Clear(string sharedName = null)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value, string sharedName = null)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key, T defaultValue, string sharedName = null)
        {
            throw new NotImplementedException();
        }
    }
}

