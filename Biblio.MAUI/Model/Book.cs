using System;
namespace Biblio.MAUI.Model
{
	public class Book
	{
        public int id { get; set; }
        public string titre { get; set; }
        public string auteurId { get; set; }
        public string auteurNom { get; set; }
        public int pages { get; set; }
        public string image { get; set; }
        public string resume { get; set; }
     
        // Propriétés dérivées pour faciliter l'affichage
        public string imageUrl { get { return $"{AppConfig.ImageBaseUrl}{image}"; } }
        public string nbPagesToString { get { return pages + " pages"; } }

        public Book()
		{
		}
	}
}

