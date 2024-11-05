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
            for (int i = 0 ; i <= _hookUris.Count - 1; ++i)
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
                Image crucianImage = (Image)FishMenu.FindName("CrucianImage");
                crucianImage.Source = crucian.Image;

                Fish perch = fishToShow[1].Clone();
                Image perchImage = (Image)FishMenu.FindName("PerchImage");
                perchImage.Source = perch.Image;
            }
            if (fishToShow.Count >= 4)
            {
                FishMenu.Width = 200;

                Fish salmon = fishToShow[2].Clone();
                Image salmonImage = (Image)FishMenu.FindName("SalmonImage");
                salmonImage.Source = salmon.Image;

                Fish flounder = fishToShow[3].Clone();
                Image flounderImage = (Image)FishMenu.FindName("FlounderImage");
                flounderImage.Source = flounder.Image;
            }
            if (fishToShow.Count >= 5)
            {
                FishMenu.Width = 250;

                Fish tuna = fishToShow[4].Clone();
                Image tunaImage = (Image)FishMenu.FindName("TunaImage");
                tunaImage.Source = tuna.Image;
            }
            if (fishToShow.Count >= 6)
            {
                FishMenu.Width = 300;

                Fish seaDevil = fishToShow[5].Clone();
                Image seaDevilImage = (Image)FishMenu.FindName("SeaDevilImage");
                seaDevilImage.Source = seaDevil.Image;
            }
            if (fishToShow.Count >= 7)
            {
                FishMenu.Width = 350;

                Fish shark = fishToShow[6].Clone();
                Image sharkImage = (Image)FishMenu.FindName("SharkImage");
                sharkImage.Source = shark.Image;
            }
        }

    }
}
