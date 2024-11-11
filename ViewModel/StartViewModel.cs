using System.Collections.ObjectModel;
using System.Windows.Input;
using FishingGame.Model;

namespace FishingGame.ViewModel
{
    public class StartViewModel : ViewModelBase
    {
        private readonly MainFacade mainFacade;
        public ICommand StartGameCommand   { get; set; }

        private string _selectedLocation;
        public string SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                OnPropertyChanged(nameof(SelectedLocation));
            }
        }

        public ObservableCollection<string> Locations { get; set; }

        public StartViewModel(MainFacade facade)
        {
            SelectedLocation = "Sea";
            mainFacade = facade;
            Locations = new ObservableCollection<string>
            {
                "Lake", "Sea"
            };
            StartGameCommand = new RelayCommand(StartGame);
        }
        private void StartGame(object parameter)
        {
            mainFacade.StartGame(_selectedLocation);
        }
    }
}
