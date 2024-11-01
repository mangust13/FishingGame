using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using FishingGame;
using FishingGame.ViewModel;
using System;

namespace FishingGame
{
    public partial class FishingWindow : Window
    {
        private FishingViewModel _viewModel;
        private MainFacade _mainFacade;
        private bool isFishing = false;
        private ObservableCollection<Fish> caughtFishList = new ObservableCollection<Fish>();

        List<Uri> hookUris = new List<Uri>
        {
            new Uri("Assets/Fishermen/FishermanAnimation/Animation1.png", UriKind.Relative),
            new Uri("Assets/Fishermen/FishermanAnimation/Animation2.png", UriKind.Relative),
            new Uri("Assets/Fishermen/FishermanAnimation/Animation3.png", UriKind.Relative),
            new Uri("Assets/Fishermen/FishermanAnimation/Animation4.png", UriKind.Relative),
        };

        public FishingWindow(MainFacade gameFacade)
        {
            InitializeComponent();
            _viewModel = new FishingViewModel();
            _mainFacade = gameFacade;
            DataContext = _viewModel;

            BaitInfoPopup.DataContext = gameFacade.fisherman.bait;
            RodInfoPopup.DataContext = gameFacade.fisherman.rod;
            FishermanInfoPopup.DataContext = gameFacade.fisherman;
            

            gameFacade.fisherman.BaitChanged += OnBaitChanged;
            gameFacade.fisherman.RodChanged += OnRodChanged;

            this.KeyDown += FishingWindow_KeyDown;
            DisplayFishCost();
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
     
        public async void HookAnimation()
        {
            foreach (Uri uri in hookUris)
            {
                fishermanImage.Source = new BitmapImage(uri);
                await Task.Delay(100);
            }
        }
        public async void HookAnimationReverse()
        {
            for (int i = hookUris.Count - 1; i >= 0; i--)
            {
                fishermanImage.Source = new BitmapImage(hookUris[i]);
                await Task.Delay(100);
            }
        }

        private async void FishingWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    _viewModel.MoveLeftCommand.Execute(null);
                    break;
                case Key.Right:
                    _viewModel.MoveRightCommand.Execute(null);
                    break;
                case Key.Up:
                    _viewModel.MoveUpCommand.Execute(null);
                    break;
                case Key.Down:
                    _viewModel.MoveDownCommand.Execute(null);
                    break;
                case Key.F:
                    if (!isFishing && StartFishingCheckCollision())
                        StartFishing();
                    else if (StartFishingCheckCollision())
                        StopFishing();
                    break;
                case Key.E:
                    OpenShop();
                    break;
            }
        }
        public void StartFishing()
        {
            isFishing = true;
            HookAnimation();
            menuPopup.IsOpen = true;
            DisplayFishByWeightCapacity();
        }
        public void StopFishing()
        {
            isFishing = false;
            HookAnimationReverse();
            menuPopup.IsOpen = false;
        }
        public void OpenShop()
        {
            CollectBaitCheckCollision();
            OpenShopCheckCollision();
            if (_mainFacade.fisherman.bait != null)
                OnBaitChanged(this, EventArgs.Empty);
        }

        public bool StartFishingCheckCollision()
        {
            Rect fishermanRect = new Rect(fishermanImage.Margin.Left, fishermanImage.Margin.Top, fishermanImage.ActualWidth, fishermanImage.ActualHeight);
            Rect rectangleRect = new Rect(StartFishingRect.Margin.Left, StartFishingRect.Margin.Top, StartFishingRect.ActualWidth, StartFishingRect.ActualHeight);

            return fishermanRect.IntersectsWith(rectangleRect);
        }
        public void CollectBaitCheckCollision()
        {
            Rect fishermanRect = new Rect(fishermanImage.Margin.Left, fishermanImage.Margin.Top, fishermanImage.ActualWidth, fishermanImage.ActualHeight);
            Rect rectangleRect = new Rect(BaitCollectRect.Margin.Left, BaitCollectRect.Margin.Top, BaitCollectRect.ActualWidth, BaitCollectRect.ActualHeight);

            if (fishermanRect.IntersectsWith(rectangleRect))
            {
                _mainFacade.fisherman.bait = _mainFacade.Baits[3].Clone();
                BaitCollect.Source = null;
            }
        }

        public void OpenShopCheckCollision()
        {
            Rect fishermanRect = new Rect(fishermanImage.Margin.Left, fishermanImage.Margin.Top, fishermanImage.ActualWidth, fishermanImage.ActualHeight);
            Rect shopRect = new Rect(ShopRect.Margin.Left, ShopRect.Margin.Top, ShopRect.ActualWidth, ShopRect.ActualHeight);

            if (fishermanRect.IntersectsWith(shopRect))
            {
                ShopPopup.IsOpen = !ShopPopup.IsOpen;
            }
        }

        public void DisplayFishByWeightCapacity()
        {
            var crucianHandler = new CrucianHandler();
            var perchHandler = new PerchHandler();
            var salmonHandler = new SalmonHandler();
            var flounderHandler = new FlounderHandler();
            var tunaHandler = new TunaHandler();
            var seaDevilHandler = new SeaDevilHandler();
            var sharkHandler = new SharkHandler();
            List<Fish> fishToShow = _mainFacade.fishPrototypes.Where(f => f.Size <= _mainFacade.fisherman.rod.WeightCapacity).ToList();

            crucianHandler.SetNext(perchHandler)
                           .SetNext(salmonHandler)
                           .SetNext(flounderHandler)
                           .SetNext(tunaHandler)
                           .SetNext(seaDevilHandler)
                           .SetNext(sharkHandler);

            // Запуск ланцюжка обробників
            crucianHandler.Handle(_mainFacade, menuPopup, fishToShow);
        }

        private void FishermanIcon_MouseDown(object sender, MouseEventArgs e)
        {
            FishermanInfoPopup.IsOpen = !FishermanInfoPopup.IsOpen;
            BaitInfoPopup.IsOpen = false;
            RodInfoPopup.IsOpen = false;
        }
        private void BaitIcon_MouseDown(object sender, MouseEventArgs e)
        {
            BaitInfoPopup.IsOpen = !BaitInfoPopup.IsOpen;
            FishermanInfoPopup.IsOpen = false;
            RodInfoPopup.IsOpen = false;
        }
        private void RodIcon_MouseDown(object sender, MouseEventArgs e)
        {
            RodInfoPopup.IsOpen = !RodInfoPopup.IsOpen;
            FishermanInfoPopup.IsOpen = false;
            BaitInfoPopup.IsOpen = false;
        }

        private async void FishImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (caughtFishList.Count >= 7)
            {
                MessageBox.Show("Your inventory is full.");
                return;
            }

            int fishIndex = menuPanel.Children.IndexOf((UIElement)sender);
            Fish selectedFish = _mainFacade.fishPrototypes[fishIndex].Clone();
            Random random = new Random();

            if (_mainFacade.fisherman.bait.Chance > random.NextDouble() * 100)
            {
                caughtFishList.Add(selectedFish);

                await CheckEndGameCondition(selectedFish);
            }
            else
                caughtFishList.Add(_mainFacade.fishPrototypes[0].Clone());

            // Додати вибрану рибу до зв'язаного списку
            HookAnimationReverse();
            DisplayFishCost();
            isFishing = false;
            menuPopup.IsOpen = false;
            UpdateFishermanInfoPopup();
        }
        private async Task CheckEndGameCondition(Fish selectedFish)
        {
            await Task.Delay(50);

            if (selectedFish.Name == "Shark")
            {
                _mainFacade.ShowEndGameWindow();
                this.Close();
            }
        }

        int totalCost = 0;
        private void DisplayFishCost()
        {
            foreach (Fish fish in caughtFishList)
                totalCost += fish.Cost;

            FishCostText.Text = "Total Fish Cost: " + totalCost.ToString();
        }
        private void UpdateFishermanInfoPopup()
        {
            var itemsControl = FindChild<ItemsControl>(FishermanInfoPopup.Child);
            if (itemsControl != null)
            {
                itemsControl.ItemsSource = caughtFishList;
            }
        }

        private T FindChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild)
                {
                    return typedChild;
                }

                var result = FindChild<T>(child);
                if (result != null) return result;
            }

            return null;
        }

        private void ShopImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image shopImage = sender as Image;
            string itemName = shopImage.Name;

            if (itemName.StartsWith("Bait"))
            {
                int baitIndex = int.Parse(itemName.Substring(4)) - 1;
                Bait selectedBait = _mainFacade.Baits[baitIndex].Clone();

                if (totalCost >= selectedBait.Cost)
                {
                    totalCost -= selectedBait.Cost;
                    _mainFacade.fisherman.bait = selectedBait;
                    OnBaitChanged(this, EventArgs.Empty);
                    ShopPopup.IsOpen = false;
                    shopImage.Visibility = Visibility.Collapsed;

                    string priceTextBlockName = "Price" + itemName;
                    TextBlock priceTextBlock = ShopPopup.FindName(priceTextBlockName) as TextBlock;
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

                if (totalCost >= selectedRod.Cost)
                {
                    totalCost -= selectedRod.Cost;
                    _mainFacade.fisherman.rod = selectedRod;
                    OnRodChanged(this, EventArgs.Empty);
                    ShopPopup.IsOpen = false;
                    shopImage.Visibility = Visibility.Collapsed;

                    string priceTextBlockName = "Price" + itemName;
                    TextBlock priceTextBlock = ShopPopup.FindName(priceTextBlockName) as TextBlock;
                    if (priceTextBlock != null)
                        priceTextBlock.Visibility = Visibility.Collapsed;
                }
                else
                    MessageBox.Show("You don't have enough money to buy this rod.");
            }
            FishCostText.Text = "Total Fish Cost: " + totalCost.ToString();
            caughtFishList.Clear();
            UpdateFishermanInfoPopup();
        }
    }
}
