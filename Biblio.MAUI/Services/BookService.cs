using Biblio.MAUI.Model;
using Newtonsoft.Json;

namespace Biblio.MAUI.Services;

public class BookService
{
    HttpClient httpClient;

    public BookService()
    {
        this.httpClient = new HttpClient();
    }

    public async Task<List<Book>> GetBooksAsync()
    {
        try
        {
            var response = await httpClient.GetAsync(AppConfig.ApiBaseUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var books = JsonConvert.DeserializeObject<List<Book>>(content) ?? new List<Book>();
                return books;
            }
            else
            {
                Console.WriteLine($"BookService : {response.StatusCode}");
                return new List<Book>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"BookService : {ex.Message}");
            return new List<Book>();
        }
    }
}

