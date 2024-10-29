using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lab4
{
    public interface IState
    {
        void HandleInput(Key key);
    }
    public class MoveState : IState
    {
        private readonly FishingWindow _window;

        public MoveState(FishingWindow window)
        {
            _window = window;
        }

        public void HandleInput(Key key)
        {
            switch (key)
            {
                case Key.Left:
                    _window.MoveFishermanLeft();
                    break;
                case Key.Right:
                    _window.MoveFishermanRight();
                    break;
                case Key.Up:
                    _window.MoveFishermanUp();
                    break;
                case Key.Down:
                    _window.MoveFishermanDown();
                    break;
                case Key.F:
                    if (_window.StartFishingCheckCollision())
                    {
                        _window.HookAnimation();
                        _window.menuPopup.IsOpen = true;
                        _window.DisplayFishByWeightCapacity();
                    }
                        break;
                case Key.E:
                    _window.OpenShop();
                    break;
            }
        }
    }

    public class FishingState : IState
    {
        private readonly FishingWindow _window;

        public FishingState(FishingWindow window)
        {
            _window = window;
        }

        public void HandleInput(Key key)
        {
            switch (key)
            {
                case Key.Left:
                    break;
                case Key.Right:
                    break;
                case Key.Up:
                    break;
                case Key.Down:
                    break;
                case Key.F:
                    _window.HookAnimationReverse();
                    break;
                case Key.E:
                    _window.OpenShop();
                    break;
            }
        }
    }
}
