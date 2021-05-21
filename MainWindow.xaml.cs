using Matches;
using People;
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

namespace Kopakabana
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Tournament Tour = new Tournament();
        public MainWindow()
        {
            InitializeComponent();
            Referee.Visibility = Visibility.Hidden;
            Team.Visibility = Visibility.Hidden;
            Match.Visibility = Visibility.Hidden;
            /*Lista.Items.Add(new Match("Real","Barca",2,0));
            Lista.Items.Add(new Match("Atletico","sevilla",0,0));
            T = new List<Team>{ new Team("Real"), new Team("Sevilla"), new Team("Barca"), new Team("Atletico") };
            Mat = new List<Match>();
            Ref = new List<Referee>();
            Mat.Add(new Match(T[0],T[1], 0, 1));
            Mat.Add(new Match(T[2], T[3], 2, 0));
            Ref.Add(new Referee("Przemek","Kuczynski"));*/
            DataContext = this;
        }

        private void Mecze_Click(object sender, RoutedEventArgs e)
        {
            // Lista.ItemsSource = Tour.getMatches();
            Lista.ItemsSource = null;
            string poptype = " ";
            foreach (Match M in Tour.getMatches())
            {
                if (M.GetType().Name != poptype)
                {
                    poptype = M.GetType().Name;
                    Lista.Items.Add(new ListBoxItem(){Content=poptype.ToUpper() ,Foreground = Brushes.Black});
                }
                poptype = M.GetType().Name;
                ListBoxItem Item = new ListBoxItem() { Content = M };
                Lista.SelectionChanged += new SelectionChangedEventHandler(SelectMatch);
                Lista.Items.Add(Item);
            }
            Referee.Visibility = Visibility.Hidden;
            Team.Visibility = Visibility.Hidden;
        }
        private void Sedzia(object sender, RoutedEventArgs e)
        {
            Lista.SelectedItem = false;
            try
            {
                Lista.Items.Clear();
            }
            catch { }
            //Lista.Visibility = Visibility.Visible;
            Lista.ItemsSource = Tour.getReferees();
            Referee.Visibility = Visibility.Visible;
            Team.Visibility = Visibility.Hidden;
        }

        private void Druzyny_Click(object sender, RoutedEventArgs e)
        {
            //Lista.Visibility = Visibility.Visible;
            Lista.SelectedItem = false;
            try
            {
                Lista.Items.Clear();
            }
            catch { }
            Lista.ItemsSource = Tour.getTeams();
            Referee.Visibility = Visibility.Hidden;
            Team.Visibility = Visibility.Visible;
        }

        private void RefAdd(object sender, RoutedEventArgs e)
        {

        }

        private void RefDelete(object sender, RoutedEventArgs e)
        {

        }

        private void RefEdit(object sender, RoutedEventArgs e)
        {

        }

        private void TAdd(object sender, RoutedEventArgs e)
        {

        }

        private void TDelete(object sender, RoutedEventArgs e)
        {

        }

        private void TEdit(object sender, RoutedEventArgs e)
        {

        }
        private void SelectMatch(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(Lista.SelectedItem==null)
                    throw new Exception();
                var I = (Match)((ListBoxItem)(Lista.SelectedItem)).Content;
                Main.Visibility = Visibility.Hidden;
                Match.Visibility = Visibility.Visible;
                Name1.Text = I.T1.getName();
                Name2.Text = I.T2.getName();
                if (I is VolleyBall)
                {
                    Res1.Text = ((VolleyBall)I).Result1.ToString();
                    Res2.Text = ((VolleyBall)I).Result1.ToString();
                    Type.Text = I.GetType().Name;
                }
                else
                {

                }
            }
            catch(Exception) { }
            /*ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
            Match I = (Match)(lbi.Content);*/
            // MessageBox.Show(I.T1.ToString(), "Error", MessageBoxButton.OK);
        }

        private void BackMain(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Visible;
            Match.Visibility = Visibility.Hidden;
            Lista.SelectedItem = false;
        }
        /* private void SelectMatch(object sender, RoutedEventArgs e)
{
    try
    {
        Match Mat = Lista.SelectedItem as Match;
        MessageBox.Show(Mat.T1.ToString(), "Error", MessageBoxButton.OK);
    }
    catch
    {
        MessageBox.Show("NULL", "NULL", MessageBoxButton.OK);
    }
    return;
}*/
    }
    /* public class Match
     {
         public string name1 { get; set; }
         public string name2 { get; set; }
         Team Team1;
         Team Team2;
         public int wynik1;
         public int wynik2;
         public string wynik;
         public Match(string n1, string n2, int w1, int w2)
         {
             name1 = n1;
             name2 = n2;
             wynik1 = w1;
             wynik2 = w2;
         }
         public Match(Team t1, Team t2, int w1, int w2)
         {
             this.Team1 = t1;
             this.Team2 = t2;
             wynik1 = w1;
             wynik2 = w2;
         }
         public override string ToString()
         {
             try
             {
                 name1 = Team1.name;
                 name2 = Team2.name;
                 string wynik = wynik1 + " : " + wynik2;
                 return name1+" - " + name2 + "\t\t\t" + wynik;
             }
             catch { return ""; }
         }
         /*public override string ToString()
         {

             string wynik =name1+" - "+name2+"\t\t\t\t\t"+ wynik1 + " : " + wynik2;
             return wynik;
         }
     }
     public class Referee
     {
         public string imie { get; set; }
         public string nazwisko { get; set; }
         public Referee(string i, string n)
         {
             imie = i;
             nazwisko = n;
         }
         public override string ToString()
         {

             string wynik = imie + "  " + nazwisko;
             return wynik;
         }
     }
     public class Team
     {
         public string name { get; set; }
         public Team(string n)
         {
             name = n;
         }
         public override string ToString()
         {
             return name;
         }
     }*/
}