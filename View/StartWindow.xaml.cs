using FishingGame.Model;
using FishingGame.ViewModel;
using System.Windows;

namespace FishingGame.View
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }
        public StartWindow(MainFacade mainfacade) : this()
        {
            DataContext = new StartViewModel(mainfacade);
        }
    }
}