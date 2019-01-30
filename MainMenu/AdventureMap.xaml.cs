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
    /// Interaction logic for AdventureMap.xaml
    /// </summary>
    public partial class AdventureMap : Window
    {
        const int mapCoordsX = 38;
        const int mapCoordsY = 18;
        static char[,] mapTiels;
        static bool[,] mapTielsReserved;
        Character cPlayer;
        AdventureMapGenerator amg;
        ColumnDefinition columDef;
        RowDefinition rowDef;
        List<Label> mapTielsList;
        static Creatures creature1;
        int playerTurn;

        public AdventureMap()
        {
            InitializeComponent();
            adventureMapForm.WindowState = System.Windows.WindowState.Maximized;
            // Обнуляем переменные
            amg = new AdventureMapGenerator();
            mapTiels = amg.GetMapTiels();
            mapTielsReserved = amg.GetTielsReserved();
            playerTurn = 0;

            cPlayer = Character.LoadCharacter();
            mapTielsList = new List<Label>();

            CreteAdventureMapGrid();
            DrawMap();
        }
        private void CreteAdventureMapGrid()
        {
            for (int i = 0; i < mapCoordsX; i++)
            {
                for (int j = 0; j < mapCoordsY; j++)
                {
                    columDef = new ColumnDefinition();
                    columDef.Width = new GridLength(36);
                    adventureMapGrid.ColumnDefinitions.Add(columDef);

                    rowDef = new RowDefinition();
                    rowDef.Height = new GridLength(36);
                    adventureMapGrid.RowDefinitions.Add(rowDef);
                }
            }
        }
        /// <summary>
        /// Прорисовывает карту
        /// </summary>
        private void DrawMap()
        {
            for (int y = 0; y < mapCoordsY; y++)
            {
                for (int x = 0; x < mapCoordsX; x++)
                {
                    ImageBrush imgBrush = new ImageBrush();
                    Label tileLabel = new Label();
                    BitmapImage bmi = new BitmapImage();
                    switch (mapTiels[x, y])
                    {
                        case '.':
                            {
                                tileLabel.Background = GetImageBrush("Земля.png");                               
                                break;
                            }
                        case '#':
                        case '&':
                            {
                                tileLabel.Background = GetImageBrush("Куст.png");
                                break;
                            }
                        case 'L':
                            {
                                tileLabel.Background = GetImageBrush("Дорога.png"); break;
                            }
                        default:
                            break;
                    }
                    mapTielsList.Add(tileLabel);
                    Grid.SetRow(tileLabel, y);
                    Grid.SetColumn(tileLabel, x);
                    adventureMapGrid.Children.Add(tileLabel);
                }
            }

            // Spawn Player
            cPlayer.CoordinateX = 34;
            cPlayer.CoordinateY = 9;
            mapTiels[cPlayer.CoordinateX, cPlayer.CoordinateY] = 'P';
            mapTielsList[cPlayer.CoordinateY * mapCoordsX + cPlayer.CoordinateX].Background = GetImageBrush("WarriorOnTheGround.png");
            // Spawn Creatures
            SpawnCreatures();
        }
        private void SpawnCreatures()
        {
            creature1 = new Creatures(mapTielsReserved);
            mapTiels[creature1.CoordinateX, creature1.CoordinateY] = 'C';
            mapTielsList[creature1.CoordinateY * mapCoordsX + creature1.CoordinateX].Background = GetImageBrush(creature1._imageName);
        }
        /// <summary>
        /// Return background image
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static ImageBrush GetImageBrush (string fileName)
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

        /// <summary>
        /// Действия при нажатии на клавишу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void adventureMapForm_KeyDown(object sender, KeyEventArgs e)
        {
            PlayerMovement(e);
        }
        private void PlayerMovement(KeyEventArgs e)
        {
            int changedY = 0, changedX = 0;
            switch (e.Key)
            {
                case Key.NumPad1:
                    {
                        changedX--;
                        changedY++;
                        break;
                    }
                case Key.NumPad2:
                case Key.Down:
                    {
                        changedY++;
                        break;
                    }
                case Key.NumPad3:
                    {
                        changedX++;
                        changedY++;
                        break;
                    }
                case Key.NumPad4:
                case Key.Left:
                    {
                        changedX--;
                        break;
                    }
                case Key.NumPad5:
                    {
                        // Waiting
                        break;
                    }
                case Key.NumPad6:
                case Key.Right:
                    {
                        changedX++;
                        break;
                    }
                case Key.NumPad7:
                    {
                        changedY--;
                        changedX--;
                        break;
                    }
                case Key.NumPad8:
                case Key.Up:
                    {
                        changedY--;
                        break;
                    }
                case Key.NumPad9:
                    {
                        changedX++;
                        changedY--;
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Wrong input");
                        break;
                    }
            }
            playerTurn++;

            if (mapTiels[cPlayer.CoordinateX + changedX, cPlayer.CoordinateY + changedY] == 'C')
            {
                BattleWindow battleWindow = new BattleWindow(creature1, cPlayer);
                battleWindow.Owner = this;
                battleWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                battleWindow.ShowDialog();

                SpawnCreatures();
            }

            if (cPlayer.HitPoints == 0)
            {
                MessageBox.Show("Local monks found your lifeless body and brought it to the castle. You were so glad, that gave them half of all your's money!");
                cPlayer.Money /= 2;
                cPlayer.HitPoints += 10;
                Character.SaveCharacter(cPlayer);
                GameWindow gameWindow = new GameWindow();
                Close();
                gameWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                gameWindow.ShowDialog();                
            }

            if (!mapTielsReserved[cPlayer.CoordinateX + changedX, cPlayer.CoordinateY + changedY])
            {
                mapTiels[cPlayer.CoordinateX, cPlayer.CoordinateY] = '.';
                mapTielsList[cPlayer.CoordinateY * mapCoordsX + cPlayer.CoordinateX].Background = GetImageBrush("Земля.png");

                cPlayer.CoordinateX += changedX;
                cPlayer.CoordinateY += changedY;

                mapTiels[cPlayer.CoordinateX, cPlayer.CoordinateY] = 'P';
                mapTielsList[cPlayer.CoordinateY * mapCoordsX + cPlayer.CoordinateX].Background = GetImageBrush("WarriorOnTheGround.png");
            }
            else if (mapTiels[cPlayer.CoordinateX + changedX, cPlayer.CoordinateY + changedY] == 'L')
            {
                Character.SaveCharacter(cPlayer);
                GameWindow gameWindow = new GameWindow();
                Close();
                gameWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                gameWindow.ShowDialog();
            }
        }

    }
}
