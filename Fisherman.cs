using System.Windows.Media;

namespace Lab4
{
    public class Fisherman
    {
        private static Fisherman _instance;
        public event EventHandler BaitChanged;
        public event EventHandler RodChanged;

        private Bait _bait;
        private Rod _rod;
        public ImageSource Image { get; set; }

        private Fisherman(Bait bait, Rod rod, ImageSource image)
        {
            this._bait = bait;
            this._rod = rod;
            this.Image = image;
        }

        public static Fisherman GetInstance(Bait bait, Rod rod, ImageSource image)
        {
            if (_instance == null)
            {
                _instance = new Fisherman(bait, rod, image);
            }
            return _instance;
        }

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