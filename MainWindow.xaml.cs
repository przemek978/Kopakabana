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
        public List<Match> Mat { get; set; }
        public List<Referee> Ref { get; set; }
        public List<Team> T { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            /*Lista.Items.Add(new Match("Real","Barca",2,0));
            Lista.Items.Add(new Match("Atletico","sevilla",0,0));*/
            Mat = new List<Match>();
            Ref = new List<Referee>();
            T = new List<Team>{ new Team("Real"), new Team("Sevilla"), new Team("Barca"), new Team("Atletico") };
            Mat.Add(new Match(T[0],T[1], 0, 1));
            Mat.Add(new Match(T[2], T[3], 2, 0));
            Ref.Add(new Referee("Przemek","Kuczynski"));
            //Mat.Add(new Match("Real", "Barca", 2, 0));
            ///Mat.Add(new Match("Atletico", "sevilla", 0, 0));
            DataContext = this;
        }

        private void Mecze_Click(object sender, RoutedEventArgs e)
        {
            Lista.ItemsSource = Mat;
        }
        private void Sedzia(object sender, RoutedEventArgs e)
        {
            Lista.ItemsSource = Ref;
        }

        private void Druzyny_Click(object sender, RoutedEventArgs e)
        {
            Lista.ItemsSource = T;
        }

    }
    public class Match
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
        }*/
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
    }
}