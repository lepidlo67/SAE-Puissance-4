using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

// N'oublie pas cette ligne pour utiliser ton projet Système !
using Systeme_Puissance_4;

namespace SAE_Puissance_4
{
    public partial class Jeu : Window
    {
        private MoteurJeu _moteur;
        private DispatcherTimer _timer;

        public Jeu(ParametresJeu parametres)
        {
            InitializeComponent();
            _moteur = new MoteurJeu(parametres);

            InitialiserChrono();

            AppliquerPersonnalisation();

            ConfigurerAffichageInitial();
            GenererGrilleVisuelle();
        }

        private void AppliquerPersonnalisation()
        {
            this.FontFamily = new FontFamily(_moteur.Parametres.NomPolice);

            this.FontSize = _moteur.Parametres.TaillePolice;

            if (_moteur.Parametres.NiveauContraste == 2)
            {
                this.Background = Brushes.Black;
                this.Foreground = Brushes.White;
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

            BrushConverter convertisseur = new BrushConverter();

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

            if (_moteur.Parametres.ActiverChrono)
            {
                PanelChronoJ1.Visibility = Visibility.Visible;
                PanelChronoJ2.Visibility = Visibility.Hidden;
            }
        }

        private void InitialiserChrono()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            string joueurActif = _moteur.ObtenirNomJoueurActuel();

            int tempsRestant = _moteur.RetirerTemps(joueurActif == "J1" ? 1 : 2, 1);

            MettreAJourAffichageChrono(joueurActif);

            if (tempsRestant <= 0)
            {
                _timer.Stop();

                string gagnant = (joueurActif == "J1" ? _moteur.Parametres.ContreRobot ? "IA" : "J2" : "J1");

                MessageBox.Show($"Temps écoulé pour le Joueur {joueurActif} ! {gagnant} remporte la partie !");
            }
        }

        private void MettreAJourAffichageChrono(string joueurActif)
        {
            if (joueurActif == "J1")
            {
                PanelChronoJ1.Visibility = Visibility.Visible;
                PanelChronoJ2.Visibility = Visibility.Hidden;
            }
            else
            {
                PanelChronoJ1.Visibility = Visibility.Hidden;
                PanelChronoJ2.Visibility = Visibility.Visible;
            }

            TxtChronoJ1.Text = $"{_moteur.TempsJ1}sec";
            TxtChronoJ2.Text = $"{_moteur.TempsJ2}sec";
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
                    Tag = i
                };

                caseVide.MouseLeftButtonDown += Case_Click;
                GrilleJeu.Children.Add(caseVide);
            }
        }

        private void Case_Click(object sender, MouseButtonEventArgs e)
        {
            Ellipse caseCliquee = (Ellipse)sender;
            int indexCase = (int)caseCliquee.Tag;

            int colonne = indexCase % _moteur.Parametres.Colonnes;

            int ligneOuTombeLeJeton = _moteur.VerifierPlacement(colonne);

            if (ligneOuTombeLeJeton != -1)
            {
                MettreAJourJetonGraphique(colonne, ligneOuTombeLeJeton);

                if (_moteur.VerifierVictoire())
                {
                    TxtTour.Text = $"Victoire de {_moteur.ObtenirNomGagnant()}!";
                    MessageBox.Show("Partie terminée !");

                    if (_moteur.Parametres.ModeChallenge)
                    {
                        Jeu nouv = new(_moteur.Parametres);
                        MessageBox.Show("Une autre partie avec les mêmes paramètres va être lancée grâce au mode challenge", "Mode challenge", MessageBoxButton.OK, MessageBoxImage.Information);
                        nouv.Show();
                        this.Close();
                    }
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
            QuitterJeu();
        }

        private void QuitterJeu()
        {
            MessageBox.Show("Partie enregistrée !");

            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close();
        }
    }
}