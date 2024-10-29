using FishingGame;
using System;
using System.Reflection;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace Lab4
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
        public string LocationBackground {  get; set; }
        public MainFacade(StartWindow startWindow)
        {
            this.startWindow = startWindow;
        }
        public void StartGame(string location, object sender)
        {
            InitializeFishPrototypes();
            InitializeBaits();
            InitializeRods();

            bait = Baits[0].Clone();
            rod = Rods[0].Clone();

            fisherman = Fisherman.GetInstance(bait, rod,
                    new BitmapImage(new Uri("Assets/Fishermen/Fisherman.png", UriKind.Relative)));

            if (location == "Sea")
                LocationBackground = "/Assets/Sea.png";
            else
                LocationBackground = "/Assets/Lake.png";
            
            OpenNewWindow();
            InitializeShop();
            InitilizeFishing(fishingWindow);
        }
        private void OpenNewWindow()
        {
            fishingWindow = new FishingWindow(this);
            startWindow.Close();
            fishingWindow.Show();
        }

        private void InitilizeFishing(FishingWindow fishingWindow)
        {
            Image fishermanImage = (Image)fishingWindow.FindName("fishermanImage");
            fishermanImage.Source = fisherman.Image;
        }
        
        private void InitializeFishPrototypes()
        {
            fishPrototypes.Add(new Fish("Crucian",  5,      5, new BitmapImage(new Uri("Assets/Fishes/Crucian.png", UriKind.Relative))));//карась
            fishPrototypes.Add(new Fish("Perch",    10,     10, new BitmapImage(new Uri("Assets/Fishes/Perch.png", UriKind.Relative))));//окунь
            fishPrototypes.Add(new Fish("Salmon",   15,     15, new BitmapImage(new Uri("Assets/Fishes/Salmon.png", UriKind.Relative))));//лосось
            fishPrototypes.Add(new Fish("Flounder", 15,     15, new BitmapImage(new Uri("Assets/Fishes/Flounder.png", UriKind.Relative))));//камбала
            fishPrototypes.Add(new Fish("Tuna",     25,     25, new BitmapImage(new Uri("Assets/Fishes/Tuna.png", UriKind.Relative))));//Тунець
            fishPrototypes.Add(new Fish("Sea Devil",35,     35, new BitmapImage(new Uri("Assets/Fishes/SeaDevil.png", UriKind.Relative))));//чорт
            fishPrototypes.Add(new Fish("Shark",    45,     100, new BitmapImage(new Uri("Assets/Fishes/Shark.png", UriKind.Relative))));//Акула
        }

        private void InitializeBaits()
        {
            Baits.Add(new Bait("Lvl1", 0, 30, new BitmapImage(new Uri("Assets/Baits/Bait1.jpg", UriKind.Relative))));
            Baits.Add(new Bait("Lvl2", 100, 40,  new BitmapImage(new Uri("Assets/Baits/Bait2.jpg", UriKind.Relative))));
            Baits.Add(new Bait("Lvl3", 200, 50,  new BitmapImage(new Uri("Assets/Baits/Bait3.jpg", UriKind.Relative))));
            Baits.Add(new Bait("Lvl4", 300, 60, new BitmapImage(new Uri("Assets/Baits/Bait4.png", UriKind.Relative))));
            Baits.Add(new Bait("Lvl5", 400, 70, new BitmapImage(new Uri("Assets/Baits/Bait5.jpg", UriKind.Relative))));
        }

        private void InitializeRods()
        {
            Rods.Add(new Rod("Lvl1", 0, 10, new BitmapImage(new Uri("Assets/Rods/Rod1.jpg", UriKind.Relative))));
            Rods.Add(new Rod("Lvl2", 100, 20, new BitmapImage(new Uri("Assets/Rods/Rod2.jpg", UriKind.Relative))));
            Rods.Add(new Rod("Lvl3", 200, 30, new BitmapImage(new Uri("Assets/Rods/Rod3.jpg", UriKind.Relative))));
            Rods.Add(new Rod("Lvl4", 300, 40, new BitmapImage(new Uri("Assets/Rods/Rod4.jpg", UriKind.Relative))));
            Rods.Add(new Rod("Lvl5", 400, 50, new BitmapImage(new Uri("Assets/Rods/Rod5.jpg", UriKind.Relative))));
        }

        private void InitializeShop()
        {
            Image image;
            Bait bait2 = Baits[1].Clone();
            image = (Image)fishingWindow.FindName("Bait2");
            image.Source = bait2.Image;

            Bait bait3 = Baits[2].Clone();
            image = (Image)fishingWindow.FindName("Bait3");
            image.Source = bait3.Image;

            Bait bait5 = Baits[4].Clone();
            image = (Image)fishingWindow.FindName("Bait5");
            image.Source = bait5.Image;

            Rod rod2 = Rods[1].Clone();
            image = (Image)fishingWindow.FindName("Rod2");
            image.Source = rod2.Image;

            Rod rod3 = Rods[2].Clone();
            image = (Image)fishingWindow.FindName("Rod3");
            image.Source = rod3.Image;

            Rod rod4 = Rods[3].Clone();
            image = (Image)fishingWindow.FindName("Rod4");
            image.Source = rod4.Image;

            Rod rod5 = Rods[4].Clone();
            image = (Image)fishingWindow.FindName("Rod5");
            image.Source = rod5.Image;
        }
    }
}