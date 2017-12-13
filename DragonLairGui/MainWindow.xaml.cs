using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DragonsLair;
using System.Collections.ObjectModel;

namespace DragonLairGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Controller c;
        TournamentRepo tp;
        public MainWindow()
        {
            c = new Controller();
            tp = new TournamentRepo();
            InitializeComponent();
          

        }
        private void Window_Loaded (object sender, RoutedEventArgs e)
        {
            ObservableCollection<Tournament> list = new ObservableCollection<Tournament>();
            foreach (Tournament tournament in tp.tournaments)
            {
                list.Add(tournament);
                this.CB1.ItemsSource = list;
            }
           
        }

        
        private void ShowScore_Button_Click(object sender, RoutedEventArgs e)
        {
            string tournamentName = TournamentName_Textbox.Text;

            ShowStuff_Textbox.Text = c.ShowScore(tournamentName);
         
            foreach (Tournament tournament in tp.tournaments)
            {
                CB1.Items.Add(tournament.Name);

            }
            









        }
     

      

        public void TournamentName_Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void ShowStuff_Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Tournament_Add_Click(object sender, RoutedEventArgs e)
        {
            string name = TournamentName_Textbox.Text;
            tp.RegisterTournament(name);
        }

        private void Show_Round_Click(object sender, RoutedEventArgs e)
        {
            string name = TournamentName_Textbox.Text;
            c.ShowRound(name,)
        }
    }
}