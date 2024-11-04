using System.Windows;
using FishingGame.Model;
using FishingGame.View;

namespace FishingGame
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainFacade mainFacade = new MainFacade();
            StartWindow startWindow = new StartWindow(mainFacade);
            mainFacade.SetStartWindow(startWindow);
            startWindow.Show();
        }
    }
}
