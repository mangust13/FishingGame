using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace Lab4
{
    public class Bait
    {
        public Bait(string name, int cost, int chance, ImageSource image)
        {
            Name = name;
            Cost = cost;
            Chance = chance;
            Image = image;
        }

        public string Name { get; init; }
        public int Cost { get; init; }
        public int Chance {get; set;}
        public ImageSource Image {get; set;}

        public Bait Clone()
        {
            return new Bait(this.Name, this.Cost, this.Chance, this.Image);
        }
    }
}