using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;

namespace Lab4
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
            return new Fish(this.Name, this.Size, this.Cost, this.Image);
        }
    }
}
