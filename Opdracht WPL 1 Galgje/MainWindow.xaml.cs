using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Collections;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Media;

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
        int teller = 10;
        int teller2 = 1;
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        ImageBrush gallow = new ImageBrush();
        




        public MainWindow()
        {
            InitializeComponent();            
            timer.Tick += new EventHandler(DispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);

            timer2.Tick += new EventHandler(DispatcherTimer2_Tick);
            timer2.Interval = new TimeSpan(0, 0, 1);








        }

        private void lblVerbergwoord_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
            if (txtResultaat.Text == string.Empty)
            {
                MessageBox.Show("Geef een geheim woord in !", "Geen woord ingevoerd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                timer.Start();              
                
                lblRaad.IsEnabled = true;
                lblRaad.Opacity = 1;
                lblVerbergwoord.Visibility = Visibility.Hidden;
                geheimwoord = txtResultaat.Text;
                mask = new string('*', geheimwoord.Length);
                txtResultaat.Focus();
                lblResultaat.Content = $"{levens} Levens \nJuiste Letters:\nFoute Letters:\n\n{mask}";
                txtResultaat.Clear();               


            }
            
        }


        private void lblRaad_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            teller = 10;
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
                    timer.Stop();
                    lblTijd.Content = "";
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
                timer.Stop();
                lblTijd.Content = "";
                galg.Source = new BitmapImage(new Uri(@"img/galg9.png", UriKind.RelativeOrAbsolute));
                lblResultaat.Content = "Je hebt het geheime woord niet\nop tijd geraden !\nJe bent opgehangen !!\nSpeler 2 is de winnaar";
            }
        }
        private void lblNieuwspel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            txtResultaat.Clear();
            lblResultaat.Content = "Geef een geheim woord in";
            lblRaad.IsEnabled = false;
            lblRaad.Opacity = .7;
            lblVerbergwoord.Visibility = Visibility.Visible;
            levens = 10;
            teller = 10;
            picnum = 0;
            geheimwoord = "";
            fouteletters = "";
            juisteletters = "";
            galg.Source = default;
            
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
                    timer.Stop();
                    lblTijd.Content = "";
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

        
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
                        
            lblTijd.Content = teller;
            teller--;
            if (teller < 0)
            {
                timer.Stop();
                grid.Visibility = Visibility.Visible;              
                levens--;
                lblResultaat.Content = $"{levens} Levens \nJuiste Letters:\nFoute Letters:\n\n{mask}";
                galg.Source = new BitmapImage(new Uri(@"img/galg" + picnum + ".png", UriKind.RelativeOrAbsolute));
                picnum++;
                teller = 10;
                timer2.Start();                      
            }         
            
        }

        private void DispatcherTimer2_Tick(object sender, EventArgs e)
        {
            teller2--;
            if (teller2 == 0)
            {
                timer2.Stop();
                teller2 = 1;
                grid.Visibility = Visibility.Hidden;
                timer.Start();

            }
        }



        private void lblRaad_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            lblRaad.BorderBrush = new SolidColorBrush(Colors.Black);
        }

        private void lblRaad_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            lblRaad.BorderBrush = new SolidColorBrush(Colors.Gray);
        }

        private void lblNieuwspel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            lblNieuwspel.BorderBrush = new SolidColorBrush(Colors.Black);
        }

        private void lblNieuwspel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            lblNieuwspel.BorderBrush = new SolidColorBrush(Colors.Gray);
        }

        private void lblVerbergwoord_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            lblVerbergwoord.BorderBrush = new SolidColorBrush(Colors.Black);
        }

        private void lblVerbergwoord_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            lblVerbergwoord.BorderBrush = new SolidColorBrush(Colors.Gray);
        }
    }
}
