using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Systeme_Puissance_4; // Permet d'appeler ton projet Système

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

            SliderContraste.Value = ParametresJeu.Current.NiveauContraste;

            SliderTaillePolice.Value = ParametresJeu.Current.TaillePolice;

            foreach (ComboBoxItem item in ComboPolice.Items)
            {
                if (item.Content.ToString() == ParametresJeu.Current.NomPolice)
                {
                    item.IsSelected = true;
                    break;
                }
            }

            BrushConverter convertisseur = new BrushConverter();
            PreviewJ1.Fill = (Brush)convertisseur.ConvertFromString(ParametresJeu.Current.CouleurJ1);
            PreviewJ2.Fill = (Brush)convertisseur.ConvertFromString(ParametresJeu.Current.CouleurJ2);
        }

        private void SliderContraste_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!this.IsLoaded) return;

            if (e.NewValue == 2)
            {
                this.Background = Brushes.LightGray;
            }
            else
            {
                this.Background = Brushes.White;
            }
        }

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

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Brush couleurSelectionnee = btn.Background;

            if (RadioJ1.IsChecked == true && PreviewJ2.Fill != couleurSelectionnee)
            {
                PreviewJ1.Fill = couleurSelectionnee;
            }
            else if (RadioJ2.IsChecked == true)
            {
                if (PreviewJ1.Fill != couleurSelectionnee)
                    PreviewJ2.Fill = couleurSelectionnee;
            }
        }

        private void BtnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            ParametresJeu.Current.NiveauContraste = (int)SliderContraste.Value;
            ParametresJeu.Current.TaillePolice = SliderTaillePolice.Value;

            if (ComboPolice.SelectedItem != null)
            {
                ParametresJeu.Current.NomPolice = ((ComboBoxItem)ComboPolice.SelectedItem).Content.ToString();
            }

            ParametresJeu.Current.CouleurJ1 = PreviewJ1.Fill.ToString();
            ParametresJeu.Current.CouleurJ2 = PreviewJ2.Fill.ToString();

            this.Close();
        }

        private void BtnReinitialiser_Click(object sender, RoutedEventArgs e)
        {
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