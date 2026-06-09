using System;
using System.Collections.Generic;
using System.Text;

namespace Systeme_Puissance_4
{
    public class ParametresJeu
    {
        public static ParametresJeu Current { get; set; } = new ParametresJeu();
        public bool ContreRobot { get; set; } = false;
        public int DifficulteRobot { get; set; } = 0;  

        public int Colonnes { get; set; } = 7;
        public int Lignes { get; set; } = 6;
        public int JetonsPourGagner { get; set; } = 4;

        public bool ModeChallenge { get; set; } = false;
        public bool ActiverChrono { get; set; } = false;

        public int NiveauContraste { get; set; } = 0; 
        public string NomPolice { get; set; } = "Arial";
        public double TaillePolice { get; set; } = 16;

        public string CouleurJ1 { get; set; } = "#FF0000";
        public string CouleurJ2 { get; set; } = "#FFFF00";
    }
}
