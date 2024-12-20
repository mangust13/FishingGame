﻿using System.Windows.Media;

namespace FishingGame.Model
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
        public int Chance { get; set; }
        public ImageSource Image { get; set; }

        public Bait Clone()
        {
            return new Bait(Name, Cost, Chance, Image);
        }
    }
}