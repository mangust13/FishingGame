using System.Windows.Media;

namespace FishingGame.Model
{
    public class Rod
    {
        public Rod(string name, int cost, int weightCapacity, ImageSource image)
        {
            Name = name;
            Cost = cost;
            WeightCapacity = weightCapacity;
            Image = image;
        }

        public string Name { get; init; }
        public int Cost { get; init; }
        public int WeightCapacity { get; set; }
        public ImageSource Image { get; set; }

        public Rod Clone()
        {
            return new Rod(Name, Cost, WeightCapacity, Image);
        }
    }
}
