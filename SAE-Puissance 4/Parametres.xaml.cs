using System;
using System.Windows;
using System.Windows.Media;

namespace SAE_Puissance_4
{
    /// <summary>
    /// Logique d'interaction pour Parametres.xaml
    /// </summary>
    public partial class Parametres : Window
    {
        public Parametres()
        {
            InitializeComponent();
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