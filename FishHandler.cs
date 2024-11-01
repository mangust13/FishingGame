using System.Windows.Controls;
using System.Windows.Controls.Primitives;


namespace FishingGame
{
    public abstract class FishHandler
    {
        protected FishHandler _nextHandler;

        public FishHandler SetNext(FishHandler nextHandler)
        {
            _nextHandler = nextHandler;
            return nextHandler;
        }

        public abstract void Handle(MainFacade gameFacade, Popup menuPopup, List<Fish> fishToShow);
    }

    public class CrucianHandler : FishHandler
    {
        public override void Handle(MainFacade gameFacade, Popup menuPopup, List<Fish> fishToShow)
        {
            if (fishToShow.Count >= 2)
            {
                menuPopup.Width = 100;

                Fish crucian = gameFacade.fishPrototypes[0].Clone();
                Image image = (Image)menuPopup.FindName("CrucianImage");
                image.Source = crucian.Image;

                _nextHandler?.Handle(gameFacade, menuPopup, fishToShow);
            }
            return;
        }
    }

    public class PerchHandler : FishHandler
    {
        public override void Handle(MainFacade gameFacade, Popup menuPopup, List<Fish> fishToShow)
        {
                Fish perch = gameFacade.fishPrototypes[1].Clone();
                Image image = (Image)menuPopup.FindName("PerchImage");
                image.Source = perch.Image;

                _nextHandler?.Handle(gameFacade, menuPopup, fishToShow);
                return;

        }
    }
    public class SalmonHandler : FishHandler
    {
        public override void Handle(MainFacade gameFacade, Popup menuPopup, List<Fish> fishToShow)
        {
            if (fishToShow.Count >= 4)
            {
                menuPopup.Width = 200;
                Fish salmon = gameFacade.fishPrototypes[2].Clone();
                Image image = (Image)menuPopup.FindName("SalmonImage");
                image.Source = salmon.Image;

                _nextHandler?.Handle(gameFacade, menuPopup, fishToShow);
            }
            return;

        }
    }
    public class FlounderHandler : FishHandler
    {
        public override void Handle(MainFacade gameFacade, Popup menuPopup, List<Fish> fishToShow)
        {

            Fish flounder = gameFacade.fishPrototypes[3].Clone();
            Image image = (Image)menuPopup.FindName("FlounderImage");
            image.Source = flounder.Image;

            _nextHandler?.Handle(gameFacade, menuPopup, fishToShow);

            return;

        }
    }

    public class TunaHandler : FishHandler
    {
        public override void Handle(MainFacade gameFacade, Popup menuPopup, List<Fish> fishToShow)
        {
            if (fishToShow.Count >= 5)
            {
                menuPopup.Width = 250;
                Fish tuna = gameFacade.fishPrototypes[4].Clone();
                Image image = (Image)menuPopup.FindName("TunaImage");
                image.Source = tuna.Image;

                _nextHandler?.Handle(gameFacade, menuPopup, fishToShow);
            }
            return;
        }
    }

    public class SeaDevilHandler : FishHandler
    {
        public override void Handle(MainFacade gameFacade, Popup menuPopup, List<Fish> fishToShow)
        {
            if (fishToShow.Count >= 6)
            {
                menuPopup.Width = 350;
                Fish seaDevil = gameFacade.fishPrototypes[5].Clone();
                Image image = (Image)menuPopup.FindName("SeaDevilImage");
                image.Source = seaDevil.Image;

                _nextHandler?.Handle(gameFacade, menuPopup, fishToShow);
            }
            return;
        }
    }

    public class SharkHandler : FishHandler
    {
        public override void Handle(MainFacade gameFacade, Popup menuPopup, List<Fish> fishToShow)
        {
            if (fishToShow.Count >= 7)
            {
                menuPopup.Width = 400;
                Fish shark = gameFacade.fishPrototypes[6].Clone();
                Image image = (Image)menuPopup.FindName("SharkImage");
                image.Source = shark.Image;

                _nextHandler?.Handle(gameFacade, menuPopup, fishToShow);
            }
            return;
        }
    }
}
