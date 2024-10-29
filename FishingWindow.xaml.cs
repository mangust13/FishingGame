using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using FishingGame;

namespace Lab4
{
    public partial class FishingWindow : Window
    {
        MainFacade mainFacade;
        int MoveAnimationIndex = 0;
        double step = 15;
        private IState currentState;
        private ObservableCollection<Fish> caughtFishList = new ObservableCollection<Fish>();
        List<Uri> moveUris = new List<Uri>
        {
            new Uri("Assets/Fishermen/FishermanMove1.png", UriKind.Relative),
            new Uri("Assets/Fishermen/FishermanMove2.png", UriKind.Relative),
            new Uri("Assets/Fishermen/Fisherman.png", UriKind.Relative),
        };
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
            this.mainFacade = gameFacade;

            DataContext = gameFacade;
            BaitInfoPopup.DataContext = gameFacade.fisherman.bait;
            RodInfoPopup.DataContext = gameFacade.fisherman.rod;
            FishermanInfoPopup.DataContext = gameFacade.fisherman;

            gameFacade.fisherman.BaitChanged += OnBaitChanged;
            gameFacade.fisherman.RodChanged += OnRodChanged;

            this.KeyDown += SeaWindow_KeyDown;
            DisplayFishCost();
            currentState = new MoveState(this);
        }

        private void OnBaitChanged(object sender, EventArgs e)
        {
            BaitInfoPopup.DataContext = mainFacade.fisherman.bait;
            BaitIcon.Source = mainFacade.fisherman.bait.Image;
        }

        private void OnRodChanged(object sender, EventArgs e)
        {
            RodInfoPopup.DataContext = mainFacade.fisherman.rod;
            RodIcon.Source = mainFacade.fisherman.rod.Image;
        }

        

        
        private void MooveAnimation()
        {
            ++MoveAnimationIndex;
            if (MoveAnimationIndex >= moveUris.Count)
                MoveAnimationIndex = 0;
            fishermanImage.Source = new BitmapImage(moveUris[MoveAnimationIndex]);
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
            List<Uri> reversedUris = new List<Uri>(hookUris);
            reversedUris.Reverse();
            foreach (Uri uri in reversedUris)
            {
                fishermanImage.Source = new BitmapImage(uri);
                await Task.Delay(100);
            }
        }

        private bool isFishing = false;

        private async void SeaWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
            case Key.Left:
            case Key.Right:
            case Key.Up:
            case Key.Down:
                currentState.HandleInput(e.Key);
                break;
            case Key.F:
                if (!isFishing && StartFishingCheckCollision())
                    StartFishing();
                else
                   StopFishing();
                break;
            case Key.E:
                currentState.HandleInput(e.Key);
                break;
            }

        }
        public void StartFishing()
        {
            currentState.HandleInput(Key.F);
            isFishing = true;
            currentState = new FishingState(this);
        }
        public void StopFishing()
        {
            currentState.HandleInput(Key.F);
            isFishing = false;
            currentState = new MoveState(this);
        }
        public void OpenShop()
        {
            CollectBaitCheckCollision();
            OpenShopCheckCollision();
            if (mainFacade.fisherman.bait != null)
                OnBaitChanged(this, EventArgs.Empty);
        }

        public void MoveFishermanLeft()
        {
            MooveAnimation();
            Thread.Sleep(45);
            fishermanImage.Margin = new Thickness(fishermanImage.Margin.Left - step, fishermanImage.Margin.Top, 0, 0);
            menuPopup.IsOpen = false;
            ShopPopup.IsOpen = false;
        }

        public void MoveFishermanRight()
        {
            MooveAnimation();
            Thread.Sleep(45);
            fishermanImage.Margin = new Thickness(fishermanImage.Margin.Left + step, fishermanImage.Margin.Top, 0, 0);
            menuPopup.IsOpen = false;
            ShopPopup.IsOpen = false;
        }

        public void MoveFishermanUp()
        {
            MooveAnimation();
            Thread.Sleep(45);
            fishermanImage.Margin = new Thickness(fishermanImage.Margin.Left, fishermanImage.Margin.Top - step, 0, 0);
            menuPopup.IsOpen = false;
            ShopPopup.IsOpen = false;
        }

        public void MoveFishermanDown()
        {
            MooveAnimation();
            Thread.Sleep(45);
            fishermanImage.Margin = new Thickness(fishermanImage.Margin.Left, fishermanImage.Margin.Top + step, 0, 0);
            menuPopup.IsOpen = false;
            ShopPopup.IsOpen = false;
        }
        public bool StartFishingCheckCollision()
        {
            Rect fishermanRect = new Rect(fishermanImage.Margin.Left, fishermanImage.Margin.Top, fishermanImage.ActualWidth, fishermanImage.ActualHeight);
            Rect rectangleRect = new Rect(StartFishingRect.Margin.Left, StartFishingRect.Margin.Top, StartFishingRect.ActualWidth, StartFishingRect.ActualHeight);

            if (fishermanRect.IntersectsWith(rectangleRect))
                return true;
            else
                return false;
        }
        public void CollectBaitCheckCollision()
        {
            Rect fishermanRect = new Rect(fishermanImage.Margin.Left, fishermanImage.Margin.Top, fishermanImage.ActualWidth, fishermanImage.ActualHeight);
            Rect rectangleRect = new Rect(BaitCollectRect.Margin.Left, BaitCollectRect.Margin.Top, BaitCollectRect.ActualWidth, BaitCollectRect.ActualHeight);

            if (fishermanRect.IntersectsWith(rectangleRect))
            {
                mainFacade.fisherman.bait = mainFacade.Baits[3].Clone();
                BaitCollect.Source = null;
            }
            else
                return;
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
            List<Fish> fishToShow = mainFacade.fishPrototypes.Where(f => f.Size <= mainFacade.fisherman.rod.WeightCapacity).ToList();

            crucianHandler.SetNext(perchHandler)
                           .SetNext(salmonHandler)
                           .SetNext(flounderHandler)
                           .SetNext(tunaHandler)
                           .SetNext(seaDevilHandler)
                           .SetNext(sharkHandler);

            // Запуск ланцюжка обробників
            crucianHandler.Handle(mainFacade, menuPopup, fishToShow);         
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
            Fish selectedFish = mainFacade.fishPrototypes[fishIndex].Clone();           
            Random random = new Random();

            if (mainFacade.fisherman.bait.Chance > random.NextDouble() * 100)
            {
                caughtFishList.Add(selectedFish);

                await CheckEndGameCondition(selectedFish);
            }
            else
                caughtFishList.Add(mainFacade.fishPrototypes[0].Clone());

            // Додати вибрану рибу до зв'язаного списку
            HookAnimationReverse();
            DisplayFishCost();
            isFishing = false;
            currentState = new MoveState(this);
            menuPopup.IsOpen = false;
            UpdateFishermanInfoPopup();
        }
        private async Task CheckEndGameCondition(Fish selectedFish)
        {
            await Task.Delay(50);

            if (selectedFish.Name == "Crucian")
            {
                mainFacade.ShowEndGameWindow();
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
                Bait selectedBait = mainFacade.Baits[baitIndex].Clone();

                if (totalCost >= selectedBait.Cost)
                {
                    totalCost -= selectedBait.Cost;
                    mainFacade.fisherman.bait = selectedBait;
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
                Rod selectedRod = mainFacade.Rods[rodIndex].Clone();

                if (totalCost >= selectedRod.Cost)
                {
                    totalCost -= selectedRod.Cost;
                    mainFacade.fisherman.rod = selectedRod;
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