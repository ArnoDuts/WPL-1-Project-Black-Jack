﻿using System;
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
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Boer", "Dame", "Koning"
            };
        string soort = null;
        int kaartsoorten = 1;
        List<int> totaalSpeler = new List<int>();
        List<int> totaalBank = new List<int>();
        
        public string GeefKaart(Boolean isSpeler)
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
            if (isSpeler == true)
            {
                if (kaarten[index] == "Boer" || kaarten[index] == "Dame" || kaarten[index] == "Koning")
                {
                    totaalSpeler.Add(10);
                }
                else
                {
                    totaalSpeler.Add(Convert.ToInt32(kaarten[index]));
                }
                TxbTotaalSpeler.Text = Convert.ToString(totaalSpeler.Sum());
            }
            if (isSpeler == false)
            {
                if (kaarten[index] == "Boer" || kaarten[index] == "Dame" || kaarten[index] == "Koning")
                {
                    totaalBank.Add(10);
                }
                else
                {
                    totaalBank.Add(Convert.ToInt32(kaarten[index]));
                }
                TxbTotaalBank.Text = Convert.ToString(totaalBank.Sum());
            }
            
            return soort + " " + kaarten[index] + "\r";
        }

        

        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            
            BtnDeel.IsEnabled = false;
            BtnHit.IsEnabled = true;
            BtnStand.IsEnabled = true;
            TxbResultaat.Text = "♠ Let's play Blackjack ♣";
            TbxKaartenSpeler.Text = "";
            TbxKaartenBank.Text = "";
            TxbTotaalBank.Text = "";
            TxbTotaalSpeler.Text = "";
            




            for (int i = 0; i < AantalKaarten; i++)
            {
                
                TbxKaartenSpeler.Text += GeefKaart(true);
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
            if (Convert.ToInt32(TxbTotaalSpeler.Text) > 21)
            {
                TxbResultaat.Text = "Verloren";
                TxbResultaat.Foreground = Brushes.Red;
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
            }
            //if(Convert.ToInt32(TxbTotaalBank.Text) > 21)
            //{
            //    TxbResultaat.Text = "Gewonnen";
            //    TxbResultaat.Foreground = Brushes.Green;
            //    BtnDeel.IsEnabled = true;
            //    BtnHit.IsEnabled = false;
            //    BtnStand.IsEnabled = false;
            //}
            if (Convert.ToInt32(TxbTotaalSpeler.Text) == 21)
            {
                TxbResultaat.Text = "Blackjack!";
                TxbResultaat.Foreground = Brushes.Green;
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
            }
            
        }

        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            


            while (Convert.ToInt32(TxbTotaalBank.Text) <= 17)
            {
                TbxKaartenBank.Text += GeefKaart(false);

                
            }
            if (Convert.ToInt32(TxbTotaalBank.Text) > 21)
            {
                TxbResultaat.Text = "Gewonnen";
                TxbResultaat.Foreground = Brushes.Green;
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                
                
            }
            if (Convert.ToInt32(TxbTotaalBank.Text) < Convert.ToInt32(TxbTotaalSpeler.Text))
            {
                TxbResultaat.Text = "Gewonnen";
                TxbResultaat.Foreground = Brushes.Green;
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;

                
            }
            if (Convert.ToInt32(TxbTotaalBank.Text) > Convert.ToInt32(TxbTotaalSpeler.Text) && Convert.ToInt32(TxbTotaalBank.Text) > 21)
            {
                TxbResultaat.Text = "Gewonnen";
                TxbResultaat.Foreground = Brushes.Green;
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
            }
            else
            {
                TxbResultaat.Text = "Verloren";
                TxbResultaat.Foreground = Brushes.Red;
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                
            }
            
        }

        
    }
}
