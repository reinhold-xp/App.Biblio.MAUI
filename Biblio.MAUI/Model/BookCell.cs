using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Biblio.MAUI.Model
{
	public class BookCell : INotifyPropertyChanged
    {
        public Book book { get; set; }
        public bool isFavorite { get; set; }
        public string imageSourceFav { get { return isFavorite ? "pin_true.png" : "pin_false.png"; } }
        public ICommand favClickCommand { get; set; }
        public Action<BookCell> favChangedAction { get; set; }

        public BookCell()
		{
            favClickCommand = new Command(() =>
            {

                // On alterne les images pin1 et pin2,
                // à chaque click sur l'image buttton,
                // quand la propriété isFavorite change
                isFavorite = !isFavorite;
                OnPropertyChanged(nameof(imageSourceFav));

                // Appel action (delegate) pour 
                // persister la liste des favoris
                favChangedAction.Invoke(this);
            });
        }

        // Implementation INotifyPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;


        // On signale les changements d'état
        // (param name = nom de la propriété qui a changé)
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}

