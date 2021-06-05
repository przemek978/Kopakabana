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
            ///Czytanie danych, genreowanie meczy wczytywanie danych wynikow do wygenrewanych meczow i na tej podstawie zlicznaie wygranych
            Tour.Read();
            //Tour.GetTop4();
            Tour.GenerateMatches();
            Tour.CountWins();
            Tour.GetTop4();
            Tour.GenerateMatches();
            DataContext = this;
        }
        ////AKTUALZACJA WIDOKU DANYCH//////////////////////////////////////////////////////////////////////
        public void Refresh()
        {
            /*
             * Metoda ktora odswieza liste
             * Generuje ponownie mecze gdy zostana dodane lub usuniete druzyny 
             * Na podstawie ponownie wygenerwoanych meczow przypsiuje wyniki
             * zlicza wygrane
             */
            /*
             * Metoda wywolywana zazzwyczaj gdy pojawily sie zmiany w sedziach lub druzynach
             */
            Lista.Items.Refresh();//Lista
            Tour.GenerateMatches();//Ponowne generowanie meczow
            Tour.Save();//zapis druzyn i sedziow
            Tour.CountWins();//ponowne oblicznie wygranych
            //Tour.GetTop4();//Ponowne generowanie 4 najlepszych druzyn
        }
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ////WYBOR GRUP///////////////////////////////////////////////////////////////////////////////
        private void Mecze_Click(object sender, RoutedEventArgs e)///Ready
        {
            Tour.GetTop4();
            Lista.Items.Clear();
            SetScore.Visibility = Visibility.Visible;
            string poptype = " ";
            foreach (Match M in Tour.GetMatches())
            {
                if (M.GetType().Name != poptype)
                {
                    if (M is VolleyBall && ((VolleyBall)M).WhatSemi == true && poptype != "Semifinal")
                    {
                        poptype = "Semifinal";
                        Lista.Items.Add(new ListBoxItem() { Content = "Semifinal".ToUpper(), Foreground = Brushes.White });
                    }
                    else if (M is VolleyBall && M.WhatFinal == true)
                    {
                        poptype = M.GetType().Name;
                        Lista.Items.Add(new ListBoxItem() { Content = "Final".ToUpper(), Foreground = Brushes.White });
                    }
                    else if (M.WhatSemi == false && M.WhatFinal == false)
                    {
                        poptype = M.GetType().Name;
                        Lista.Items.Add(new ListBoxItem() { Content = poptype.ToUpper(), Foreground = Brushes.White });
                    }
                }
                //poptype = M.GetType().Name;
                ListBoxItem Item = new ListBoxItem() { Content = M };
                // Lista.SelectionChanged += new SelectionChangedEventHandler(SelectMatch);
                Lista.Items.Add(Item);
            }

            Referee.Visibility = Visibility.Hidden;
            Team.Visibility = Visibility.Hidden;
        }///Ready
        private void Sedzia(object sender, RoutedEventArgs e)
        {
            Lista.Items.Clear();
            SetScore.Visibility = Visibility.Hidden;
            foreach (Referee Ref in Tour.GetReferees())
            {
                ListBoxItem Item = new ListBoxItem() { Content = Ref };
                Lista.Items.Add(Item);
            }

            Referee.Visibility = Visibility.Visible;
            Team.Visibility = Visibility.Hidden;
        }///Ready
        private void Druzyny_Click(object sender, RoutedEventArgs e)
        {
            Lista.Items.Clear();
            Tour.GetTop4();
            SetScore.Visibility = Visibility.Hidden;
            foreach (Team T in Tour.GetTeams())
            {
                ListBoxItem Item = new ListBoxItem() { Content = T };
                Lista.Items.Add(Item);
            }
            Referee.Visibility = Visibility.Hidden;
            Team.Visibility = Visibility.Visible;
        }///Ready
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ////ZARZĄDZANIE SĘDZIAMI////////////////////////////////////////////////////////////////////
        private void RefAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                Manage Man = new Manage();
                //List<Referee> RE = Tour.GetReferees();
                Man.Width = 252;
                Man.Height = 150;
                Man.AddRef.Visibility = Visibility.Visible;
                if (true == Man.ShowDialog())
                {
                    Tour.CheckRef(Man.NameRef.Text, Man.SurnameRef.Text);
                    Tour.AddReferee(new Referee(Man.NameRef.Text, Man.SurnameRef.Text));
                    /*RE.Add(new Referee(Man.NameRef.Text, Man.SurnameRef.Text));
                    Tour.SetReferees(RE);*/
                    Refresh();
                    Sedzia(sender, e);
                }
            }
            catch (ExistNameException ex)
            {
                MessageBox.Show(ex.GetName() + " " + ex.GetSurname() + ex.Message, "Error", MessageBoxButton.OK);
            }

        }
        private void RefDelete(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Lista.SelectedItem == null)
                    throw new NotSelectedException("Nie wybrano sędziego do usunięcia");
                var I = Tour.GetReferees();
                I.RemoveAt(Lista.SelectedIndex);
                Tour.SetReferees(I);
                Refresh();
                Sedzia(sender, e);
            }
            catch (NotSelectedException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }
        private void RefEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Lista.SelectedItem == null)
                    throw new NotSelectedException("Nie wybrano sędziego do edycji");
                Manage Man = new Manage();
                string popname, popsur;
                Man.Width = 252;
                Man.Height = 150;
                Man.AddRef.Visibility = Visibility.Visible;
                ///////////////////////////////////////////////////////////////////////////////////////////////
                popname = Man.NameRef.Text = ((Referee)((ListBoxItem)Lista.SelectedItem).Content).GetName();
                popsur = Man.NameRef.Text = ((Referee)((ListBoxItem)Lista.SelectedItem).Content).GetSurname();
                Man.NameRef.Text = ((Referee)((ListBoxItem)Lista.SelectedItem).Content).GetName();
                Man.SurnameRef.Text = ((Referee)((ListBoxItem)Lista.SelectedItem).Content).GetSurname();
                ///////////////////////////////////////////////////////////////////////////////////////////////
                if (true == Man.ShowDialog())
                {
                    //Edycja sedziego wraz z aktualzacja w liscie meczow
                    Tour.CheckRef(Man.NameRef.Text, Man.SurnameRef.Text);
                    Tour.SetReferee(Lista.SelectedIndex, Man.NameRef.Text, Man.SurnameRef.Text);
                    Refresh();
                    Sedzia(sender, e);
                }
            }
            catch (ExistNameException ex)
            {
                MessageBox.Show(ex.GetName() + " " + ex.GetSurname() + ex.Message, "Error", MessageBoxButton.OK);
            }
            catch (NotSelectedException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ////ZARZĄDZANIE DRUZYNAMI//////////////////////////////////////////////////////////////////
        private void TAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                Manage Man = new Manage();
                // List<Team> Te = Tour.GetTeams();
                Man.Width = 615;
                Man.Height = 215;
                Man.AddTeam.Visibility = Visibility.Visible;
                if (true == Man.ShowDialog())
                {
                    Tour.CheckName(Man.NameT.Text);
                    Tour.AddTeam(new Team(Man.NameT.Text, new Player(Man.NameP1.Text, Man.SurnameP1.Text), new Player(Man.NameP2.Text, Man.SurnameP2.Text), new Player(Man.NameP3.Text, Man.SurnameP3.Text), new Player(Man.NameP4.Text, Man.SurnameP4.Text)));
                    /*Te.Add(new Team(Man.NameT.Text, new Player(Man.NameP1.Text, Man.SurnameP1.Text), new Player(Man.NameP2.Text, Man.SurnameP2.Text), new Player(Man.NameP3.Text, Man.SurnameP3.Text), new Player(Man.NameP4.Text, Man.SurnameP4.Text)));
                    Tour.SetTeams(Te);*/
                    Refresh();
                    Druzyny_Click(sender, e);
                }
            }
            catch (ExistNameException ex)
            {
                MessageBox.Show(ex.GetName() + ex.Message, "Error", MessageBoxButton.OK);
            }
        }///Ready
        private void TDelete(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Lista.SelectedItem == null)
                    throw new NotSelectedException("Nie wybrano drużyny do usunięcia");
                var I = Tour.GetTeams();
                I.RemoveAt(Lista.SelectedIndex);
                Tour.SetTeams(I);
                Refresh();
                Druzyny_Click(sender, e);
            }
            catch (NotSelectedException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }///Ready
        private void TEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Lista.SelectedItem == null)
                    throw new NotSelectedException("Nie wybrano drużyny do edycji");
                Manage Man = new Manage();
                string pop;
                Man.Width = 615;
                Man.Height = 215;
                Man.AddTeam.Visibility = Visibility.Visible;
                //////////////////////////////////////////////////////////////////////////////////////////
                pop = Man.NameT.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).GetName();
                Man.NameP1.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).GetPlayers()[0].GetName();
                Man.SurnameP1.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).GetPlayers()[0].GetSurname();
                Man.NameP2.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).GetPlayers()[1].GetName();
                Man.SurnameP2.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).GetPlayers()[1].GetSurname();
                Man.NameP3.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).GetPlayers()[2].GetName();
                Man.SurnameP3.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).GetPlayers()[2].GetSurname();
                Man.NameP4.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).GetPlayers()[3].GetName();
                Man.SurnameP4.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).GetPlayers()[3].GetSurname();
                //////////////////////////////////////////////////////////////////////////////////////////
                if (true == Man.ShowDialog())
                {
                    Tour.CheckName(Man.NameT.Text);
                    Tour.SetTeam(Lista.SelectedIndex, Man.NameT.Text, new Player(Man.NameP1.Text, Man.SurnameP1.Text), new Player(Man.NameP2.Text, Man.SurnameP2.Text), new Player(Man.NameP3.Text, Man.SurnameP3.Text), new Player(Man.NameP4.Text, Man.SurnameP4.Text));
                    Tour.SearchName(Man.NameT.Text);
                    Refresh();
                    Druzyny_Click(sender, e);
                }
            }
            catch (ExistNameException ex)
            {
                MessageBox.Show(ex.GetName() + ex.Message, "Error", MessageBoxButton.OK);
            }
            catch (NotSelectedException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }


        }///Ready
        ////////////////////////////////////////////////////////////////////////////////////////////
        ///
        //WYBOR MECZU PRZEZ DWUKROTNE KLIKNIECIE///////////////////////////////////////////////////////////////////////////////
        private void LeftDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Ass1.Text = "";
                Ass2.Text = "";
                if (Lista.SelectedItem == null)
                    throw new Exception();
                var I = (Match)((ListBoxItem)(Lista.SelectedItem)).Content;
                Main.Visibility = Visibility.Hidden;
                Match.Visibility = Visibility.Visible;
                Name1.Text = I.GetTeam1().GetName();
                Name2.Text = I.GetTeam2().GetName();
                if (I is VolleyBall)
                {
                    ASS.Visibility = Visibility.Visible;
                    Res1.Text = ((VolleyBall)I).GetResult1().ToString();
                    Res2.Text = ((VolleyBall)I).GetResult2().ToString();
                    Ref.Text = ((VolleyBall)I).GetReferee().ToString();
                    Type.Text = I.GetType().Name;
                    Ass1.Text = ((VolleyBall)I).GetAssistant1().ToString();
                    Ass2.Text = ((VolleyBall)I).GetAssistant2().ToString();
                }
                else
                {
                    ASS.Visibility = Visibility.Hidden;
                    Res1.Text = I.GetResult1().ToString();
                    Res2.Text = I.GetResult2().ToString();
                    Type.Text = I.GetType().Name;
                    Ref.Text = I.GetReferee().ToString();

                }
            }
            catch (Exception) { }
            /*ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
            Match I = (Match)(lbi.Content);*/
            // MessageBox.Show(I.T1.ToString(), "Error", MessageBoxButton.OK);
        }///Ready
        ////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ////USTAWIANIE WYNIKOW//////////////////////////////////////////////////////////
        private void SetScore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Lista.SelectedItem == null)
                    throw new NotSelectedException("Nie wybrano meczu");
                Score SC = new Score();
                Match Ob = ((Match)((ListBoxItem)Lista.SelectedItem).Content);
                SC.Team1.Text = Ob.GetTeam1().GetName();
                SC.Team2.Text = Ob.GetTeam2().GetName();
                SC.Score1.Text = Ob.GetResult1().ToString();
                SC.Score2.Text = Ob.GetResult2().ToString();
                if (true == SC.ShowDialog())
                {
                    int r = 0;
                    if (Ob is VolleyBall)
                        r = 1;
                    if (Ob is TugOfWar)
                        r = 2;
                    if (Ob is DodgeBall)
                        r = 3;
                    if (Ob is VolleyBall && Ob.WhatSemi == true)
                        r = 4;
                    if (Ob is VolleyBall && Ob.WhatFinal == true)
                        r = 5;
                    Tour.SetResult(Lista.SelectedIndex - r, int.Parse(SC.Score1.Text), int.Parse(SC.Score2.Text));
                    //Ob = GetMatch(Lista.SelectedIndex - 1);
                    Ob.SetWhoWon();
                    Tour.SaveScore(Ob, true);
                    //Tour.GenerateSemifinals();
                    //Tour.GenerateFinal();
                    Refresh();
                    Mecze_Click(sender, e);
                    //Tour.CountWins();
                    //Tour.SaveScore(Ob, true);

                }
            }
            catch (NotSelectedException ex)
            {
                MessageBox.Show(ex.Message, "Bład", MessageBoxButton.OK);
            }
            catch (Exception)
            {
                MessageBox.Show("Zła wartość", "Bład", MessageBoxButton.OK);
            }

        }///Ready
        ////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ////////////////////////////////////////////////////////////////////////////////////////////
        private void BackMain(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Visible;
            Match.Visibility = Visibility.Hidden;
            Lista.SelectedItem = false;
            ((ListBoxItem)(Lista.SelectedItem)).IsSelected = false;
        }///Ready
    }
}