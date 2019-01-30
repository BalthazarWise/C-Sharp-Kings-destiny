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
using System.Windows.Shapes;

namespace MainMenu
{
    /// <summary>
    /// Interaction logic for Market.xaml
    /// </summary>
    public partial class Market : Window
    {
        int _totalPrice = 0;
        int _potionSmallCount = 0;
        int _potionMediumCount = 0;
        int _potionLargeCount = 0;
        int _statPointsCount = 0;
        Character cPlayer = Character.LoadCharacter();
        public Market()
        {
            InitializeComponent();
            marketTitle.Content = "Market (you have " + cPlayer.Money + " coins)";
        }

        private void healingPotionSmallHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("permanently restore 50HP\n200 coins per potion");
        }

        private void healingPotionMediumHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("permanently restore 200HP\n500 coins per potion");
        }

        private void healingPotionLargeHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("permanently restore 500HP\n2000 coins per potion");
        }

        private void statPointsHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("get stat points to upgrade your stats\n 1000 coins per stat point");
        }

        private int ConvertSenderTextBoxToInt16 (object sender)
        {
            int count;
            TextBox countTextBox = (TextBox)sender;
            try
            {
                count = Convert.ToInt16(countTextBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid count");
                return 0;
            }
            return count;
        }

        private void healingPotionSmallCount_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _potionSmallCount = ConvertSenderTextBoxToInt16(sender);
            healingPotionSmallPrice.Content = 200 * _potionSmallCount;
            recountTotalPrice();
        }

        private void healingPotionMediumCount_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _potionMediumCount = ConvertSenderTextBoxToInt16(sender);
            healingPotionMediumPrice.Content = 500 * _potionMediumCount;
            recountTotalPrice();
        }

        private void healingPotionLargeCount_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _potionLargeCount = ConvertSenderTextBoxToInt16(sender);
            healingPotionLargePrice.Content = 2000 * _potionLargeCount;
            recountTotalPrice();
        }

        private void statPointsCount_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _statPointsCount = ConvertSenderTextBoxToInt16(sender);
            statPointsPrice.Content = 1000 * _statPointsCount;
            recountTotalPrice();
        }

        private void recountTotalPrice()
        {
            _totalPrice = _potionSmallCount * 200 + _potionMediumCount * 500 + _potionLargeCount * 2000 + _statPointsCount * 1000;
            marketBuy.Content = "Buy (" + _totalPrice + ")";
        }

        private void marketBuy_Click(object sender, RoutedEventArgs e)
        {
            if (cPlayer.Money < _totalPrice)
            {
                MessageBox.Show("Not enough money");
                return;
            }
            else
            {
                cPlayer.Money -= _totalPrice;
                cPlayer.HitPoints += _potionSmallCount * 50;
                cPlayer.HitPoints += _potionMediumCount * 200;
                cPlayer.HitPoints += _potionLargeCount * 500;
                cPlayer.statPointsCount += _statPointsCount;
                Character.SaveCharacter(cPlayer);
                GameWindow gameWindow = new GameWindow();
                Close();
                gameWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                gameWindow.ShowDialog();
            }
        }
    }
}
