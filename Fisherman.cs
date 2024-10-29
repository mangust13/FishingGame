using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab4
{
    public class Fisherman
    {
        public event EventHandler BaitChanged;
        public event EventHandler RodChanged;
        private Bait _bait;
        private Rod _rod;

        public Fisherman(
            Bait bait,
            Rod rod,
            ImageSource image)
        {
            this.bait = bait;
            this.rod = rod;
            Image = image;
        }
        public ImageSource Image { get; set; }
        public Bait bait {
            get { return _bait; } 
            set { 
                _bait = value;
                BaitChanged?.Invoke(this, EventArgs.Empty);
            } 
        }
        public Rod rod {
            get { return _rod; }
            set {
                _rod = value;
                RodChanged?.Invoke(this, EventArgs.Empty);}
            }
    }

}
