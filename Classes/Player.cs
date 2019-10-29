using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game.Classes
{
    class Player
    {
        private int score;
        private string naam;
        public Player(int score, string naam)
        {
            this.score = score;
            this.naam = naam;
        }

        public int GetScore()
        {
            return score;
        }

        public string GetNaam()
        {
            return naam;
        }

        public void AddPoint()
        {
            score += 1;
        }
    }
}
