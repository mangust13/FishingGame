using System.Windows.Controls;
using System.Windows.Media.Imaging;
using FishingGame.View;

namespace FishingGame.Model
{
    public class MainFacade
    {
        public StartWindow startWindow;
        public FishingWindow fishingWindow;
        public Fisherman fisherman;
        public List<Fish> fishPrototypes = new List<Fish>();
        public List<Bait> Baits = new List<Bait>();
        public List<Rod> Rods = new List<Rod>();

        Rod rod;
        Bait bait;
        public string LocationBackground { get; set; }
        public MainFacade() { }
        public void StartGame(string location)
        {
            InitializeFishPrototypes();
            InitializeBaits();
            InitializeRods();

            bait = Baits[0].Clone();
            rod = Rods[0].Clone();
            LocationBackground = location == "Sea" ? "/Assets/Sea.png" : "/Assets/Lake.png";
            fisherman = Fisherman.GetInstance(bait, rod,
                    new BitmapImage(new Uri("/Assets/Fishermen/Fisherman.png", UriKind.Relative)));

            OpenNewWindow();
            InitializeShop();
        }
        private void OpenNewWindow()
        {
            fishingWindow = new FishingWindow(this);
            startWindow?.Close();
            fishingWindow.Show();
        }

        public void SetStartWindow(StartWindow window)
        {
            startWindow = window;
        }
        public void ShowEndGameWindow()
        {
            var endGameWindow = new EndWindow();
            endGameWindow.Show();
        }

        private void InitializeFishPrototypes()
        {
            fishPrototypes.Add(new Fish("Crucian", 5, 5, new BitmapImage(new Uri("../Assets/Fishes/Crucian.png", UriKind.Relative))));//карась
            fishPrototypes.Add(new Fish("Perch", 10, 10, new BitmapImage(new Uri("../Assets/Fishes/Perch.png", UriKind.Relative))));//окунь
            fishPrototypes.Add(new Fish("Salmon", 15, 15, new BitmapImage(new Uri("../Assets/Fishes/Salmon.png", UriKind.Relative))));//лосось
            fishPrototypes.Add(new Fish("Flounder", 15, 15, new BitmapImage(new Uri("../Assets/Fishes/Flounder.png", UriKind.Relative))));//камбала
            fishPrototypes.Add(new Fish("Tuna", 25, 25, new BitmapImage(new Uri("../Assets/Fishes/Tuna.png", UriKind.Relative))));//Тунець
            fishPrototypes.Add(new Fish("Sea Devil", 35, 35, new BitmapImage(new Uri("../Assets/Fishes/SeaDevil.png", UriKind.Relative))));//чорт
            fishPrototypes.Add(new Fish("Shark", 45, 100, new BitmapImage(new Uri("../Assets/Fishes/Shark.png", UriKind.Relative))));//Акула
        }

        private void InitializeBaits()
        {
            Baits.Add(new Bait("Lvl1", 0, 30, new BitmapImage(new Uri("../Assets/Baits/Bait1.jpg", UriKind.Relative))));
            Baits.Add(new Bait("Lvl2", 30, 40, new BitmapImage(new Uri("../Assets/Baits/Bait2.jpg", UriKind.Relative))));
            Baits.Add(new Bait("Lvl3", 50, 50, new BitmapImage(new Uri("../Assets/Baits/Bait3.jpg", UriKind.Relative))));
            Baits.Add(new Bait("Lvl4", 90, 60, new BitmapImage(new Uri("../Assets/Baits/Bait4.png", UriKind.Relative))));
            Baits.Add(new Bait("Lvl5", 130, 70, new BitmapImage(new Uri("../Assets/Baits/Bait5.jpg", UriKind.Relative))));
        }

        private void InitializeRods()
        {
            Rods.Add(new Rod("Lvl1", 0, 10, new BitmapImage(new Uri("../Assets/Rods/Rod1.jpg", UriKind.Relative))));
            Rods.Add(new Rod("Lvl2", 30, 20, new BitmapImage(new Uri("../Assets/Rods/Rod2.jpg", UriKind.Relative))));
            Rods.Add(new Rod("Lvl3", 50, 30, new BitmapImage(new Uri("../Assets/Rods/Rod3.jpg", UriKind.Relative))));
            Rods.Add(new Rod("Lvl4", 90, 40, new BitmapImage(new Uri("../Assets/Rods/Rod4.jpg", UriKind.Relative))));
            Rods.Add(new Rod("Lvl5", 130, 50, new BitmapImage(new Uri("../Assets/Rods/Rod5.jpg", UriKind.Relative))));
        }

        private void InitializeShop()
        {
            //Baits
            Bait bait2 = Baits[1].Clone();
            var imageBait2 = (Button)fishingWindow.FindName("Bait2");
            if (imageBait2 != null && imageBait2.Content is StackPanel panel2)
            {
                var image = panel2.Children.OfType<Image>().FirstOrDefault();
                image.Source = bait2.Image;
            }

            Bait bait3 = Baits[2].Clone();
            var imageBait3 = (Button)fishingWindow.FindName("Bait3");
            if (imageBait3 != null && imageBait3.Content is StackPanel panel3)
            {
                var image = panel3.Children.OfType<Image>().FirstOrDefault();
                image.Source = bait3.Image;
            }

            Bait bait4 = Baits[3].Clone();
            var imageBait4 = (Button)fishingWindow.FindName("Bait4");
            if (imageBait4 != null && imageBait4.Content is StackPanel panel4)
            {
                var image = panel4.Children.OfType<Image>().FirstOrDefault();
                image.Source = bait4.Image;
            }

            Bait bait5 = Baits[4].Clone();
            var imageBait5 = (Button)fishingWindow.FindName("Bait5");
            if (imageBait5 != null && imageBait5.Content is StackPanel panel5)
            {
                var image = panel5.Children.OfType<Image>().FirstOrDefault();
                image.Source = bait5.Image;
            }

            //Rods
            Rod rod2 = Rods[1].Clone();
            var imageRod2 = (Button)fishingWindow.FindName("Rod2");
            if (imageBait2 != null && imageBait2.Content is StackPanel panel6)
            {
                var image = panel6.Children.OfType<Image>().FirstOrDefault();
                image.Source = rod2.Image;
            }

            Rod rod3 = Rods[2].Clone();
            var imageRod3 = (Button)fishingWindow.FindName("Rod3");
            if (imageRod3 != null && imageRod3.Content is StackPanel panel7)
            {
                var image = panel7.Children.OfType<Image>().FirstOrDefault();
                image.Source = rod3.Image;
            }

            Rod rod4 = Rods[3].Clone();
            var imageRod4 = (Button)fishingWindow.FindName("Rod4");
            if (imageRod4 != null && imageRod4.Content is StackPanel panel8)
            {
                var image = panel8.Children.OfType<Image>().FirstOrDefault();
                image.Source = rod2.Image;
            }

            Rod rod5 = Rods[4].Clone();
            var imageRod5 = (Button)fishingWindow.FindName("Rod5");
            if (imageRod5 != null && imageRod5.Content is StackPanel panel9)
            {
                var image = panel9.Children.OfType<Image>().FirstOrDefault();
                image.Source = rod5.Image;
            }

        }
    }
}