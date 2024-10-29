using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab4
{
    public class FishermanSingleton
    {
        private static FishermanSingleton instance;
        private Fisherman currentFisherman; // Поточний рибак

        private FishermanSingleton() { }

        public static FishermanSingleton GetInstance()
        {
            if (instance == null)
                instance = new FishermanSingleton();

            return instance;
        }

        public void ChooseFisherman(Bait bait, Rod rod, ImageSource image)
        {
            currentFisherman = new Fisherman(bait, rod, image);
        }

        public Fisherman GetCurrentFisherman()
        {
            return currentFisherman;
        }
    }

}