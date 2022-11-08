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

        

        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            Random deelkaarten = new Random();
            BtnDeel.IsEnabled = false;
            BtnHit.IsEnabled = true;
            BtnStand.IsEnabled = true;
            TxbResultaat.Text = "♠ Let's play Blackjack ♣";
            int aantalkaartenspeler = 2;
            int aantalkaartenbank = 1;
            List<string> kaarten = new List<string>()
            {
            "Aas", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Boer", "Dame", "koning"
            };
            
            string soort = null;
            int kaartsoorten = 1;


            for (int i = 0; i < aantalkaartenspeler; i++)
            {
                kaartsoorten = deelkaarten.Next(1, 4);

                switch (kaartsoorten)
                {
                    case 1:
                        soort = "Harten";
                        break;
                    case 2:
                        soort = "Ruiten";
                        break;
                    case 3:
                        soort = "Schoppen";
                        break;
                    case 4:
                        soort = "Klaveren";
                        break;

                }
                
                int index = deelkaarten.Next(kaarten.Count);
                TbxKaartenSpeler.Text += soort + " " + kaarten[index] + "\r" ;
               
            }
            for (int i = 0; i < aantalkaartenbank; i++)
            {
                kaartsoorten = deelkaarten.Next(1, 4);

                switch (kaartsoorten)
                {
                    case 1:
                        soort = "Harten";
                        break;
                    case 2:
                        soort = "Ruiten";
                        break;
                    case 3:
                        soort = "Schoppen";
                        break;
                    case 4:
                        soort = "Klaveren";
                        break;

                }

                int index = deelkaarten.Next(kaarten.Count);
                TbxKaartenBank.Text = soort + " " + kaarten[index];
            }
            
        }

        
    }
}
