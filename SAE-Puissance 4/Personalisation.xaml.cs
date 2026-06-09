using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SAE_Puissance_4
{
    /// <summary>
    /// Logique d'interaction pour Personalisation.xaml
    /// </summary>
    public partial class Personalisation : Window
    {
        public Personalisation()
        {
            InitializeComponent();
        }

        // --- GESTION DU CONTRASTE ---
        private void SliderContraste_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Sécurité pour éviter une erreur lors du chargement initial de la fenêtre
            if (!this.IsLoaded) return;

            // 0 = Bas, 1 = Moyen, 2 = Elevé
            if (e.NewValue == 2)
            {
                this.Background = Brushes.LightGray; // Exemple visuel pour le contraste élevé
            }
            else
            {
                this.Background = Brushes.White;
            }
        }

        // --- GESTION DE LA POLICE ---
        private void ComboPolice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LabelExemplePolice != null && ComboPolice.SelectedItem != null)
            {
                ComboBoxItem item = (ComboBoxItem)ComboPolice.SelectedItem;
                LabelExemplePolice.FontFamily = new FontFamily(item.Content.ToString());
            }
        }

        private void SliderTaillePolice_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (LabelExemplePolice != null)
            {
                LabelExemplePolice.FontSize = e.NewValue;
            }
        }

        // --- GESTION DES COULEURS DES JETONS ---
        private void Color_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Brush couleurSelectionnee = btn.Background;

            // Applique la couleur au joueur sélectionné
            if (RadioJ1.IsChecked == true)
            {
                PreviewJ1.Fill = couleurSelectionnee;
            }
            else if (RadioJ2.IsChecked == true)
            {
                PreviewJ2.Fill = couleurSelectionnee;
            }
        }

        // --- BOUTONS D'ACTION ---
        private void BtnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            // Logique de sauvegarde des choix à implémenter ici plus tard
            this.Close();
        }

        private void BtnReinitialiser_Click(object sender, RoutedEventArgs e)
        {
            // Remise des valeurs par défaut
            SliderContraste.Value = 0;
            ComboPolice.SelectedIndex = 0;
            SliderTaillePolice.Value = 16;

            PreviewJ1.Fill = Brushes.Red;
            PreviewJ2.Fill = Brushes.Yellow;

            RadioJ1.IsChecked = true;
            this.Background = Brushes.White;
        }
    }
}