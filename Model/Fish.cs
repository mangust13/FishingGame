using System.Windows.Media;

namespace FishingGame.Model
{
    public class Fish
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public int Cost { get; set; }
        public ImageSource Image { get; set; }

        public Fish(string name, int size, int cost, ImageSource image)
        {
            Name = name;
            Size = size;
            Cost = cost;
            Image = image;
        }
        public Fish Clone()
        {
            return new Fish(Name, Size, Cost, Image);
        }
    }
}
