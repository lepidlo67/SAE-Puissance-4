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
            // 1. On crée l'objet contenant les paramètres par défaut du système
            Systeme_Puissance_4.ParametresJeu paramsParDefaut = new Systeme_Puissance_4.ParametresJeu();

            // 2. On donne cet objet à la fenêtre de jeu lors de sa création
            Jeu fenetre = new Jeu(paramsParDefaut);

            // 3. On affiche le jeu et on ferme le menu
            fenetre.Show();
            this.Close();
        }
    }
}