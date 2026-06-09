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
        public string NomGagnant { get; set; }

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
            int n = Parametres.JetonsPourGagner;

            int[][] directions = new int[][]
            {
                new int[] { 0, 1 },
                new int[] { 1, 0 },
                new int[] { 1, 1 },
                new int[] { -1, 1 }
            };

            for (int i = 0; i < Parametres.Lignes; i++)
            {
                for (int j = 0; j < Parametres.Colonnes; j++)
                {
                    int joueurCherche = Plateau[i, j];

                    if (joueurCherche == -1)
                        continue;

                    foreach (var dir in directions)
                    {
                        int dLigne = dir[0];
                        int dColonne = dir[1];

                        int derniereLigne = i + (n - 1) * dLigne;
                        int derniereColonne = j + (n - 1) * dColonne;

                        if (derniereLigne >= 0 && derniereLigne < Parametres.Lignes &&
                            derniereColonne >= 0 && derniereColonne < Parametres.Colonnes)
                        {
                            bool alignementComplet = true;

                            for (int k = 1; k < n; k++)
                            {
                                if (Plateau[i + k * dLigne, j + k * dColonne] != joueurCherche)
                                {
                                    alignementComplet = false;
                                    break;
                                }
                            }

                            if (alignementComplet)
                            {
                                NomGagnant = Plateau[i, j] == 1 ? "J1" : Parametres.ContreRobot ? "IA" : "J2";
                                return true;
                            }
                        }
                    }
                }

            }
            return false;
        }

        // Fournit le nom du joueur à l'interface pour l'affichage
        public string ObtenirNomJoueurActuel()
        {
            return NomJoueur;
        }

        public string? ObtenirNomGagnant()
        {
            if (VerifierVictoire())
                return NomGagnant;

            return null;
        }

        public void AlternerJoueurs()
        {
            NomJoueur = NomJoueur == "J1" ? Parametres.ContreRobot ? "IA" : "J2" : "J1";
        }

        public string ObtenirCouleurJoueurActuel()
        {
            return NomJoueur == "J1" ? Parametres.CouleurJ1 : Parametres.CouleurJ2;
        }
    }
}