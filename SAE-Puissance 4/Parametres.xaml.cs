using System;
using System.Windows;
using System.Windows.Media;

using Systeme_Puissance_4;

namespace SAE_Puissance_4
{
    /// <summary>
    /// Logique d'interaction pour Parametres.xaml
    /// </summary>
    public partial class Parametres : Window
    {
        public ParametresJeu MesParametres { get; set; }
        public Parametres()
        {
            InitializeComponent();

            // MODIFICATION : Au chargement, on affiche les vraies valeurs globales actuelles
            TxtColonnes.Text = ParametresJeu.Current.Colonnes.ToString();
            TxtLignes.Text = ParametresJeu.Current.Lignes.ToString();
            TxtJetons.Text = ParametresJeu.Current.JetonsPourGagner.ToString();
            TglChallenge.IsChecked = ParametresJeu.Current.ModeChallenge;
            TglChrono.IsChecked = ParametresJeu.Current.ActiverChrono;
            SliderDifficulte.Value = ParametresJeu.Current.DifficulteRobot;

            if (ParametresJeu.Current.ContreRobot)
                BtnVsRobot_Click(null, null);
            else
                BtnVsAmi_Click(null, null);
        }


        private void BtnVsAmi_Click(object sender, RoutedEventArgs e)
        {
            BtnVsAmi.Background = Brushes.Black;
            BtnVsAmi.Foreground = Brushes.White;

            BtnVsRobot.Background = Brushes.White;
            BtnVsRobot.Foreground = Brushes.Black;

            PanelDifficulte.Visibility = Visibility.Collapsed;
        }

        private void BtnVsRobot_Click(object sender, RoutedEventArgs e)
        {
            BtnVsRobot.Background = Brushes.Black;
            BtnVsRobot.Foreground = Brushes.White;

            BtnVsAmi.Background = Brushes.White;
            BtnVsAmi.Foreground = Brushes.Black;

            PanelDifficulte.Visibility = Visibility.Visible;
        }


        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            PopupInfo.Visibility = Visibility.Visible;
        }

        private void BtnCloseInfo_Click(object sender, RoutedEventArgs e)
        {
            PopupInfo.Visibility = Visibility.Collapsed;
        }


        private void BtnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            //On enregistre directement les choix dans l'instance globale
            ParametresJeu.Current.Colonnes = int.Parse(TxtColonnes.Text);
            ParametresJeu.Current.Lignes = int.Parse(TxtLignes.Text);
            ParametresJeu.Current.JetonsPourGagner = int.Parse(TxtJetons.Text);
            ParametresJeu.Current.ModeChallenge = TglChallenge.IsChecked == true;
            ParametresJeu.Current.ActiverChrono = TglChrono.IsChecked == true;
            ParametresJeu.Current.DifficulteRobot = (int)SliderDifficulte.Value;
            ParametresJeu.Current.ContreRobot = (BtnVsRobot.Background == Brushes.Black);
            this.Close();
        }

        private void BtnReinitialiser_Click(object sender, RoutedEventArgs e)
        {
            TxtColonnes.Text = "7";
            TxtLignes.Text = "6";
            TxtJetons.Text = "4";

            TglChallenge.IsChecked = false;
            TglChrono.IsChecked = false;

            SliderDifficulte.Value = 0; 
            PopupInfo.Visibility = Visibility.Collapsed;

            BtnVsAmi_Click(null, null);
        }

    }
}