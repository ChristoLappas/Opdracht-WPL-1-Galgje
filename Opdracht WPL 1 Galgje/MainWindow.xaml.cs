using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Collections;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Opdracht_WPL_1_Galgje
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int levens = 10;
        string geheimwoord;        
        string fouteletters = "";
        string juisteletters = "";
        int picnum = 0;
        string mask;
        char[] geheimarray;
        
        



        public MainWindow()
        {
            InitializeComponent();

        }

        private void btnVerbergwoord_Click(object sender, RoutedEventArgs e)
        {

            
            if (txtResultaat.Text == string.Empty)
            {
                MessageBox.Show("Geef een geheim woord in !", "Geen woord ingevoerd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                btnRaad.IsEnabled = true;
                btnVerbergwoord.Visibility = Visibility.Hidden;
                geheimwoord = txtResultaat.Text;
                mask = new string('*', geheimwoord.Length);                
                txtResultaat.Focus();                
                lblResultaat.Content = $"{levens} Levens \nJuiste Letters:\nFoute Letters:\n\n{mask}";
                txtResultaat.Clear();
                

            }
        }

        private void btnRaad_Click(object sender, RoutedEventArgs e)
        {

            if (levens > 0)
            {
                if (txtResultaat.Text.Length == 1)
                {
                    if (juisteletters.Contains(txtResultaat.Text) || fouteletters.Contains(txtResultaat.Text))
                    {
                        MessageBox.Show("U heeft deze letter al ingegeven", "Dubbel", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        maskeren(txtResultaat.Text, mask);
                        RaadLetter(geheimwoord, txtResultaat.Text);
                        Vergelijk(geheimwoord, juisteletters);
                        txtResultaat.Clear();
                        txtResultaat.Focus();
                    }
                    



                }               
                else if (txtResultaat.Text == geheimwoord)
                {
                    lblResultaat.Content = $"Hoera !!\nJe hebt\n'{geheimwoord}'\ncorrect geraden !!\nSpeler 1\nheeft gewonnen";
                    txtResultaat.Clear();
                    txtResultaat.Focus();
                }
                else if (txtResultaat.Text.Length > 1)
                {
                    levens--;
                    galg.Source = new BitmapImage(new Uri(@"img/galg" + picnum + ".png", UriKind.RelativeOrAbsolute));
                    picnum++;
                    lblResultaat.Content = $"{levens} Levens \nJuiste Letters: {juisteletters}\nFoute Letters: {fouteletters}\n{mask}";
                    txtResultaat.Clear();
                    txtResultaat.Focus();
                }
                
            }
            if (levens == 0)
            {
                galg.Source = new BitmapImage(new Uri(@"img/galg9.png", UriKind.RelativeOrAbsolute));
                lblResultaat.Content = "Je hebt het geheime woord niet\nop tijd geraden !\nJe bent opgehangen !!\nSpeler 2 is de winnaar";
            }          




        }

        
       

        private void RaadLetter(string geheim, string letter)
        {          
            
            
            if (geheim.Contains(letter))
            {

                juisteletters += letter;               
                lblResultaat.Content = $"{levens} Levens \nJuiste Letters: {juisteletters}\nFoute Letters: {fouteletters}\n{mask}";

            }
            else
            {
                levens--;
                galg.Source = new BitmapImage(new Uri(@"img/galg" + picnum + ".png", UriKind.RelativeOrAbsolute));
                picnum++;
                fouteletters += letter;
                lblResultaat.Content = $"{levens} Levens \nJuiste Letters: {juisteletters}\nFoute Letters: {fouteletters}\n{mask}";

            }

        }

        private void Vergelijk(string a, string b)
        {
            if (juisteletters != string.Empty)
            {
                if (a != "" && a.All(e => b.Contains(e)) && b.All(e => a.Contains(e)))
                {
                    lblResultaat.Content = $"Hoera !!\nJe hebt\n'{geheimwoord}'\ncorrect geraden !!\nSpeler 1\nheeft gewonnen";
                }
            }
            

        }

        private void maskeren(string s, string m)
        {

            int lengte = geheimwoord.Length;
            char c;
            char letter = Convert.ToChar(s);
            geheimarray = m.ToCharArray();
            for (int i = 0; i < lengte; i++)
            {
                c = geheimwoord[i];
                if (c.Equals(letter))
                {
                    geheimarray[i] = letter;
                }
                
            }
            m = new string(geheimarray);
            mask = m;

        }

        private void btnNieuwspel_Click(object sender, RoutedEventArgs e)
        {
            txtResultaat.Clear();
            lblResultaat.Content = "Geef een geheim woord in";
            btnRaad.IsEnabled = false;
            btnVerbergwoord.Visibility = Visibility.Visible;
            levens = 10;
            picnum = 0;
            geheimwoord = "";
            fouteletters = "";
            juisteletters = "";
            galg.Source = default;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            DispatcherTimer wekker = new DispatcherTimer();
            wekker.Tick += new EventHandler(DispatcherTimer_Tick);
            wekker.Interval = new TimeSpan(0, 0, 1);
            wekker.Start();

            DateTime tijd = DateTime.Now;
            lblTijd.Content = $"{tijd.ToLongDateString()} {tijd.ToLongTimeString()}";

        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            lblTijd.Content = $"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}";
        }
    }
}
