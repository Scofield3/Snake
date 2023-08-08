using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class Score
    {
        public string Name;
        public int Points;

        public Score (string name, int score)
        {
            this.Name = name;
            this.Points = score;
        }
    }
}
