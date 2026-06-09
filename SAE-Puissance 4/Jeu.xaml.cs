using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
// N'oublie pas cette ligne pour utiliser ton projet Système !
using Systeme_Puissance_4;

namespace SAE_Puissance_4
{
    public partial class Jeu : Window
    {
        // La référence vers le cerveau de ton jeu
        private MoteurJeu _moteur;

        // Le constructeur reçoit les paramètres depuis tes fenêtres précédentes
        public Jeu(ParametresJeu parametres)
        {
            InitializeComponent();

            // On lance le moteur avec les paramètres
            _moteur = new MoteurJeu(parametres);

            // On configure l'interface (noms, scores, etc.)
            ConfigurerAffichageInitial();

            // On dessine la grille
            GenererGrilleVisuelle();
        }

        private void ConfigurerAffichageInitial()
        {
            // Adapte l'affichage si on joue contre l'IA
            if (_moteur.Parametres.ContreRobot)
            {
                TxtNomJ2.Text = "IA";
                IconeJ2.Text = "🤖";
                JetonJ2_Cercle.Visibility = Visibility.Collapsed;
                JetonJ2_Carre.Visibility = Visibility.Visible;
            }
            else
            {
                TxtNomJ2.Text = "J2";
                IconeJ2.Text = "👤";
                JetonJ2_Cercle.Visibility = Visibility.Visible;
                JetonJ2_Carre.Visibility = Visibility.Collapsed;
            }

            // Adapte l'affichage si le mode Challenge est activé
            if (_moteur.Parametres.ModeChallenge)
            {
                TxtScoreJ1.Visibility = Visibility.Visible;
                TxtScoreJ2.Visibility = Visibility.Visible;
                TxtScoreJ1.Text = "0";
                TxtScoreJ2.Text = "0";
            }

            // Adapte l'affichage si le chrono est activé
            if (_moteur.Parametres.ActiverChrono)
            {
                PanelChronoJ1.Visibility = Visibility.Visible;
                PanelChronoJ2.Visibility = Visibility.Hidden;
            }
        }

        private void GenererGrilleVisuelle()
        {
            GrilleJeu.Columns = _moteur.Parametres.Colonnes;
            GrilleJeu.Rows = _moteur.Parametres.Lignes;

            int totalCases = _moteur.Parametres.Colonnes * _moteur.Parametres.Lignes;

            for (int i = 0; i < totalCases; i++)
            {
                Ellipse caseVide = new Ellipse
                {
                    Width = 40,
                    Height = 40,
                    Fill = Brushes.White,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    Margin = new Thickness(5),
                    Tag = i // On mémorise le numéro de la case
                };

                caseVide.MouseLeftButtonDown += Case_Click;
                GrilleJeu.Children.Add(caseVide);
            }
        }

        private void Case_Click(object sender, MouseButtonEventArgs e)
        {
            Ellipse caseCliquee = (Ellipse)sender;
            int indexCase = (int)caseCliquee.Tag;

            // On calcule la colonne cliquée
            int colonne = indexCase % _moteur.Parametres.Colonnes;

            // On demande au système de placer le jeton
            int ligneOuTombeLeJeton = _moteur.PlacerJeton(colonne);

            // Si la ligne n'est pas -1, c'est que le coup est valide
            if (ligneOuTombeLeJeton != -1)
            {
                // On met à jour l'interface graphique
                MettreAJourJetonGraphique(colonne, ligneOuTombeLeJeton);

                // On demande au système si quelqu'un a gagné
                if (_moteur.VerifierVictoire())
                {
                    TxtTour.Text = "Victoire !";
                    MessageBox.Show("Partie terminée !");
                }
                else
                {
                    // Sinon, on met à jour le texte pour le joueur suivant
                    TxtTour.Text = "Au tour de " + _moteur.ObtenirNomJoueurActuel() + " de jouer :";
                }
            }
            else
            {
                MessageBox.Show("Cette colonne est pleine !");
            }
        }

        private void MettreAJourJetonGraphique(int col, int lig)
        {
            int indexVisuel = lig * _moteur.Parametres.Colonnes + col;
            Ellipse ellipse = (Ellipse)GrilleJeu.Children[indexVisuel];

            string codeCouleur = _moteur.ObtenirCouleurJoueurActuel();
            ellipse.Fill = (Brush)new BrushConverter().ConvertFromString(codeCouleur);
        }

        private void BtnEnregistrerQuitter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Partie enregistrée !");

            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close();
        }
    }
}