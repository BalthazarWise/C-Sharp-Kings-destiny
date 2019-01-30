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
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Character cPlayer = Character.LoadCharacter();

        public GameWindow()
        {
            InitializeComponent();
            titleLabel.Content = cPlayer.Name + " - Level " + cPlayer.Level + " - " + cPlayer.Money + " coins";
            hitPointsCount.Content = cPlayer.HitPoints;
            manaPointsCount.Content = cPlayer.ManaPoints;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Save & Exit
            MessageBox.Show("Character saved\nWe'll wait for you, keeper!");
            Character.SaveCharacter(cPlayer);
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Adventure
            AdventureMap form = new AdventureMap();
            Close();
            form.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Market
            Market form = new Market();
            form.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Close();
            form.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CreateCharacter form = new CreateCharacter(cPlayer.Strength, cPlayer.Inteligence, cPlayer.Luck, cPlayer.statPointsCount, cPlayer.Money, cPlayer.Experience, cPlayer.Level);
            form.characterName.IsEnabled = false;
            form.characterClass.IsEnabled = false;
            form.countIntRemove.IsEnabled = false;
            form.countStrRemove.IsEnabled = false;
            form.countLuckRemove.IsEnabled = false;
            form.SaveCharacter.Content = "Save character";
            form.characterName.Text = cPlayer.Name;
            form.characterClass.Text = cPlayer.CharacterClass;
            form.countStr.Content = cPlayer.Strength;
            form.countInt.Content = cPlayer.Inteligence;
            form.countLuck.Content = cPlayer.Luck;
            Close();
            form.ShowDialog();

        }
    }
}
