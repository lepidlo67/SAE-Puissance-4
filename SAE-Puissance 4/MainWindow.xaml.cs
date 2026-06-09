using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Systeme_Puissance_4;

namespace SAE_Puissance_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppliquerPersonnalisation();
        }
        private void AppliquerPersonnalisation()
        {
            this.FontFamily = new FontFamily(ParametresJeu.Current.NomPolice);

            if (ParametresJeu.Current.NiveauContraste == 2)
            {
                this.Background = Brushes.Black;
                // Comme tes boutons n'ont pas de style complexe, tu pourrais aussi les colorier en noir/blanc ici
            }
            else if (ParametresJeu.Current.NiveauContraste == 1)
            {
                this.Background = Brushes.LightGray;
            }
            else
            {
                this.Background = Brushes.White;
            }
        }
        private void btn_parametres_Click(object sender, RoutedEventArgs e)
        {
            Parametres fenetre = new Parametres(); 
            fenetre.ShowDialog(); 
        }

        private void btn_personalisation_Click(object sender, RoutedEventArgs e)
        {
            Personalisation fenetre = new Personalisation();
            fenetre.ShowDialog();
        }

        private void btn_Jouer_Click(object sender, RoutedEventArgs e)
        {
            Jeu fenetre = new Jeu(ParametresJeu.Current);

            fenetre.Show();
            this.Close();
        }
    }
}