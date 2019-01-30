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

namespace MainMenu
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // New game
            CreateCharacter createCharacter = new CreateCharacter(1, 1, 1, 15, 100, 0, 1);
            Close();
            createCharacter.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            createCharacter.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Load game
            GameWindow gameWindow = new GameWindow();
            Close();
            gameWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            gameWindow.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // About
            MessageBox.Show("About");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // Exit
            MessageBox.Show("We'll wait for you, keeper!");
            Close();
        }
    }
}
