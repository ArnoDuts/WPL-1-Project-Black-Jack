using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace WPL_1_Project_Black_Jack
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
        
        Random deelkaarten = new Random();
        int AantalKaarten = 2;
        readonly List<string> kaarten = new List<string>()
            {
            "Aas", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Boer", "Dame", "Koning"
            };
        string soort = null;
        int kaartsoorten = 1;
        List<int> totaalSpeler = new List<int>();
        List<int> totaalBank = new List<int>();
        int kapitaal = 0;
        int inzet = 0;
        List<string> nieuwDeck = new List<string>();
        int kaartenInDeck = 52;

        private List<string> DeckVullen()
        {
            List<string> kaarten = new List<string>();
            string[] kaartSoorten = {"Spades","Clubs","Hearts","Diamonds"};
            string[] kaartWaardes = {"2","3","4","5","6","7","8","9","10","Ace","Jack","Queen","King" };
            foreach (string kaartWaarde in kaartWaardes)
            {
                foreach (string kaartSoort in kaartSoorten)
                {
                    kaarten.Add(kaartWaarde.ToLower()+ "_of_" + kaartSoort.ToLower() + ".png");
                }
            }
            return kaarten;
        }

        public string GeefKaart(Boolean isSpeler)
        {
            if (nieuwDeck.Count < 1)
            {
                nieuwDeck = DeckVullen();
            }
            string kaart = nieuwDeck[deelkaarten.Next(nieuwDeck.Count)];

            Image image = new Image();
            image.Source = new BitmapImage(new Uri($"Images/{kaart}", UriKind.Relative));
            ImgContainer.Children.Add(image);
            string waarde = kaart.Split('.')[0].Split("_of_")[1];

            if (isSpeler == true)
            {
                if (waarde == "Boer" || waarde == "Dame" || waarde == "Koning")
                {
                    totaalSpeler.Add(10);
                }
                else if (waarde == "Aas")
                {
                    totaalSpeler.Add(11);
                }
                else
                {
                    totaalSpeler.Add(int.Parse(waarde));
                }
                TxbTotaalSpeler.Text = totaalSpeler.Sum().ToString();
            }
            if (isSpeler == false)
            {
                if (waarde == "Boer" || waarde == "Dame" || waarde == "Koning")
                {
                    totaalSpeler.Add(10);
                }
                else if (waarde == "Aas")
                {
                    totaalSpeler.Add(11);
                }
                else
                {
                    totaalBank.Add(int.Parse(waarde));
                }
                TxbTotaalBank.Text = totaalBank.Sum().ToString();
            }
            
            
        }

        

        private async void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            ClearGame();
            BtnDeel.IsEnabled = false;
            inzet = int.Parse(txbInzet.Text);
            BtnHit.IsEnabled = true;
            BtnStand.IsEnabled = true;
            BtnDoubleDown.IsEnabled = true;
            txbInzet.IsEnabled = false;
            TxbResultaat.Text = "♠ Let's play Blackjack ♣";
            TxbResultaat.Foreground = Brushes.Black;

            if (inzet > int.Parse(txbKapitaal.Text))
            {
                MessageBox.Show("Uw inzet is groter dan uw kapitaal. Probeer het opnieuw!");
                ClearGame();
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                txbInzet.IsEnabled = true;

            }
            else if (inzet < (int.Parse(txbKapitaal.Text)/100) * 10)
            {
                MessageBox.Show("Uw inzet moet 10% van het huidige kapitaal zijn! Probeer het opnieuw!");
                ClearGame();
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                txbInzet.IsEnabled = true;
            }

            AantalKaarten = 2;
            for (int i = 0; i < AantalKaarten; i++)
            {

                TbxKaartenSpeler.Text += GeefKaart(true);
                await Task.Delay(1000);
            }

            AantalKaarten = 1;
            for (int i = 0; i < AantalKaarten; i++)
            {

                TbxKaartenBank.Text += GeefKaart(false);
            }

        }

        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
            TbxKaartenSpeler.Text+= GeefKaart(true);
            if (int.Parse(TxbTotaalSpeler.Text) > 21)
            {
                TxbResultaat.Text = "Verloren";
                TxbResultaat.Foreground = Brushes.Red;
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
            }
            else if (int.Parse(TxbTotaalSpeler.Text) > 21 && TxbResultaat.Text.Contains("Aas"))
            {
                TxbTotaalSpeler.Text = (int.Parse(TxbTotaalSpeler.Text) - 10).ToString();
            }
            else if (int.Parse(TxbTotaalSpeler.Text) == 21)
            {
                TxbResultaat.Text = "Blackjack!";
                TxbResultaat.Foreground = Brushes.Green;
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                txbKapitaal.Text = (int.Parse(txbKapitaal.Text) + (inzet * 2)).ToString();
            }
            
        }

        private async void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            


            while (int.Parse(TxbTotaalBank.Text) <= 17)
            {
                TbxKaartenBank.Text += GeefKaart(false);
                await Task.Delay(1000);

            }

            
            if (int.Parse(TxbTotaalBank.Text) > 21)
            {
                TxbResultaat.Text = "Gewonnen";
                TxbResultaat.Foreground = Brushes.Green;
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                txbKapitaal.Text = (int.Parse(txbKapitaal.Text) + (inzet * 2)).ToString();
                txbInzet.IsEnabled = true;


            }
            else if (int.Parse(TxbTotaalBank.Text) < int.Parse(TxbTotaalSpeler.Text))
            {
                TxbResultaat.Text = "Gewonnen";
                TxbResultaat.Foreground = Brushes.Green;
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                txbKapitaal.Text = (int.Parse(txbKapitaal.Text) + (inzet * 2)).ToString();
                txbInzet.IsEnabled = true;

            }
            else if (int.Parse(TxbTotaalBank.Text) > int.Parse(TxbTotaalSpeler.Text) && int.Parse(TxbTotaalBank.Text) > 21)
            {
                TxbResultaat.Text = "Gewonnen";
                TxbResultaat.Foreground = Brushes.Green;
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                txbKapitaal.Text = (int.Parse(txbKapitaal.Text) + (inzet * 2)).ToString();
                txbInzet.IsEnabled = true;
            }
            else if (int.Parse(TxbTotaalBank.Text) == int.Parse(TxbTotaalSpeler.Text))
            {
                TxbResultaat.Text = "Push";
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                txbKapitaal.Text = (int.Parse(txbKapitaal.Text) + inzet).ToString();
                txbInzet.IsEnabled = true;
            }
            else
            {
                TxbResultaat.Text = "Verloren";
                TxbResultaat.Foreground = Brushes.Red;
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                txbKapitaal.Text =(int.Parse(txbKapitaal.Text) - inzet).ToString();
                txbInzet.IsEnabled = true;

            }

            if (kapitaal == 0)
            {
                BtnDeel.IsEnabled = false;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                MessageBox.Show("U kan het spel niet verder spelen! Start een nieuw spel");
            }
            
        }

        private void btnNieuwSpel_Click(object sender, RoutedEventArgs e)
        {
            kapitaal = 100;
            txbKapitaal.Text = kapitaal.ToString();
            TxbResultaat.Foreground = Brushes.Black;
            BtnDeel.IsEnabled = true;
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            ClearGame();
            txbInzet.IsEnabled = true;
            
        }

        private void ClearGame ()
        {
            TxbResultaat.Text = "♠ Let's play Blackjack ♣";
            TbxKaartenSpeler.Text = "";
            TbxKaartenBank.Text = "";
            totaalBank.Clear();
            totaalSpeler.Clear();
            TxbTotaalBank.Text = "0";
            TxbTotaalSpeler.Text = "0";
        }

        private void BtnDoubleDown_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(txbKapitaal.Text) < 2 * inzet)
            {
                MessageBox.Show("U heeft niet genoeg kapitaal op dubbele inzet te gebruiken.");
            }
            else
            {
                txbInzet.Text = (2 * inzet).ToString();
            }
        }
    }
}
