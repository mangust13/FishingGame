using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using FishingGame.ViewModel;
using FishingGame.Model;

namespace FishingGame.View
{
    public partial class FishingWindow : Window
    {
        private FishingViewModel _viewModel;
        private MainFacade _mainFacade;

        public FishingWindow(MainFacade gameFacade)
        {
            InitializeComponent();
            _viewModel = new FishingViewModel(gameFacade);
            DataContext = _viewModel;
            _viewModel.UpdateBackground(gameFacade.LocationBackground);
            _viewModel.FishMenu = fishMenu;
            _viewModel.ShopMenu = shopMenu;
            
            _viewModel.FishermanInfoPopup = fishermanInfoPopup;

            _mainFacade = gameFacade;
            BaitInfoPopup.DataContext = gameFacade.fisherman.bait;
            RodInfoPopup.DataContext = gameFacade.fisherman.rod;
            

            gameFacade.fisherman.BaitChanged += OnBaitChanged;
            gameFacade.fisherman.RodChanged += OnRodChanged;

            this.KeyDown += FishingWindow_KeyDown;
            _viewModel.DisplayFishCost();
        }

        private void OnBaitChanged(object sender, EventArgs e)
        {
            BaitInfoPopup.DataContext = _mainFacade.fisherman.bait;
            BaitIcon.Source = _mainFacade.fisherman.bait.Image;
        }

        private void OnRodChanged(object sender, EventArgs e)
        {
            RodInfoPopup.DataContext = _mainFacade.fisherman.rod;
            RodIcon.Source = _mainFacade.fisherman.rod.Image;
        }

        private async void FishingWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (!_viewModel.isFishing)
                        _viewModel.MoveLeftCommand.Execute(null);
                    break;
                case Key.Right:
                    if (!_viewModel.isFishing)
                        _viewModel.MoveRightCommand.Execute(null);
                    break;
                case Key.Up:
                    if (!_viewModel.isFishing)
                        _viewModel.MoveUpCommand.Execute(null);
                    break;
                case Key.Down:
                    if (!_viewModel.isFishing)
                        _viewModel.MoveDownCommand.Execute(null);
                    break;
                case Key.F:
                    _viewModel.FishermanRect = new Rect(fishermanImage.Margin.Left, fishermanImage.Margin.Top, fishermanImage.ActualWidth, fishermanImage.ActualHeight);
                    _viewModel.CollisionRect = new Rect(StartFishingRect.Margin.Left, StartFishingRect.Margin.Top, StartFishingRect.ActualWidth, StartFishingRect.ActualHeight);
                    _viewModel.FishingCommand.Execute(null);
                    break;
                case Key.E:
                    _viewModel.FishermanRect = new Rect(fishermanImage.Margin.Left, fishermanImage.Margin.Top, fishermanImage.ActualWidth, fishermanImage.ActualHeight);
                    _viewModel.CollisionRect = new Rect(ShopRect.Margin.Left, ShopRect.Margin.Top, ShopRect.ActualWidth, ShopRect.ActualHeight);
                    _viewModel.OpenShopCommand.Execute(null);
                    break;
                default:
                    break;
            }
        }

        private void FishermanIcon_MouseDown(object sender, MouseEventArgs e)
        {
            fishermanInfoPopup.IsOpen = !fishermanInfoPopup.IsOpen;
            BaitInfoPopup.IsOpen = false;
            RodInfoPopup.IsOpen = false;
        }
        private void BaitIcon_MouseDown(object sender, MouseEventArgs e)
        {
            BaitInfoPopup.IsOpen = !BaitInfoPopup.IsOpen;
            fishermanInfoPopup.IsOpen = false;
            RodInfoPopup.IsOpen = false;
        }
        private void RodIcon_MouseDown(object sender, MouseEventArgs e)
        {
            RodInfoPopup.IsOpen = !RodInfoPopup.IsOpen;
            fishermanInfoPopup.IsOpen = false;
            BaitInfoPopup.IsOpen = false;
        }
        private void ShopImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image shopImage = sender as Image;
            string itemName = shopImage.Name;

            if (itemName.StartsWith("Bait"))
            {
                int baitIndex = int.Parse(itemName.Substring(4)) - 1;
                Bait selectedBait = _mainFacade.Baits[baitIndex].Clone();

                if (_viewModel._totalCost >= selectedBait.Cost)
                {
                    _viewModel._totalCost -= selectedBait.Cost;
                    _mainFacade.fisherman.bait = selectedBait;
                    OnBaitChanged(this, EventArgs.Empty);
                    shopMenu.IsOpen = false;
                    shopImage.Visibility = Visibility.Collapsed;

                    string priceTextBlockName = "Price" + itemName;
                    TextBlock priceTextBlock = shopMenu.FindName(priceTextBlockName) as TextBlock;
                    if (priceTextBlock != null)
                        priceTextBlock.Visibility = Visibility.Collapsed;
                }
                else
                    MessageBox.Show("You don't have enough money to buy this bait.");
            }
            else if (itemName.StartsWith("Rod"))
            {
                int rodIndex = int.Parse(itemName.Substring(3)) - 1;
                Rod selectedRod = _mainFacade.Rods[rodIndex].Clone();

                if (_viewModel._totalCost >= selectedRod.Cost)
                {
                    _viewModel._totalCost -= selectedRod.Cost;
                    _mainFacade.fisherman.rod = selectedRod;
                    OnRodChanged(this, EventArgs.Empty);
                    shopMenu.IsOpen = false;
                    shopImage.Visibility = Visibility.Collapsed;

                    string priceTextBlockName = "Price" + itemName;
                    TextBlock priceTextBlock = shopMenu.FindName(priceTextBlockName) as TextBlock;
                    if (priceTextBlock != null)
                        priceTextBlock.Visibility = Visibility.Collapsed;
                }
                else
                    MessageBox.Show("You don't have enough money to buy this rod.");
            }
            _viewModel.CaughtFishList.Clear();
            _viewModel.UpdateFishermanInfoPopup();
        }
    }
}