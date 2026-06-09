using System;
using System.Collections.Generic;
using System.Text;

namespace Systeme_Puissance_4
{
    public class MoteurJeu
    {
        public ParametresJeu Parametres { get; set; }

        public MoteurJeu(ParametresJeu parametres)
        {
            Parametres = parametres;

            // C'est ici que tu devras initialiser ton tableau 2D (la grille mathématique)
            // ex: _grille = new int[parametres.Lignes, parametres.Colonnes];
        }

        // Méthode appelée par l'interface quand on clique sur une colonne
        public int PlacerJeton(int colonne)
        {
            // TO DO : Coder la vraie logique mathématique ici (SAE 2.01).
            // Tu devras parcourir ton tableau 2D de bas en haut pour trouver la première case vide.

            // Pour l'instant, on triche pour que ton interface compile et affiche un jeton tout en bas :
            return Parametres.Lignes - 1;
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
            return "J1"; // TO DO : Alterner entre J1 et J2/IA
        }

        // Fournit la couleur du joueur à l'interface pour colorier le rond
        public string ObtenirCouleurJoueurActuel()
        {
            return Parametres.CouleurJ1; // TO DO : Alterner la couleur selon le tour
        }
    }
}