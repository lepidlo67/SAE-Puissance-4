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
            _moteur = new MoteurJeu(parametres);

            // 1. On applique le design global
            AppliquerPersonnalisation();

            ConfigurerAffichageInitial();
            GenererGrilleVisuelle();
        }

        // Nouvelle méthode à copier-coller
        private void AppliquerPersonnalisation()
        {
            // --- 1. POLICE ---
            // Applique la police choisie à toute la fenêtre
            this.FontFamily = new FontFamily(_moteur.Parametres.NomPolice);

            // (Optionnel) Appliquer la taille de base. 
            // Attention : ça ne modifiera pas les TextBlock où tu as déjà écrit FontSize="24" en dur dans le XAML.
            this.FontSize = _moteur.Parametres.TaillePolice;

            // --- 2. CONTRASTE ---
            // 0 = Bas/Normal, 1 = Moyen, 2 = Elevé
            if (_moteur.Parametres.NiveauContraste == 2)
            {
                this.Background = Brushes.Black;
                this.Foreground = Brushes.White; // Le texte par défaut devient blanc
            }
            else if (_moteur.Parametres.NiveauContraste == 1)
            {
                this.Background = Brushes.LightGray;
                this.Foreground = Brushes.Black;
            }
            else
            {
                this.Background = Brushes.White;
                this.Foreground = Brushes.Black;
            }

            // --- 3. COULEURS DES JETONS DANS L'EN-TÊTE ---
            // Pour que les ronds J1 et J2 en haut de l'écran aient la bonne couleur
            BrushConverter convertisseur = new BrushConverter();

            // Note : Il faut ajouter un x:Name="JetonJ1" à l'ellipse du joueur 1 dans ton Jeu.xaml pour que ça marche
            JetonJ1.Fill = (Brush)convertisseur.ConvertFromString(_moteur.Parametres.CouleurJ1);

            if (JetonJ2_Cercle != null)
                JetonJ2_Cercle.Fill = (Brush)convertisseur.ConvertFromString(_moteur.Parametres.CouleurJ2);

            if (JetonJ2_Carre != null)
                JetonJ2_Carre.Background = (Brush)convertisseur.ConvertFromString(_moteur.Parametres.CouleurJ2);
        }

        private void ConfigurerAffichageInitial()
        {
            TxtTour.Text = "Au tour de " + _moteur.ObtenirNomJoueurActuel() + " de jouer :";

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
            int ligneOuTombeLeJeton = _moteur.VerifierPlacement(colonne);

            // Si la ligne n'est pas -1, c'est que le coup est valide
            if (ligneOuTombeLeJeton != -1)
            {
                MettreAJourJetonGraphique(colonne, ligneOuTombeLeJeton);

                if (_moteur.VerifierVictoire())
                {
                    TxtTour.Text = $"Victoire de {_moteur.ObtenirNomJoueurActuel()}!";
                    MessageBox.Show("Partie terminée !");
                }
                else
                {
                    _moteur.AlternerJoueurs();

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