using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FishingGame.ViewModel
{
    public class FishingViewModel : ViewModelBase
    {
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

        private ImageSource _currentImage;
        public ImageSource CurrentImage
        {
            get => _currentImage;
            set
            {
                _currentImage = value;
                OnPropertyChanged(nameof(CurrentImage));
            }
        }
        private bool _isFishing = false;
        public bool IsFishing
        {
            get => _isFishing;
            set
            {
                _isFishing = value;
                OnPropertyChanged(nameof(IsFishing));
            }
        }
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
        private Rect _startFishingRect;
        public Rect StartFishingRect
        {
            get => _startFishingRect;
            set
            {
                _startFishingRect = value;
                OnPropertyChanged(nameof(StartFishingRect));
            }
        }
        public ICommand MoveLeftCommand { get; }
        public ICommand MoveRightCommand { get; }
        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }
        public ICommand StartFishingCommand { get; }
        public ICommand StopFishingCommand { get; }
        List<Uri> _hookUris = new List<Uri>
        {
            new Uri("Assets/Fishermen/FishermanAnimation/Animation1.png", UriKind.Relative),
            new Uri("Assets/Fishermen/FishermanAnimation/Animation2.png", UriKind.Relative),
            new Uri("Assets/Fishermen/FishermanAnimation/Animation3.png", UriKind.Relative),
            new Uri("Assets/Fishermen/FishermanAnimation/Animation4.png", UriKind.Relative),
        };

        private readonly List<Uri> moveUris = new List<Uri>
        {
            new Uri("Assets/Fishermen/FishermanMove1.png", UriKind.RelativeOrAbsolute),
            new Uri("Assets/Fishermen/FishermanMove2.png", UriKind.RelativeOrAbsolute),
            new Uri("Assets/Fishermen/Fisherman.png", UriKind.RelativeOrAbsolute)
        };
        public void UpdateBackground(string newImagePath)
        {
            BackgroundImage = new BitmapImage(new Uri(newImagePath, UriKind.RelativeOrAbsolute));
        }
        public FishingViewModel()
        {
            FishermanPositionLeft = 10;
            FishermanPositionTop = 10;

            MoveLeftCommand = new RelayCommand(_ => MoveLeft());
            MoveRightCommand = new RelayCommand(_ => MoveRight());
            MoveUpCommand = new RelayCommand(_ => MoveUp());
            MoveDownCommand = new RelayCommand(_ => MoveDown());
            StartFishingCommand = new RelayCommand(_ => StartFishing());
            StopFishingCommand = new RelayCommand(_ => StopFishing());
        }
        public async void StartFishing()
        {
            if (StartFishingCheckCollision() && !IsFishing)
            {
                IsFishing = true;
                await HookAnimation();
            }
        }

        public async void StopFishing()
        {
            if (IsFishing)
            {
                IsFishing = false;
                await HookAnimationReverse();
            }
        }
        public bool StartFishingCheckCollision()
        {
            return FishermanRect.IntersectsWith(StartFishingRect);
        }
        private async Task HookAnimation()
        {
            for (int i = 0 ; i <= _hookUris.Count - 1; ++i)
            {
                CurrentImage = new BitmapImage(_hookUris[i]);
                await Task.Delay(100);
            }
        }

        private async Task HookAnimationReverse()
        {
            for (int i = _hookUris.Count - 1; i >= 0; i--)
            {
                CurrentImage = new BitmapImage(_hookUris[i]);
                await Task.Delay(100);
            }
        }


        private void UpdateAnimation()
        {
            Thread.Sleep(15);

            ++_animationIndex;
                if (_animationIndex >= moveUris.Count)
                _animationIndex = 0;
            CurrentImage= new BitmapImage(moveUris[_animationIndex]);

            OnPropertyChanged(nameof(CurrentImage));
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
    }
}
