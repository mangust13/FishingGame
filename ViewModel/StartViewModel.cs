using System.Collections.ObjectModel;
using System.Windows.Input;
using Lab4;

namespace FishingGame.ViewModel
{
    public class StartViewModel : ViewModelBase
    {
        private readonly MainFacade mainFacade;
        public ObservableCollection<string> Locations  { get; set; }
        public ICommand StartGameCommand   { get; set; }

        private string selectedLocation;
        private string SelectedLocation 
        {
            get { return selectedLocation; }
            set
            {
                selectedLocation = value;
                OnPropertyChanged(nameof(SelectedLocation));
            }
        }

        public StartViewModel(MainFacade facade)
        {
            mainFacade = facade;
            Locations = new ObservableCollection<string> { "Sea", "Lake" };
            SelectedLocation = Locations[0];
            StartGameCommand = new RelayCommand(StartGame);
        }
        private void StartGame(object parameter)
        {
            mainFacade.StartGame(SelectedLocation);
        }
    }
}
