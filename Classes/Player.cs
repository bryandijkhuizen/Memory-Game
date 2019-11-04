using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game.Classes
{
    class Player
    {
        //Benodigde variabelen
        private int score;
        private string naam;

        //Methode voor de aanmaak van een speler
        public Player(int score, string naam)
        {
            this.score = score;
            this.naam = naam;
        }

        //Aanmaak Score methode
        public int GetScore()
        {
            return score;
        }

        //Aanmaak Naam methode
        public string GetNaam()
        {
            return naam;
        }

        //Methode die punten toevoegd in de Multiplayer gamemode
        public void AddPoint()
        {
            score += 1;
        }

        //Methode voor het resetten van multiplayer
        public void ResetScores()
        {
            score = 0;
        }
    }
}
