using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TeamProjectChess.Model
{
   public class PlayerCard
    {

        public int ID { get; set; }
        public int Rank { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public int BirthYear { get; set; }
        public string  Country { get; set; }
        public int ClassicRateValue { get; set; }
        public int BlitzRateValue { get; set; }
        public int RapidRateValue { get; set; }
        public PlayerCard(int r, string n, string imn)
        {
            ImageName = imn;
            Rank = r;
            Name = n;
        }

    }
}
