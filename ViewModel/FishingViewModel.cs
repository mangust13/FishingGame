using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public ICommand MoveLeftCommand { get; }
        public ICommand MoveRightCommand { get; }
        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }
        public void UpdateBackground(string newImagePath)
        {
            BackgroundImage = new BitmapImage(new Uri(newImagePath, UriKind.RelativeOrAbsolute));
        }


        private readonly List<Uri> moveUris = new List<Uri>
        {
            new Uri("Assets/Fishermen/FishermanMove1.png", UriKind.RelativeOrAbsolute),
            new Uri("Assets/Fishermen/FishermanMove2.png", UriKind.RelativeOrAbsolute),
            new Uri("Assets/Fishermen/Fisherman.png", UriKind.RelativeOrAbsolute)
        };

        public FishingViewModel()
        {
            
            FishermanPositionLeft = 0;
            FishermanPositionTop = 10;

            MoveLeftCommand = new RelayCommand(_ => MoveLeft());
            MoveRightCommand = new RelayCommand(_ => MoveRight());
            MoveUpCommand = new RelayCommand(_ => MoveUp());
            MoveDownCommand = new RelayCommand(_ => MoveDown());

            CurrentImage = new BitmapImage(moveUris[0]);
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
