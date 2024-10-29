using System.Windows;

namespace Lab4
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
