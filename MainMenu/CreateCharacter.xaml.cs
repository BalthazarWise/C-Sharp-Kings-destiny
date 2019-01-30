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
    /// Interaction logic for CreateCharacter.xaml
    /// </summary>
    public partial class CreateCharacter : Window
    {
        string _cName = "admin";
        string _cClass = "Warior";
        int _cStrength;
        int _cInteligence;
        int _cLuck;
        int _leftPoints;
        int _cMoney;
        int _cExp;
        int _cLevel;
        public CreateCharacter
            (
            int cStrength,
            int cInteligence,
            int cLuck,
            int leftPoints,
            int cMoney, 
            int cExp,
            int cLevel
            )            
        {
            InitializeComponent();
            _leftPoints = leftPoints;
            _cStrength = cStrength;
            _cInteligence = cInteligence;
            _cLuck = cLuck;
            _cMoney = cMoney;
            _cExp = cExp;
            _cLevel = cLevel;
            leftPointsLabel.Content = "Points\nleft\n" + _leftPoints;
        }

        private void Button_Click_CreateCharacter(object sender, RoutedEventArgs e)
        {
            _cClass = characterClass.Text;

            bool validation = true;

            // Name valid
            _cName = characterName.Text;
            foreach (char chr in _cName)
            {
                if (!((int)(chr) >= 48 && (int)(chr) <= 57 || (int)(chr) >= 65 && (int)(chr) <= 90 || (int)(chr) >= 97 && (int)(chr) <= 122))
                {
                    MessageBox.Show("Invalid name format. You can use only a-z & 0-9 symbols in your nickname");
                    validation = false;
                    break;
                }
            }

            //Class valid
            if (characterClass.Text == "")
            {
                MessageBox.Show("Chose your class!");
                validation = false;
            }

            // Stats valid sum
            if (_leftPoints != 0)
            {
                MessageBox.Show("Use all your points!");
                validation = false;
            }

            // Create character
            if (validation)
            {
                Character cPlayer = new Character
                (
                    _cName,
                    _cClass,
                    _cStrength,
                    _cInteligence,
                    _cLuck,
                    _cMoney,
                    _cExp,
                    _cLevel
                    
                );
                Character.SaveCharacter(cPlayer);
                MessageBox.Show("Character created");
                GameWindow gameWindow = new GameWindow();
                Close();
                gameWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                gameWindow.ShowDialog();
            }
        }

        private void Button_Click_AddStats(object sender, RoutedEventArgs e)
        {
            Button btn = new Button();
            btn = (Button)sender;

            switch (btn.Name)
            {
                case "countStrAdd":
                    {
                        if (_leftPoints > 0)
                        {
                            _cStrength++;
                            countStr.Content = _cStrength;
                            _leftPoints--;
                        }
                        break;
                    }
                case "countStrRemove":
                    {
                        if (_cStrength > 1)
                        {
                            _cStrength--;
                            countStr.Content = _cStrength;
                            _leftPoints++;
                        }
                        break;
                    }
                case "countIntAdd":
                    {
                        if (_leftPoints > 0)
                        {
                            _cInteligence++;
                            countInt.Content = _cInteligence;
                            _leftPoints--;
                        }
                        break;
                    }
                case "countIntRemove":
                    {
                        if (_cInteligence > 1)
                        {
                            _cInteligence--;
                            countInt.Content = _cInteligence;
                            _leftPoints++;
                        }
                        break;
                    }
                case "countLuckAdd":
                    {
                        if (_leftPoints > 0)
                        {
                            _cLuck++;
                            countLuck.Content = _cLuck;
                            _leftPoints--;
                        }
                        break;
                    }
                case "countLuckRemove":
                    {
                        if (_cLuck > 1)
                        {
                            _cLuck--;
                            countLuck.Content = _cLuck;
                            _leftPoints++;
                        }
                        break;
                    }
            }
            leftPointsLabel.Content = "Points\nleft\n" + _leftPoints;
        }
    }
}
