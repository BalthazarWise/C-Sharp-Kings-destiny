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
    /// Interaction logic for BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window
    {
        Creatures _creature;
        Character _player;
        Random rnd = new Random();
        string battleLog = "The battle begins\n";
        public BattleWindow(Creatures creature, Character cPlayer)
        {
            InitializeComponent();
            _creature = creature;
            _player = cPlayer;
            creaturePicture.Background = GetImageBrush(_creature._imageNameWithoutBackground);
            cPlayerPicture.Background = GetImageBrush(@"Warrior.png");
            cPlayerHP.Content = _player.HitPoints;
            cPlayerMP.Content = _player.ManaPoints;
            creatureHP.Content = creature.HitPoints;
            creatureMP.Content = _creature.ManaPoints;
            Instructions.Content = "Press 'A' to Attack or 'E' to Escape";
        }
        private static ImageBrush GetImageBrush(string fileName)
        {
            ImageBrush imgBrush = new ImageBrush();
            Label tileLabel = new Label();
            BitmapImage bmi = new BitmapImage();
            string fullPath = @"Images\" + fileName;

            bmi.BeginInit();
            bmi.UriSource = new Uri(fullPath, UriKind.Relative);
            bmi.EndInit();

            imgBrush.ImageSource = bmi;

            return imgBrush;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            DoBattle(e);
        }
        private void DoBattle(KeyEventArgs e)
        {
            int round = 0;
            int damage = 0;
            switch (e.Key)
            {
                case Key.A:
                    {
                        do
                        {
                            battleLog += "ROUND " + round + "\n";
                            // Player hit
                            damage = rnd.Next(_player.Strength);
                            if (rnd.Next(50) / _player.Luck == 0)
                            {
                                damage *= rnd.Next(_player.Luck);
                                battleLog += "Critical strike! ";
                            }
                            _creature.HitPoints -= damage;
                            battleLog += "you're dealt " + damage + " damage     ";
                            battleLog += "you're HP = " + _player.HitPoints + "\n";
                            // Creature hit
                            damage = rnd.Next(_creature.Strength);
                            if (rnd.Next(50) / _creature.Luck == 0)
                            {
                                damage *= rnd.Next(_creature.Luck);
                                battleLog += "Critical strike! ";
                            }
                            _player.HitPoints -= damage;
                            battleLog += "you're took " + damage + " damage     ";
                            battleLog += "creature HP = " + _creature.HitPoints + "\n\n";


                            round++;
                        } while (_creature.HitPoints > 0 && _player.HitPoints > 0);
                        if (_player.HitPoints <= 0)
                        {
                            battleLog += "You're Lose the battle";
                        }
                        else
                        {
                            battleLog += "You're WIN the battle\n";
                            _player.Money += _creature.Money;
                            battleLog += "Money earned - " + _creature.Money + "     ";
                            int expEarned= (rnd.Next(250) + 250) * (rnd.Next(_player.Luck) + 1);
                            battleLog += "Experience earned - " + expEarned;
                            _player.Experience += expEarned;
                        }
                        MessageBox.Show(battleLog);
                        Character.SaveCharacter(_player);
                        Close();
                        break;
                    }
                case Key.E:
                    {
                        Close();
                        break;
                    }                
                default:
                    {
                        MessageBox.Show("Wrong input");
                        break;
                    }
            }
        }

    }
}
