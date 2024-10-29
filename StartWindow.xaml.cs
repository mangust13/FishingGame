using FishingGame.ViewModel;
using System.Windows;

namespace Lab4
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