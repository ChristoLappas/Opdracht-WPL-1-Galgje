using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Collections;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Media;
using Microsoft.VisualBasic;
using System.Text;


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
        char[] woordarray;
        int teller = 10;
        int teller2 = 1;
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        DispatcherTimer timer3 = new DispatcherTimer();
        StringBuilder historiek = new StringBuilder();
        string speler;
        Random rnd = new Random();
        int indexWoord;
        bool hint = false;
        int rndletter;
        char hintje;
        int nieuweTeller = 10;
        int T;
        bool isGetal;
        char[] alfabet = { 'a', 'z', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'q', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'w', 'x', 'c', 'v', 'b', 'n' };
        private string[] galgjeWoorden = new string[]
        {
            "grafeem",
            "tjiftjaf",
            "maquette",
            "kitsch",
            "pochet",
            "convocaat",
            "jakkeren",
            "collaps",
            "zuivel",
            "cesium",
            "voyant",
            "spitten",
            "pancake",
            "gietlepel",
            "karwats",
            "dehydreren",
            "viswijf",
            "flater",
            "cretonne",
            "sennhut",
            "tichel",
            "wijten",
            "cadeau",
            "trotyl",
            "chopper",
            "pielen",
            "vigeren",
            "vrijuit",
            "dimorf",
            "kolchoz",
            "janhen",
            "plexus",
            "borium",
            "ontweien",
            "quiche",
            "ijverig",
            "mecenaat",
            "falset",
            "telexen",
            "hieruit",
            "femelaar",
            "cohesie",
            "exogeen",
            "plebejer",
            "opbouw",
            "zodiak",
            "volder",
            "vrezen",
            "convex",
            "verzenden",
            "ijstijd",
            "fetisj",
            "gerekt",
            "necrose",
            "conclaaf",
            "clipper",
            "poppetjes",
            "looikuip",
            "hinten",
            "inbreng",
            "arbitraal",
            "dewijl",
            "kapzaag",
            "welletjes",
            "bissen",
            "catgut",
            "oxymoron",
            "heerschaar",
            "ureter",
            "kijkbuis",
            "dryade",
            "grofweg",
            "laudanum",
            "excitatie",
            "revolte",
            "heugel",
            "geroerd",
            "hierbij",
            "glazig",
            "pussen",
            "liquide",
            "aquarium",
            "formol",
            "kwelder",
            "zwager",
            "vuldop",
            "halfaap",
            "hansop",
            "windvaan",
            "bewogen",
            "vulstuk",
            "efemeer",
            "decisief",
            "omslag",
            "prairie",
            "schuit",
            "weivlies",
            "ontzeggen",
            "schijn",
            "sousafoon"
        };



        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(DispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);

            timer2.Tick += new EventHandler(DispatcherTimer2_Tick);
            timer2.Interval = new TimeSpan(0, 0, 1);

            timer3.Tick += new EventHandler(DispatcherTimer3_Tick);
            timer3.Interval = new TimeSpan(0, 0, 1);
            timer3.Start();
            DateTime tijd = DateTime.Now;
            lblTijd2.Content = $"{tijd.ToLongTimeString()}";




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
                lblHint.IsEnabled = true;
                lblRaad.Opacity = 1;
                lblHint.Opacity = 1;
                lblVerbergwoord.Visibility = Visibility.Hidden;
                geheimwoord = txtResultaat.Text;
                mask = new string('*', geheimwoord.Length);
                txtResultaat.Focus();
                lblResultaat.Content = $"{levens} Levens \nJuiste Letters:\nFoute Letters:\n\n{mask}";
                txtResultaat.Clear();
                woordarray = geheimwoord.ToCharArray();
                lblTimer.Visibility = Visibility.Hidden;


            }

        }


        private void lblRaad_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            teller = nieuweTeller;
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
                    if (hint == true)
                    {
                        timer.Stop();
                        lblTijd.Content = "";
                        lblResultaat.Content = $"Hoera !! Je hebt '{geheimwoord}'\ncorrect geraden !!\nSpeler 1 heeft gewonnen";
                        txtResultaat.Clear();
                        txtResultaat.Focus();
                        MessageBox.Show(historiek.ToString(), "Highscores");
                    }
                    else
                    {
                        timer.Stop();
                        lblTijd.Content = "";
                        lblResultaat.Content = $"Hoera !! Je hebt '{geheimwoord}'\ncorrect geraden !!\nSpeler 1 heeft gewonnen";
                        txtResultaat.Clear();
                        txtResultaat.Focus();
                        historiek.Append($"{speler} - {levens} levens ({DateTime.Now.ToString("hh:mm:ss")})\n");
                        MessageBox.Show(historiek.ToString(), "Highscores");
                    }
                    
                }
                else if (txtResultaat.Text.Length > 1)
                {

                    levens--;
                    galg.Source = new BitmapImage(new Uri(@"img/galg" + picnum + ".png", UriKind.RelativeOrAbsolute));
                    picnum++;
                    lblResultaat.Content = $"{levens} Levens \nJuiste Letters: {juisteletters}\nFoute Letters: {fouteletters}\n\n{mask}";
                    txtResultaat.Clear();
                    txtResultaat.Focus();
                }

            }
            if (levens == 0)
            {
                timer.Stop();
                lblTijd.Content = "";
                galg.Source = new BitmapImage(new Uri(@"img/galg9.png", UriKind.RelativeOrAbsolute));
                lblResultaat.Content = $"Je hebt '{geheimwoord}' niet\nop tijd geraden !\nJe bent opgehangen !!\nSpeler 2 is de winnaar";
                MessageBox.Show(historiek.ToString(), "Highscores");
            }
        }
        private void lblNieuwspel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var single = MessageBox.Show("Singleplayer of niet ?", "Singleplayer/Multiplayer", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (single == MessageBoxResult.Yes)
            {
                var tellerInstellen = MessageBox.Show("Huidige timer staat op 10\nWil je een nieuwe timer instellen ? ", "Timer instellen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (tellerInstellen == MessageBoxResult.Yes)
                {
                    isGetal = int.TryParse((Interaction.InputBox("Geef een nieuwe timer in", "Timer aanpassen")), out T);
                    while (true)
                    {
                        if (isGetal == true && T >= 5 && T <= 20)
                        {
                            nieuweTeller = T;
                            speler = Interaction.InputBox("Geef de naam van speler 1", "Naam");
                            indexWoord = rnd.Next(0, galgjeWoorden.Length);
                            geheimwoord = galgjeWoorden[indexWoord];
                            timer.Start();
                            lblRaad.IsEnabled = true;
                            lblHint.IsEnabled = true;
                            lblRaad.Opacity = 1;
                            lblHint.Opacity = 1;
                            lblVerbergwoord.Visibility = Visibility.Hidden;
                            mask = new string('*', geheimwoord.Length);
                            txtResultaat.Focus();
                            hint = false;
                            levens = 10;
                            teller = T;
                            picnum = 0;
                            fouteletters = "";
                            juisteletters = "";
                            galg.Source = default;
                            lblTimer.Visibility = Visibility.Hidden;
                            lblResultaat.Content = $"{levens} Levens \nJuiste Letters:\nFoute Letters:\n\n{mask}";
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Nieuwe timer moet een getal zijn tussen 5 en 20", "Foute ingave", MessageBoxButton.OK, MessageBoxImage.Error);
                            isGetal = int.TryParse((Interaction.InputBox("Geef een nieuwe timer in", "Timer aanpassen")), out T);
                        }
                    }
                    
                }
                else
                {
                    speler = Interaction.InputBox("Geef de naam van speler 1", "Naam");
                    indexWoord = rnd.Next(0, galgjeWoorden.Length);
                    geheimwoord = galgjeWoorden[indexWoord];
                    timer.Start();
                    lblRaad.IsEnabled = true;
                    lblRaad.Opacity = 1;
                    lblHint.IsEnabled = true;
                    lblHint.Opacity = 1;
                    lblVerbergwoord.Visibility = Visibility.Hidden;
                    mask = new string('*', geheimwoord.Length);
                    txtResultaat.Focus();
                    hint = false;
                    levens = 10;
                    teller = 10;
                    nieuweTeller = 10;
                    picnum = 0;
                    fouteletters = "";
                    juisteletters = "";
                    galg.Source = default;
                    lblTimer.Visibility = Visibility.Visible;
                    lblResultaat.Content = $"{levens} Levens \nJuiste Letters:\nFoute Letters:\n\n{mask}";
                }
                
                


            }
            else
            {                
                txtResultaat.Clear();
                lblResultaat.Content = "Geef een geheim woord in";
                lblRaad.IsEnabled = false;
                lblRaad.Opacity = .7;
                lblHint.IsEnabled = false;
                lblHint.Opacity = .7;
                lblVerbergwoord.Visibility = Visibility.Visible;
                levens = 10;
                teller = 10;
                nieuweTeller = 10;
                picnum = 0;
                geheimwoord = "";
                fouteletters = "";
                juisteletters = "";
                hint = false;
                galg.Source = default;
                lblTimer.Visibility = Visibility.Visible;
                timer.Stop();
                lblTijd.Content = "";                
                speler = Interaction.InputBox("Geef de naam van speler 1", "Naam");
            }
            

        }



        private void RaadLetter(string geheim, string letter)
        {


            if (geheim.Contains(letter))
            {

                juisteletters += letter;
                lblResultaat.Content = $"{levens} Levens \nJuiste Letters: {juisteletters}\nFoute Letters: {fouteletters}\n\n{mask}";

            }
            else
            {
                levens--;
                galg.Source = new BitmapImage(new Uri(@"img/galg" + picnum + ".png", UriKind.RelativeOrAbsolute));
                picnum++;
                fouteletters += letter;
                lblResultaat.Content = $"{levens} Levens \nJuiste Letters: {juisteletters}\nFoute Letters: {fouteletters}\n\n{mask}";

            }

        }

        private void Vergelijk(string a, string b)
        {
            if (juisteletters != string.Empty)
            {
                if (a != "" && a.All(e => b.Contains(e)) && b.All(e => a.Contains(e)))
                {
                    if (hint == true)
                    {
                        timer.Stop();
                        lblTijd.Content = "";
                        lblResultaat.Content = $"Hoera !!\nJe hebt\n'{geheimwoord}'\ncorrect geraden !!\nSpeler 1\nheeft gewonnen";
                        MessageBox.Show(historiek.ToString(), "Highscores");
                    }
                    else
                    {
                        timer.Stop();
                        lblTijd.Content = "";
                        lblResultaat.Content = $"Hoera !!\nJe hebt\n'{geheimwoord}'\ncorrect geraden !!\nSpeler 1\nheeft gewonnen";
                        historiek.Append($"{speler} - {levens} levens ({DateTime.Now.ToString("hh:mm:ss")})\n");
                        MessageBox.Show(historiek.ToString(), "Highscores");
                    }
                    
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
                teller = nieuweTeller;
                timer2.Start();
            }
            if (teller == 0 && levens == 1)
            {
                timer.Stop();
                lblTijd.Content = "";
                galg.Source = new BitmapImage(new Uri(@"img/galg9.png", UriKind.RelativeOrAbsolute));
                lblResultaat.Content = $"Je hebt '{geheimwoord}' niet\nop tijd geraden !\nJe bent opgehangen !!\nSpeler 2 is de winnaar";
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
        private void DispatcherTimer3_Tick(object sender, EventArgs e)
        {
            lblTijd2.Content = $"{DateTime.Now.ToLongTimeString()}";
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var single = MessageBox.Show("Singleplayer of niet ?", "Singleplayer/Multiplayer", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (single == MessageBoxResult.Yes)
            {
                var tellerInstellen = MessageBox.Show("Huidige timer staat op 10\nWil je een nieuwe timer instellen ? ", "Timer instellen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (tellerInstellen == MessageBoxResult.Yes)
                {
                    isGetal = int.TryParse((Interaction.InputBox("Geef een nieuwe timer in", "Timer instellen")), out T);
                    while (true)
                    {
                        if (isGetal == true && T >= 5 && T <= 20)
                        {
                            nieuweTeller = T;
                            teller = T;
                            speler = Interaction.InputBox("Geef de naam van speler 1", "Naam");
                            indexWoord = rnd.Next(0, galgjeWoorden.Length);
                            geheimwoord = galgjeWoorden[indexWoord];
                            woordarray = geheimwoord.ToCharArray();
                            timer.Start();
                            lblRaad.IsEnabled = true;
                            lblRaad.Opacity = 1;
                            lblHint.IsEnabled = true;
                            lblHint.Opacity = 1;
                            lblVerbergwoord.Visibility = Visibility.Hidden;
                            lblTimer.Visibility = Visibility.Hidden;
                            mask = new string('*', geheimwoord.Length);
                            txtResultaat.Focus();
                            lblResultaat.Content = $"{levens} Levens \nJuiste Letters:\nFoute Letters:\n\n{mask}";
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Nieuwe timer moet een getal zijn tussen 5 en 20", "Foute ingave", MessageBoxButton.OK, MessageBoxImage.Error);
                            isGetal = int.TryParse((Interaction.InputBox("Geef een nieuwe timer in", "Timer instellen")), out T);
                        }
                    }
                    
                }
                else
                {
                    speler = Interaction.InputBox("Geef de naam van speler 1", "Naam");
                    indexWoord = rnd.Next(0, galgjeWoorden.Length);
                    geheimwoord = galgjeWoorden[indexWoord];
                    woordarray = geheimwoord.ToCharArray();
                    timer.Start();
                    lblRaad.IsEnabled = true;
                    lblRaad.Opacity = 1;
                    lblHint.IsEnabled = true;
                    lblHint.Opacity = 1;
                    lblVerbergwoord.Visibility = Visibility.Hidden;
                    lblTimer.Visibility = Visibility.Hidden;
                    mask = new string('*', geheimwoord.Length);
                    txtResultaat.Focus();
                    lblResultaat.Content = $"{levens} Levens \nJuiste Letters:\nFoute Letters:\n\n{mask}";
                }               


            }
            else
            {
                speler = Interaction.InputBox("Geef de naam van speler 1", "Naam");
                txtResultaat.Focus();
            }
            
            
        }

        private void lblHint_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            lblHint.BorderBrush = new SolidColorBrush(Colors.Black);
        }

        private void lblHint_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            lblHint.BorderBrush = new SolidColorBrush(Colors.Gray);
        }

        private void lblHint_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            hint = true;
            rndletter = rnd.Next(0, 26);
            while (true)
            {
                if (woordarray.Contains(alfabet[rndletter]))
                {
                    rndletter = rnd.Next(0, 26);
                }
                else
                {
                    hintje = alfabet[rndletter];
                    break;
                }
                
            }
            
            MessageBox.Show($"Letter '{hintje}' zit niet in het geheim woord", "Hint", MessageBoxButton.OK);
        }

        private void lblTimer_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            lblTimer.BorderBrush = new SolidColorBrush(Colors.Black);
        }

        private void lblTimer_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            lblTimer.BorderBrush = new SolidColorBrush(Colors.Gray);
        }

        private void lblTimer_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isGetal = int.TryParse((Interaction.InputBox("Geef een nieuwe timer in", "Timer instellen")), out T);
            if (isGetal == true && T >= 5 && T <= 20)
            {
                nieuweTeller = T;
                teller = T;
                
            }
            else
            {
                MessageBox.Show("Nieuwe timer moet een getal zijn tussen 5 en 20", "Foute ingave", MessageBoxButton.OK, MessageBoxImage.Error);
                isGetal = int.TryParse((Interaction.InputBox("Geef een nieuwe timer in", "Timer aanpassen")), out T);
            }

        }
    }
}
