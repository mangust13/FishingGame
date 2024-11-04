using FishingGame.ViewModel;
using System.Windows;

namespace FishingGame.View
{
    public partial class EndWindow : Window
    {
        public EndWindow()
        {
            InitializeComponent();
            DataContext = new EndViewModel();
        }
    }
}