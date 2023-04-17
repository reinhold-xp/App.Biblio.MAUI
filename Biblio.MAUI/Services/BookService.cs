using Biblio.MAUI.Model;
using Newtonsoft.Json;

namespace Biblio.MAUI.Services;

public class BookService
{
    const String BASE_API_URL = "https://api.reinhold-info.com/books";
    HttpClient httpClient;

    public BookService()
    {
        this.httpClient = new HttpClient();
    }

    public async Task<List<Book>> GetBooksAsync()
    {
        String booksJson;
        List<Book> books = new();
        var response = await httpClient.GetAsync(BASE_API_URL);

        if (response.IsSuccessStatusCode)
        {
            booksJson = await response.Content.ReadAsStringAsync();
            books = JsonConvert.DeserializeObject<List<Book>>(booksJson);
        }
        return books;
    }
}

