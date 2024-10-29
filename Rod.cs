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
        public int Cost {  get; init; }
        public int WeightCapacity { get; set; }
        public ImageSource Image { get; set; }

        public Rod Clone()
        {
            return new Rod(this.Name, this.Cost, this.WeightCapacity, this.Image);
        }
    }
}
