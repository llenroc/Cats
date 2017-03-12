using Cats.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cats.ViewModels
{
    public class CatsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(
            [CallerMemberName]
            string propertyName = null) =>
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(propertyName));

        private bool Busy;

        public bool IsBusy {
            get {
                return Busy;
            }

            set {
                Busy = value;
                OnPropertyChanged();
                GetCatsCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<Cat> Cats { get; set; }

        public Command GetCatsCommand { get; set; }

        public CatsViewModel()
        {
            Cats = new ObservableCollection<Cat>();

            GetCatsCommand = new Command(
                async () => await GetCats(), 
                () => !IsBusy
            );
        }

        async Task GetCats() {

            if (!IsBusy) {
                Exception Error = null;

                try {
                    IsBusy = true;

                    var Repository = new Repository();
                    var Items = await Repository.GetCatsAsAzure();
                    //var Items = await Repository.GetCats();

                    Cats.Clear();

                    foreach (var Cat in Items)
                    {
                        Cats.Add(Cat);
                    }
                } catch (Exception ex) {
                    Error = ex;

                    if (Error != null) {
                        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error!", Error.Message, "OK");
                    }
                } finally {
                    IsBusy = false;
                }
            }

            return;
        }
    }
}
