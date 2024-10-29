using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab4
{ 
    public partial class StartWindow : Window
    {
        private string selectedLocation = "";
        public MainFacade gameFacade;

        public StartWindow()
        {
            InitializeComponent();
            gameFacade = new MainFacade(this);
        }
        private void locationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedLocation = (locationComboBox.SelectedItem as ComboBoxItem).Content.ToString();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            gameFacade.StartGame(selectedLocation, sender);
        }
    }
    
}