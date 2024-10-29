using System.Windows.Input;
using System.Windows;

namespace FishingGame.ViewModel
{
    public class EndViewModel : ViewModelBase
    {
        public ICommand CloseCommand { get; }
        public EndViewModel()
        {
            CloseCommand = new RelayCommand(CloseWindow);
        }
        private void CloseWindow(object parameter)
        {
            if (parameter is Window window)
            {
                window.Close();
            }
        }
    }
}