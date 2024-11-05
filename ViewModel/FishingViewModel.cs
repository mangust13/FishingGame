using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FishingGame.Model;

namespace FishingGame.ViewModel
{
    public class FishingViewModel : ViewModelBase
    {
        private MainFacade _mainFacade;
        private ImageSource _backgroundImage;
        public ImageSource BackgroundImage
        {
            get => _backgroundImage;
            set
            {
                _backgroundImage = value;
                OnPropertyChanged(nameof(BackgroundImage));
            }
        }

        private int _animationStep = 15;
        private int _animationIndex = 0;

        private double _fishermanPositionLeft;
        public double FishermanPositionLeft
        {
            get => _fishermanPositionLeft;
            set
            {
                _fishermanPositionLeft = value;
                OnPropertyChanged(nameof(FishermanPositionLeft));
            }
        }

        private double _fishermanPositionTop;
        public double FishermanPositionTop
        {
            get => _fishermanPositionTop;
            set
            {
                _fishermanPositionTop = value;
                OnPropertyChanged(nameof(FishermanPositionTop));
            }
        }

        private ImageSource _fishermanImage;
        public ImageSource FishermanImage
        {
            get => _fishermanImage;
            set
            {
                _fishermanImage = value;
                OnPropertyChanged(nameof(FishermanImage));
            }
        }

        private Popup _fishermanInfoPopup;
        public Popup FishermanInfoPopup
        {
            get => _fishermanInfoPopup;
            set
            {
                _fishermanInfoPopup = value;
                OnPropertyChanged(nameof(FishermanInfoPopup));
            }
        }
        public bool isFishing = false;


        private bool _isAnimating;
        private Rect _fishermanRect;
        public Rect FishermanRect
        {
            get => _fishermanRect;
            set
            {
                _fishermanRect = value;
                OnPropertyChanged(nameof(FishermanRect));
            }
        }
        private Rect _collisionRect;
        public Rect CollisionRect
        {
            get => _collisionRect;
            set
            {
                _collisionRect = value;
                OnPropertyChanged(nameof(CollisionRect));
            }
        }

        private Popup _fishMenu;
        public Popup FishMenu
        {
            get => _fishMenu;
            set
            {
                _fishMenu = value;
                OnPropertyChanged(nameof(FishMenu));
            }
        }

        private Popup _shopMenu;
        public Popup ShopMenu
        {
            get => _shopMenu;
            set
            {
                _shopMenu = value;
                OnPropertyChanged(nameof(ShopMenu));
            }
        }

        public ICommand MoveLeftCommand { get; }
        public ICommand MoveRightCommand { get; }
        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }
        public ICommand FishingCommand { get; }
        public ICommand OpenShopCommand { get; }
        public ICommand CatchFishCommand { get; }

        List<Uri> _hookUris = new List<Uri>
        {
            new Uri("../Assets/Fishermen/FishermanAnimation/Animation1.png", UriKind.RelativeOrAbsolute),
            new Uri("../Assets/Fishermen/FishermanAnimation/Animation2.png", UriKind.RelativeOrAbsolute),
            new Uri("../Assets/Fishermen/FishermanAnimation/Animation3.png", UriKind.RelativeOrAbsolute),
            new Uri("../Assets/Fishermen/FishermanAnimation/Animation4.png", UriKind.RelativeOrAbsolute),
        };
        private readonly List<Uri> moveUris = new List<Uri>
        {
            new Uri("../Assets/Fishermen/FishermanMove2.png", UriKind.RelativeOrAbsolute),
            new Uri("../Assets/Fishermen/FishermanMove1.png", UriKind.RelativeOrAbsolute),
            new Uri("../Assets/Fishermen/Fisherman.png", UriKind.RelativeOrAbsolute)
        };

        public void UpdateFisherman(int index)
        {
            fisherman.Image = new BitmapImage(moveUris[index]);

            FishermanImage = fisherman.Image;
            OnPropertyChanged(nameof(FishermanImage));
        }
        Fisherman fisherman;
        public void UpdateBackground(string newImagePath)
        {
            BackgroundImage = new BitmapImage(new Uri(newImagePath, UriKind.RelativeOrAbsolute));
        }
        public FishingViewModel(MainFacade gameFacade)
        {
            _mainFacade = gameFacade;
            FishermanPositionLeft = 10;
            FishermanPositionTop = 10;
            fisherman = Fisherman.GetInstance(null, null, null);
            FishermanImage = fisherman.Image;

            MoveLeftCommand = new RelayCommand(_ => MoveLeft());
            MoveRightCommand = new RelayCommand(_ => MoveRight());
            MoveUpCommand = new RelayCommand(_ => MoveUp());
            MoveDownCommand = new RelayCommand(_ => MoveDown());
            FishingCommand = new RelayCommand(_ => Fishing());
            OpenShopCommand = new RelayCommand(_ => OpenShop());
            CatchFishCommand = new RelayCommand(parameter => CatchFish(parameter));
        }
        public async void Fishing()
        {
            if (!FishMenu.IsOpen && CheckCollision() && !_isAnimating)
            {
                await HookAnimation();
                FishMenu.IsOpen = true;
                DisplayFishByWeightCapacity();
            }
            else if (FishMenu.IsOpen && CheckCollision() && !_isAnimating)
            {
                await HookAnimationReverse();
                FishMenu.IsOpen = false;
            }
        }

        public bool CheckCollision()
        {
            return FishermanRect.IntersectsWith(CollisionRect);
        }
        private async Task HookAnimation()
        {
            _isAnimating = true;
            for (int i = 0; i <= _hookUris.Count - 1; ++i)
            {
                FishermanImage = new BitmapImage(_hookUris[i]);
                await Task.Delay(100);
            }
            _isAnimating = false;
        }

        private async Task HookAnimationReverse()
        {
            _isAnimating = true;
            for (int i = _hookUris.Count - 1; i >= 0; i--)
            {
                FishermanImage = new BitmapImage(_hookUris[i]);
                await Task.Delay(100);
            }
            _isAnimating = false;
        }

        private void UpdateAnimation()
        {
            Thread.Sleep(15);

            ++_animationIndex;
            if (_animationIndex >= moveUris.Count)
                _animationIndex = 0;
            UpdateFisherman(_animationIndex);
        }
        private void MoveLeft()
        {
            FishermanPositionLeft -= _animationStep;
            UpdateAnimation();
        }
        private void MoveRight()
        {
            FishermanPositionLeft += _animationStep;
            UpdateAnimation();
        }
        private void MoveUp()
        {
            FishermanPositionTop -= _animationStep;
            UpdateAnimation();
        }
        private void MoveDown()
        {
            FishermanPositionTop += _animationStep;
            UpdateAnimation();
        }
        private void OpenShop()
        {
            ShopMenu.IsOpen = false;
            if (!ShopMenu.IsOpen & CheckCollision())
            {
                ShopMenu.IsOpen = true;
            }
            else if (ShopMenu.IsOpen & CheckCollision())
            {
                ShopMenu.IsOpen = false;
            }
        }

        public void DisplayFishByWeightCapacity()
        {
            List<Fish> fishToShow = _mainFacade.fishPrototypes
                .Where(f => f.Size <= _mainFacade.fisherman.rod.WeightCapacity)
                .ToList();

            if (fishToShow.Count >= 2)
            {
                FishMenu.Width = 100;

                Fish crucian = fishToShow[0].Clone();
                Button crucianButton = (Button)FishMenu.FindName("CrucianButton");
                if (crucianButton != null)
                {
                    crucianButton.Content = new Image { Source = crucian.Image, Stretch = Stretch.Fill };
                }

                Fish perch = fishToShow[1].Clone();
                Button perchButton = (Button)FishMenu.FindName("PerchButton");
                if (perchButton != null)
                {
                    perchButton.Content = new Image { Source = perch.Image, Stretch = Stretch.Fill };
                }
            }
            if (fishToShow.Count >= 4)
            {
                FishMenu.Width = 200;

                Fish salmon = fishToShow[2].Clone();
                Button salmonButton = (Button)FishMenu.FindName("SalmonButton");
                if (salmonButton != null)
                {
                    salmonButton.Content = new Image { Source = salmon.Image, Stretch = Stretch.Fill };
                }

                Fish flounder = fishToShow[3].Clone();
                Button flounderButton = (Button)FishMenu.FindName("FlounderButton");
                if (flounderButton != null)
                {
                    flounderButton.Content = new Image { Source = flounder.Image, Stretch = Stretch.Fill };
                }
            }
            if (fishToShow.Count >= 5)
            {
                FishMenu.Width = 250;

                Fish tuna = fishToShow[4].Clone();
                Button tunaButton = (Button)FishMenu.FindName("TunaButton");
                if (tunaButton != null)
                {
                    tunaButton.Content = new Image { Source = tuna.Image, Stretch = Stretch.Fill };
                }
            }
            if (fishToShow.Count >= 6)
            {
                FishMenu.Width = 300;

                Fish seaDevil = fishToShow[5].Clone();
                Button seaDevilButton = (Button)FishMenu.FindName("SeaDevilButton");
                if (seaDevilButton != null)
                {
                    seaDevilButton.Content = new Image { Source = seaDevil.Image, Stretch = Stretch.Fill };
                }
            }
            if (fishToShow.Count >= 7)
            {
                FishMenu.Width = 350;

                Fish shark = fishToShow[6].Clone();
                Button sharkButton = (Button)FishMenu.FindName("SharkButton");
                if (sharkButton != null)
                {
                    sharkButton.Content = new Image { Source = shark.Image, Stretch = Stretch.Fill };
                }
            }
        }

        private async Task CheckEndGameCondition(Fish selectedFish)
        {
            await Task.Delay(50);

            if (selectedFish.Name == "Shark")
            {
                _mainFacade.ShowEndGameWindow();
                Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive)?.Close();
            }
        }
        public void DisplayFishCost()
        {
            _totalCost = _caughtFishList.Sum(fish => fish.Cost);
            OnPropertyChanged(nameof(TotalFishCostText));
        }
        private ObservableCollection<Fish> _caughtFishList = new ObservableCollection<Fish>();
        public ObservableCollection<Fish> CaughtFishList
        {
            get => _caughtFishList;
            set
            {
                _caughtFishList = value;
                OnPropertyChanged(nameof(CaughtFishList));
            }
        }

        public int _totalCost;
        public string TotalFishCostText
        {
            get => $"Total Fish Cost: {_totalCost}";
            private set
            {
                _totalCost = _caughtFishList.Sum(fish => fish.Cost);
                OnPropertyChanged(nameof(TotalFishCostText));
            }
        }

        public void UpdateFishermanInfoPopup()
        {
            _totalCost = _caughtFishList.Sum(fish => fish.Cost);
            OnPropertyChanged(nameof(TotalFishCostText));
        }

        public void AddFish(Fish fish)
        {
            CaughtFishList.Add(fish);
            UpdateFishermanInfoPopup();
        }

        private async void CatchFish(object parameter)
        {
            if (CaughtFishList.Count >= 7)
            {
                MessageBox.Show("Your inventory is full.");
                return;
            }

            if (parameter is string fishIndex)
            {
                int index = int.Parse((string)parameter);
                Fish selectedFish = _mainFacade.fishPrototypes[index].Clone();
                Random random = new Random();

                if (_mainFacade.fisherman.bait.Chance > random.NextDouble() * 100)
                {
                    AddFish(selectedFish);
                    await CheckEndGameCondition(selectedFish);

                }
                else
                {
                    AddFish(_mainFacade.fishPrototypes[0].Clone());
                }

                FishMenu.IsOpen = false;
                await HookAnimationReverse();
            }
        }
    }
}