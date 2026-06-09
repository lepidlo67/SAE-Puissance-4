using System;
using System.Collections.Generic;
using System.Text;

namespace Systeme_Puissance_4
{
    public class MoteurJeu
    {
        public ParametresJeu Parametres { get; set; }
        private string NomJoueur { get; set; } = "J1";
        public int[,] Plateau { get; set; }

        public MoteurJeu(ParametresJeu parametres)
        {
            Parametres = parametres;
            Plateau = new int[Parametres.Lignes, Parametres.Colonnes];

            for (int i = 0; i < Parametres.Lignes; i++)
            {
                for (int j = 0; j < Parametres.Colonnes; j++)
                {
                    Plateau[i, j] = -1;
                }
            }
        }

        public int VerifierPlacement(int colonne)
        {
            for (int i = Parametres.Lignes - 1; i >= 0; i--)
            {
                if (Plateau[i, colonne] == -1)
                {
                    AjouterPion(i, colonne);
                    return i;
                }
            }

            return -1;
        }

        public void AjouterPion(int ligne, int colonne)
        {
            Plateau[ligne, colonne] = NomJoueur == "J1" ? 1 : 2;
        }

        // Méthode appelée par l'interface pour savoir si la partie est finie
        public bool VerifierVictoire()
        {
            // TO DO : Coder la vérification des alignements (horizontal, vertical, diagonal).

            return false; // On renvoie faux pour l'instant
        }

        // Fournit le nom du joueur à l'interface pour l'affichage
        public string ObtenirNomJoueurActuel()
        {
            return NomJoueur; // TO DO : Alterner entre J1 et J2/IA
        }

        public void AlternerJoueurs()
        {
            NomJoueur = NomJoueur == "J1" ? Parametres.ContreRobot ? "IA" : "J2" : "J1";
        }

        // Fournit la couleur du joueur à l'interface pour colorier le rond
        public string ObtenirCouleurJoueurActuel()
        {
            return NomJoueur == "J1" ? Parametres.CouleurJ1 : Parametres.CouleurJ2; // TO DO : Alterner la couleur selon le tour
        }
    }
}